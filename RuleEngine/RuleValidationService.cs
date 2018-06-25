using System;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Interfaces;
using RuleEngine.Operators;

namespace RuleEngine
{
    public class RuleValidationService : IRuleValidationService
    {
        #region Private objects

        private readonly VehicleModelProxy _vehicle;

        #endregion
        #region Constructor

        public RuleValidationService(VehicleModelProxy vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle","The are no constructors that do not take an parameter" );
            _vehicle = vehicle;
        }
        #endregion

        /// -----------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ruleSet"></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------
        public bool IsSatisfied(RuleSet ruleSet)
        {
            if (ruleSet.tblRules == null)
                throw new ArgumentNullException("ruleSet","Rules are required to perform validation.");

            var rules = ruleSet.tblRules;
            var compiledRules = rules.Select(CompileRule<VehicleModelProxy>).ToList();
            return ruleSet.Operation == BoolOperator.AND 
                ? compiledRules.All(rule => rule(_vehicle)) 
                : compiledRules.Any(rule => rule(_vehicle));
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="ruleSet"></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------
        private bool UnaryOperation(VehicleModelProxy vehicle, RuleSet ruleSet)
        {
            var rules = ruleSet.tblRules;
            var compiledRules = rules.Select(CompileRule<VehicleModelProxy>).ToList();
            return ruleSet.Operation == BoolOperator.AND
                ? compiledRules.All(rule => rule(vehicle)) 
                : compiledRules.Any(rule => rule(vehicle));
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------
        static Func<T, bool> CompileRule<T>(Rule r)
        {
            var paramExprNode = Expression.Parameter(typeof(T));
            var expr = BuildExpr<T>(r, paramExprNode);
            return Expression.Lambda<Func<T, bool>>(expr, paramExprNode).Compile();
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="r"></param>
        /// <param name="paramExprNode"></param>
        /// <returns></returns>
        /// -----------------------------------------------------------------------
        static Expression BuildExpr<T>(Rule r, Expression paramExprNode)
        {
            dynamic right;
            ExpressionType tBinary;

            var left = Expression.Property(paramExprNode, r.LeftPropertyName);
            var leftPropertyType = typeof(T).GetProperty(r.LeftPropertyName).PropertyType;

            //Is operator a known .NET operator
            if (Enum.TryParse(r.ComparisonOperator, out tBinary))
            {
                if (r.RuleType == RuleType.UnaryRule)
                    right = Expression.Constant(Convert.ChangeType(r.ExpectedValue, leftPropertyType));
                else
                {
                    var rightPropertyType = typeof(T).GetProperty(r.RightPropertyName).PropertyType;
                    if (rightPropertyType != leftPropertyType)
                        throw new ArgumentException("The requested operation could not be performed because the  objects are not of the same type", r.LeftPropertyName + " and " + r.RightPropertyName);
                    right = Expression.Property(paramExprNode, r.RightPropertyName);
                }
                return Expression.MakeBinary(tBinary, left, right);
            }

            if (r.ComparisonOperator == LogicOperator.HasValue)
            {
                // x => x != null && x.Length > 0;
                if (leftPropertyType == typeof(string)) 
                {
                    // x != null
                    Expression eNull = Expression.Constant(null, typeof(string));
                    Expression e1 = Expression.NotEqual(left, eNull);

                    // x.Length > 0
                    Expression eZero = Expression.Constant(0, typeof (int));
                    Expression e2 = Expression.GreaterThan(Expression.Property(left, "Length"), eZero);

                    return Expression.And(e1, e2);
                }
                 
                if (leftPropertyType == typeof(DateTime))
                {
                    right = Expression.Constant(DateTime.MinValue, typeof (DateTime));
                    return Expression.NotEqual(left, right);
                }

                if (leftPropertyType == typeof(int))
                {
                    return Expression.NotEqual(left, Expression.Constant(0, typeof(int)));
                }
            }

            if (r.ComparisonOperator == LogicOperator.IsNullOrEmpty)
            {
                // x => x == null || x.Length == 0;
                if (leftPropertyType == typeof(string))
                {
                    // x != null
                    Expression eNull = Expression.Constant(null, typeof(string));
                    Expression e1 = Expression.Equal(left, eNull);

                    // x.Length > 0
                    Expression eZero = Expression.Constant(0, typeof(int));
                    Expression e2 = Expression.Equal(Expression.Property(left, "Length"), eZero);

                    return Expression.OrElse(e1, e2);
                }
                 
                if (leftPropertyType == typeof(DateTime))
                {
                    right = Expression.Constant(DateTime.MinValue, typeof(DateTime));
                    return Expression.Equal(left, right);
                }

                if (leftPropertyType == typeof(int))
                {
                    return Expression.Equal(left, Expression.Constant(0, typeof(int)));
                }
            }

            return null;
        }
    }
}