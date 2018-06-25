using System;
using System.Collections.Generic;

namespace RuleEngine
{
    public abstract class ProcessorBase : LogicProcessor
    {
        protected List<int> _rsBracketNo = new List<int>();
        protected List<int> _ruleConnectors = new List<int>();
        protected RuleValidationService _service;
        protected List<bool> _ruleSetResults = new List<bool>();

        /// ------------------------------------------------------------------------
        /// <summary>
        /// Determines of the condiions for the rule set have been satisfied.
        /// </summary>
        /// <returns>boolean</returns>
        /// ------------------------------------------------------------------------
        public abstract bool IsSatisfied(RuleValidationService service);

        protected void ConfigureObject(RuleValidationService service)
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
                    _rsBracketNo.Add(Convert.ToInt32((string) bracket));
                }
            }
            if (!String.IsNullOrEmpty(ruleConnectors[0]))
            {
                foreach (var connector in ruleConnectors)
                {
                    _ruleConnectors.Add(Convert.ToInt32((string) connector));
                }
            }
            ProcessCollection();
        }

        protected abstract void ProcessCollection();
    }
}