using System.Collections.Generic;
using RuleEngine.Operators;

namespace RuleEngine
{
    public abstract class LogicProcessor
    {
        #region Privates

        private List<int> _rsBracketNo;
        private List<int> _ruleConnectors;
        private List<bool> _results;
        private int _x = 0;
        private bool _notFinished = true;
        private int _ruleLength;

        #endregion
        #region Protected Methods

        protected bool ProcessResults(List<int> rsBracketNo, List<int> ruleConnectors, List<bool> results)
        {
            _rsBracketNo = rsBracketNo;
            _ruleConnectors = ruleConnectors;
            _results = results;
            _ruleLength = _results.Count;

            return Process();
        }
        #endregion
        #region Private Methods

        private bool Process()
        {
            var bracketNo = _rsBracketNo[_x];
            var result = _results[_x];

            while (_notFinished)
            {
                if (bracketNo == _rsBracketNo[_x + 1])
                {
                    bool nxtResult;
                    if (_ruleConnectors[_x] == BoolOperator.AND)
                    {
                        nxtResult = _results[_x + 1];
                        result = And(result, nxtResult);
                    }
                    else
                    {
                        nxtResult = _results[_x + 1];
                        result = Or(result, nxtResult);
                    }
                }
                else
                {
                    _x++;
                    if ((_x + 1) == _ruleLength)
                    {
                        result = _ruleConnectors[_x - 1] == BoolOperator.AND ? And(result, _results[_x]) : Or(result, _results[_x]);
                    }
                    else
                    {
                        result = _ruleConnectors[_x - 1] == BoolOperator.AND ? And(result, Process()) : Or(result, Process());
                    }
                }

                _x++;
                if (_x == _ruleLength - 1 || _x > _ruleLength - 1)
                    _notFinished = false;
                else
                    bracketNo = _rsBracketNo[_x];
            }

            return result;
        }
        #endregion
        #region Private Static Methods

        private static bool And(bool leftResult, bool rightResult)
        {
            return leftResult && rightResult;
        }

        private static bool Or(bool leftResult, bool rightResult)
        {
            return leftResult || rightResult;
        }
        #endregion
    }
}
