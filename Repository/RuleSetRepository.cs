using System;
using System.Collections.Generic;
using System.Linq;
using Repository.UnitOfWorkInterfaces;
using System.Data.Objects;
using RuleEngine;
using RuleEngine.RepositoryInterfaces;

namespace Repository
{
    public class RuleSetRepository : GenericRepository<RuleSet>, IRuleSetRepository
    {
        #region Constructor

        public RuleSetRepository(IUnitOfWork uoW)
            : base(uoW)
        {
            if (uoW == null)
                throw new ArgumentNullException("uoW","Unit of Work not Provided");
        }

        #endregion
        #region Public Methods

        /// --------------------------------------------------------------
        /// <summary>
        /// Given a ruleset id return the ruleset object.
        /// </summary>
        /// <param name="id">RuleSet Id</param>
        /// <returns>RuleSet object</returns>
        /// -------------------------------------------------------------
        public RuleSet GetRuleSet(int id)
        {
            if (id == 0)
                throw new ArgumentException("id" ,"Rule ID was not provided.");

            var include = new string[] {"tblRules"};
            return GetFirst(x => x.Id == id, include);
        }
        #endregion
    }
}

