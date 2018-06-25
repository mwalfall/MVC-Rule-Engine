using System.Collections.Generic;

namespace RuleEngine
{
    /// -----------------------------------------------------------------------
    /// <summary>
    /// The important point here is that it is an ordered set. The vehicle
    ///  will be set the the first Status
    /// Transition condition that is satisfied.
    /// </summary>
    /// -----------------------------------------------------------------------
    public partial class StatusTransitionSet
    {
        public List<StatusTransition> StatusTransitions;
    }
}
