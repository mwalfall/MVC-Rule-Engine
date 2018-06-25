using System;
using RulesDataAccess.UnitOfWork;
using RulesRepository;
using RuleEngine;
using RuleEngine.RepositoryInterfaces;
using RulesDataAccess;

namespace Rules
{
    public class EvaluationServiceFactory
    {
        private readonly IRepository<StatusTransition> _repository;

        #region Constructors

        public EvaluationServiceFactory()
        {
            _repository = new GenericRepository<StatusTransition>(new EfUnitOfWork(new DSNY_EngineEntities()));
        }
        
        public EvaluationServiceFactory(IRepository<StatusTransition> repository)
        {
            if (repository == null)
                throw new ArgumentNullException("repository", "Required parameter not provided.");
        }
        #endregion

        /// ---------------------------------------------------------------
        /// <summary>
        /// Create StatusTransition Object.
        /// </summary>
        /// <param name="vehicle">VehicleModelProxy object</param>
        /// <returns>StatusTransition object</returns>
        /// --------------------------------------------------------------
        public StatusTransition CreateStatusTransitionObject(VehicleModelProxy vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle", "Required argument not provided.");

            var include = new[] { "tblTransitionSpecification.tblRuleStatements.tblRuleSets.tblRules" };
            var statusTransition = _repository.GetFirst(x => x.VehicleStatusId == vehicle.VehicleStatus, include);
            if (statusTransition == null)
                return null;

            var service = new RuleValidationService(vehicle);
            if(service == null)
                throw new NullReferenceException("Unable to create RuleValidationService object.");

            statusTransition.ValidationService = service;
            return statusTransition;
        }
    }
}
