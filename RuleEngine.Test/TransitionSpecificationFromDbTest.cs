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
    public class TransitionSpecificationFromDbTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

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
            var repository = new GenericRepository<TransitionSpecification>(uoW);
            var include = new[] { "tblRuleStatements.tblRuleSets.tblRules" };
            var specification = repository.GetFirst(x => x.Id == 1, include);
            const bool expected = false;
            var actual = specification.IsSatisfied(new RuleValidationService(vehicle));
            Assert.AreEqual(expected, actual);
        }
    }
}
