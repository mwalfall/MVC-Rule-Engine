using System;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RuleEngine.Operators;

namespace RuleEngine.Test
{
    /// <summary>
    ///This is a test class for RuleStatementTest and is intended
    ///to contain all RuleStatementTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RuleStatementTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for RuleStatement Constructor
        ///</summary>
        [TestMethod()]
        public void RuleStatementConstructorTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var service = new RuleValidationService(vehicle);
            var target = new RuleStatement();
            Assert.IsTrue(target != null);
        }

        /// <summary>
        /// RuleValidationService object not provided.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsSatisfiedServiceNullTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var target = new RuleStatement(); 
            const bool expected = false; 
            var actual = target.IsSatisfied(new RuleValidationService(vehicle));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// RuleSet null test.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void IsSatisfiedRuleSetNullTest()
        {

            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var service = new RuleValidationService(vehicle);
            var rsBracketNo = new List<int> {0};
            var ruleConnectors = new List<int> {BoolOperator.AND};
            var target = new RuleStatement()
            { 
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            
            const bool expected = false;
            var actual = target.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// RsBracketsNo null test.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsSatisfiedRsBracketNoNullTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleConnectors = new List<int> {BoolOperator.AND};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  // 0
                    LeftPropertyName = "OBMYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet{ tblRules = ruleList, Operation = BoolOperator.AND};
            var ruleSets = new List<RuleSet> {ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RuleConnectors = String.Join(",",ruleConnectors)
            };

            const bool expected = false; 
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        /// RuleConnector null test.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsSatisfiedRuleConnectorNullTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var rsBracketNo = new List<int> {0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OBMYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo)
            };

            const bool expected = false; 
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// RuleSet count must equal RsBracketNo count test.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void IsSatisfiedRuleSetNotEqualRsBracketNoTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleConnectors = new List<int> {BoolOperator.AND};

            var rsBracketNo = new List<int>();

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OBMYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                },
                new Rule 
                { 
                    RuleType = Convert.ToInt32(RuleType.UnaryRule),  
                    LeftPropertyName = "OBMYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };

            const bool expected = false;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// RuleSet count must be one greater than qual ruleConnector count test.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void IsSatisfied_RuleSet_OneGreaterThan_RuleConnector_Test()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND};
            var rsBracketNo = new List<int> {0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                },
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Miles", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };

            const bool expected = false;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        /// Single rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedSingleRuleSetTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleConnectors = new List<int>();

            var rsBracketNo = new List<int> {0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };

            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A(T) & B(T) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTwoRuleSetsTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND};
            var rsBracketNo = new List<int> {0, 0};
            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// A(T) & B(F) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTwoRuleSetsTandfTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND};
            var rsBracketNo = new List<int> {0, 0};
            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = false;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A(T) & B(T) & C(T) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTwoRuleSetsTandTandTTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND};
            var rsBracketNo = new List<int> {0, 0, 0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = Convert.ToInt32(RuleType.UnaryRule),  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };
            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23483678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet1};

            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A(T) & B(F) & C(T) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTandFandTTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND};
            var rsBracketNo = new List<int> {0, 0, 0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = (int)BoolOperator.AND };

            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = Convert.ToInt32(RuleType.UnaryRule), 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleList2 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23483678" 
                }
            };
            var ruleSet2 = new RuleSet { tblRules = ruleList2, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet2};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = false;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// A(T) & B(F) | C(T) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTandForTTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.OR};
            var rsBracketNo = new List<int> {0, 0, 0};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };

            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleList2 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23483678" 
                }
            };
            var ruleSet2 = new RuleSet { tblRules = ruleList2, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet2};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [A(T) & B(F) & C(T)] | D(T) rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTandFandTorTTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND, BoolOperator.OR};
            var rsBracketNo = new List<int> {0, 0, 0, 1};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };

            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleList2 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23483678" 
                }
            };
            var ruleSet2 = new RuleSet { tblRules = ruleList2, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet2, ruleSet2};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [A(T) & B(F)] & [C(T)] | D(T)] rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTandFAndTorTReturnfalseTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND, BoolOperator.OR};
            var rsBracketNo = new List<int> {0, 0, 1, 1};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };

            var ruleList1 = new List<Rule>
            {
                new Rule 
                {  
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleList2 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23483678" 
                }
            };
            var ruleSet2 = new RuleSet { tblRules = ruleList2, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet2, ruleSet2};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };
            const bool expected = false;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// [A(T) & B(T)] & [C(F)] | D(T)] rule test.
        /// </summary>
        [TestMethod()]
        public void IsSatisfiedTandTAndForTReturnfalseTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };
            var ruleConnectors = new List<int> {BoolOperator.AND, BoolOperator.AND, BoolOperator.OR};
            var rsBracketNo = new List<int> {0, 0, 0, 1};

            var ruleList = new List<Rule>{   
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };

            var ruleList1 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "23453678" 
                }
            };
            var ruleSet1 = new RuleSet { tblRules = ruleList1, Operation = BoolOperator.AND };
            var ruleList2 = new List<Rule>
            {
                new Rule 
                { 
                    RuleType = (int)RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2343678" 
                }
            };

            var ruleSet2 = new RuleSet { tblRules = ruleList2, Operation = BoolOperator.AND };
            var ruleSets = new List<RuleSet> {ruleSet, ruleSet1, ruleSet2, ruleSet1};
            var service = new RuleValidationService(vehicle);
            var statement = new RuleStatement()
            {
                tblRuleSets = ruleSets,
                RsBracketNo = String.Join(",",rsBracketNo),
                RuleConnectors = String.Join(",",ruleConnectors)
            };

            const bool expected = true;
            var actual = statement.IsSatisfied(service);
            Assert.AreEqual(expected, actual);
        }
    }
}
