using System;
using System.Collections.Generic;

namespace RuleEngine
{
    public partial class TransitionSpecification : LogicProcessor
    {
        private List<int> _rsBracketNo = new List<int>();
        private List<int> _ruleConnectors = new List<int>();
        private List<bool> _results = new List<bool>();
        private RuleValidationService _service;

        /// ---------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------
        public bool IsSatisfied(RuleValidationService service)
        {
            ConfigureObject(service);
            
            foreach (var statement in tblRuleStatements)
            {
                _results.Add(statement.IsSatisfied(service));
            }

            return _ruleConnectors.Count == 0 
                ? _results[0] 
                : ProcessResults(_rsBracketNo, _ruleConnectors, _results);
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
                    _rsBracketNo.Add(Convert.ToInt32(bracket));
                }
            }
            if (!String.IsNullOrEmpty(ruleConnectors[0]))
            {
                foreach (var connector in ruleConnectors)
                {
                    _ruleConnectors.Add(Convert.ToInt32(connector));
                }
            }
            ProcessCollection();
        }

        private void ProcessCollection()
        {
            if (tblRuleStatements == null)
                throw new ArgumentNullException("RuleStatements", "Required Properties were not provided.");
            if (tblRuleStatements.Count != _rsBracketNo.Count)
                throw new ArgumentException("RuleSets", "RuleSet and RsBracketNo must contain the same number of items.");
            if (tblRuleStatements.Count > 1)
            {
                if (tblRuleStatements.Count != _ruleConnectors.Count + 1)
                    throw new ArgumentException("RuleSets", "RuleSet and RuleConnectors out of sequence.");
            }
        }
    }
}
