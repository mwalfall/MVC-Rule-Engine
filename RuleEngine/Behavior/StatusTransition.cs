using System;

namespace RuleEngine
{
    public partial class StatusTransition
    {
        public RuleValidationService ValidationService { private get; set; }
        
        public bool IsTransitioned()
        {
            if (ValidationService == null)
                throw new NullReferenceException("RuleValidationService object not provided.");

            return tblTransitionSpecification.IsSatisfied(ValidationService);
        }
    }
}
