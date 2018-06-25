using System;
using Domain;
using RuleEngine.Interfaces;


namespace RuleEngine
{
    /// <summary>
    /// Obtains the Status transition set associated with a status. The set contains
    /// all the statuses that can be transitioned to from the present status. Evaluates
    /// each possible transition to determine which status the vehicle conforms to.
    /// </summary>
    public class TransitionSetEvaluationService
    {
        private VehicleModel _vehicle;
        private readonly IRuleValidationService _ruleValidationService;

        /// -----------------------------------------------------------------------
        /// <summary>
        /// CObtains the RuleValidationService object and the vehicle to
        /// be processed.
        /// </summary>
        /// <param name="ruleValidationService">IRuleValidationService object</param>
        /// <param name="vehicle">Vehicle object</param>
        /// -----------------------------------------------------------------------
        public TransitionSetEvaluationService(IRuleValidationService ruleValidationService, VehicleModel vehicle)
        {
            if(vehicle == null)
                throw new ArgumentNullException("vehicle", "required argument not provided.");
            _vehicle = vehicle;
            _ruleValidationService = ruleValidationService;
        }

        /// -----------------------------------------------------------------------
        /// <summary>
        /// Evaluates all possible status transitions associated with the present
        /// status of the vehicle until the first transition that is satisfied is
        /// obtained. At that point teh vehcile's TransitionStatus is set.
        /// </summary>
        /// <returns>Vehicle object</returns>
        /// -----------------------------------------------------------------------
        public VehicleModel Evaluate()
        {
            /*
            var transitionSet = _vehicle.tlkpVehicleStatus..tblStatusTransitionSet;
            if (transitionSet == null)
                return _vehicle;

            foreach ( var transitionObj in transitionSet)
            {
                
                foreach (var statusTransition in transitionObj.tblStatusTransition)
                {
                    //if (!transitionObj.IsSatisfied(_ruleValidationService)) continue;
                    _vehicle.TransitionStatus = transitionObj.VehicleTransitionStatus;
                    return _vehicle;
                }
                 
            }
             * * */
            return null;
        }
    }
}
