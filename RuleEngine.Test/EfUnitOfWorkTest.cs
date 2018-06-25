using RulesDataAccess;
using RulesDataAccess.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Objects;

namespace RuleEngine.Test
{
    /// <summary>
    ///This is a test class for EfUnitOfWorkTest and is intended
    ///to contain all EfUnitOfWorkTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EfUnitOfWorkTest
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

        // Tests ===========================================================

        [TestMethod()]
        public void GetContextTest()
        {
            ObjectContext context = new DSNY_EngineEntities(); 
            var target = new EfUnitOfWork(context);
            var actual = target.GetContext;
            Assert.IsTrue(actual != null, "Actual = " + actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void EfUnitOfWorkConstructorNullContextTest()
        {
            var target = new EfUnitOfWork(null);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        public void EfUnitOfWorkConstructorTest()
        {
            var target = new EfUnitOfWork(new DSNY_EngineEntities());
            Assert.IsTrue(target != null, "target returns: " + target);
        }
    }
}
