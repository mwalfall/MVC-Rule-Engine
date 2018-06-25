using System;
using System.Collections.Generic;

namespace RuleEngine
{
    public partial class RuleStatement : LogicProcessor
    {
        #region Privates
        protected List<int> _rsBracketNo = new List<int>();
        protected List<int> _ruleConnectors = new List<int>();
        protected RuleValidationService _service;
        protected List<bool> _ruleSetResults = new List<bool>();
        #endregion
     
        /// ------------------------------------------------------------------------
        /// <summary>
        /// Determines of the condiions for the rule set have been satisfied.
        /// </summary>
        /// <returns>boolean</returns>
        /// ------------------------------------------------------------------------
        public bool IsSatisfied(RuleValidationService service)
        {
            ConfigureObject(service);

            if (tblRuleSets.Count == 0)
                return true;

            foreach (var ruleSet in tblRuleSets)
            {
                _ruleSetResults.Add(_service.IsSatisfied(ruleSet));
            }

            return _ruleConnectors.Count == 0 
                ? _ruleSetResults[0] 
                : ProcessResults(_rsBracketNo, _ruleConnectors, _ruleSetResults);
        }

        private void ConfigureObject(RuleValidationService service)
        {
            if (service == null)
                throw new NullReferenceException("RuleValidationService object not provided.");
            _service = service;


            if (RsBracketNo == null)
                throw new ArgumentNullException("RsBracketNo", "Required Properties were not provided.");
            if (RuleConnectors == null)
                throw new ArgumentNullException("RuleConnectors", "Required Properties were not provided.");

            var rsBracketNo = RsBracketNo.Split(',');
            var ruleConnectors = RuleConnectors.Split(',');

            if (rsBracketNo == null || ruleConnectors == null)
                throw new NullReferenceException("RsBracketNo or RuleConnectors was not provided.");

            if (!String.IsNullOrEmpty(rsBracketNo[0]))
            {
                foreach (var bracket in rsBracketNo)
                {
                    _rsBracketNo.Add(Convert.ToInt32((string)bracket));
                }
            }
            if (!String.IsNullOrEmpty(ruleConnectors[0]))
            {
                foreach (var connector in ruleConnectors)
                {
                    _ruleConnectors.Add(Convert.ToInt32((string)connector));
                }
            }
            ProcessCollection();
        }

        private void ProcessCollection()
        {
            if (tblRuleSets == null)
                throw new ArgumentNullException("RuleSets", "Required Properties were not provided.");
            if (tblRuleSets.Count != _rsBracketNo.Count)
                throw new ArgumentException("RuleSets", "RuleSet and RsBracketNo must contain the same number of items.");
            if (tblRuleSets.Count > 1)
            {
                if (tblRuleSets.Count != _ruleConnectors.Count + 1)
                    throw new ArgumentException("RuleSets", "RuleSet and RuleConnectors out of sequence.");
            }
        }
    }
}
