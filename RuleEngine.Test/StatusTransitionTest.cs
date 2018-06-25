using RulesDataAccess;
using RulesDataAccess.UnitOfWork;
using RulesRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuleEngine.Test
{
    /// <summary>
    ///This is a test class for RuleSetRepositoryTest and is intended
    ///to contain all RuleSetRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatusTransitionTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        [TestMethod()]
        public void EagerLoadingTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var uoW = new EfUnitOfWork(new DSNY_EngineEntities());
            var repository = new GenericRepository<StatusTransition>(uoW);
            var include = new[] { "tblTransitionSpecification.tblRuleStatements.tblRuleSets.tblRules" };
            var statusTransition = repository.GetFirst(x => x.VehicleStatusId == 3, include);
            const bool expected = false;
            if(statusTransition == null)
                Assert.Inconclusive("StatusTransition object not returned.");
            else
            {
                var actual = statusTransition.IsTransitioned();
                Assert.AreEqual(expected, actual);
            }  
        }
    }
}
