using Domain.RuleEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain;

namespace RuleEngine.Test
{
    
    
    /// <summary>
    ///This is a test class for RuleServiceTest and is intended
    ///to contain all RuleServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RuleServiceTest
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


        /// <summary>
        ///A test for RuleService Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void RuleServiceConstructorNoVehicleTest()
        {
            VehicleModel vehicle = null; 
            var target = new RuleService(vehicle);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        [TestMethod()]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void RuleServiceConstructorNoStatusTransitionObjectTest()
        {
            var vehicle = new VehicleModel {VehicleID = 0};
            var target = new RuleService(vehicle);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
        /// <summary>
        ///A test for ProcessVehicle
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Domain.dll")]
        public void ProcessVehicleTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            RuleService_Accessor target = new RuleService_Accessor(param0); // TODO: Initialize to an appropriate value
            VehicleModel vehicle = null; // TODO: Initialize to an appropriate value
            target.ProcessVehicle();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsTransitioned
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Domain.dll")]
        public void IsTransitionedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            RuleService_Accessor target = new RuleService_Accessor(param0); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsTransitioned = expected;
            actual = target.IsTransitioned;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
