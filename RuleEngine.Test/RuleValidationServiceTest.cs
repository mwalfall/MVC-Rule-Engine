using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RuleEngine.Operators;

namespace RuleEngine.Test
{
    
    
    /// <summary>
    ///This is a test class for RuleValidationServiceTest and is intended
    ///to contain all RuleValidationServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RuleValidationServiceTest
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
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IsSatisfied
        ///</summary>

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void IsSatisfiedNullVehicleTest()
        {
            var target = new RuleValidationService(null);
            const bool expected = true;
            const bool actual = false;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedHasValueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                C511Date = new DateTime(2012,10,2),
                TransSN = "2343678",
                OMBYear = 0
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.IsNullOrEmpty, 
                    ExpectedValue = "123-abc" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedANDTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                },
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2345678" 
                },
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.AND };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedOrTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Plate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "123-abc" 
                },
                //false
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "2345678" 
                },
                //false
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "4000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = BoolOperator.OR };
            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentException))]
        public void IsSatisfiedComparisonExceptionTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,  
                    LeftPropertyName = "Miles", 
                    RightPropertyName = "Transmission",
                    ComparisonOperator = LogicOperator.Equal
                },
                //false
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "2345678" 
                },
                //false
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "4000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                TransSN = "2343678",
                OMBYear = 3000,
                Fuel = 3000
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,  
                    LeftPropertyName = "OMBYear", 
                    RightPropertyName = "Fuel",
                    ComparisonOperator = LogicOperator.Equal
                },
                //true
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "2343678" 
                },
                //true
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonFalseTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000,
                Fuel = 4000
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,  
                    LeftPropertyName = "OMBYear", 
                    RightPropertyName = "Fuel",
                    ComparisonOperator = LogicOperator.Equal
                },
                //true
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "2343678" 
                },
                //true
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = false;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedANDBoolTypeTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000,
                Formalized = true
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Formalized", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "true" 
                },
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "2345678" 
                },
                new Rule 
                { 
                    RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "OMBYear", 
                    ComparisonOperator = LogicOperator.GreaterThan, 
                    ExpectedValue = "2000" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedANDBoolTypeFalseTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                Formalized = false
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "Formalized", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "false" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedANDDateTimeTypeTrueTest()
        {
            var vehicle = new VehicleModelProxy
            {
                AcceptedDate = new DateTime(2012, 7, 31)
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "AcceptedDate", 
                    ComparisonOperator = LogicOperator.Equal, 
                    ExpectedValue = "07/31/2012" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedANDDateTimeTypeFalseTest()
        {
            var vehicle = new VehicleModelProxy
            {
                AcceptedDate = new DateTime(2012, 7, 31)
            };

            var ruleList = new List<Rule>{   
                new Rule 
                {   RuleType = RuleType.UnaryRule,  
                    LeftPropertyName = "AcceptedDate", 
                    ComparisonOperator = LogicOperator.NotEqual, 
                    ExpectedValue = "07/31/2012" 
                }
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = false;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonDateEqualTest()
        {
            var vehicle = new VehicleModelProxy
            {
                CondemDate = new DateTime(2012, 08, 01),
                AcceptedDate = new DateTime(2012, 08, 01)
            };
            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,  
                    LeftPropertyName = "CondemDate", 
                    RightPropertyName = "AcceptedDate",
                    ComparisonOperator = LogicOperator.Equal
                },
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonDateNotEqualTest()
        {
            var vehicle = new VehicleModelProxy
            {
                CondemDate = new DateTime(2012, 07, 31),
                AcceptedDate = new DateTime(2012, 08, 01)
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,  
                    LeftPropertyName = "CondemDate", 
                    RightPropertyName = "AcceptedDate",
                    ComparisonOperator = LogicOperator.NotEqual
                },
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonDateGreaterTanTest()
        {
            var vehicle = new VehicleModelProxy
            {
                CondemDate = new DateTime(2012, 07, 31),
                AcceptedDate = new DateTime(2012, 08, 01)
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.ComparisonRule,
                    LeftPropertyName = "AcceptedDate",
                    RightPropertyName = "CondemDate", 
                    ComparisonOperator = LogicOperator.GreaterThan
                },
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsSatisfiedComparisonHasValueStringTest()
        {
            var vehicle = new VehicleModelProxy
            {
                Plate = "123-abc",
                TransSN = "2343678",
                OMBYear = 3000
            };

            var ruleList = new List<Rule>{ 
                //true
                new Rule 
                {   RuleType = RuleType.UnaryRule, 
                    LeftPropertyName = "TransSN",
                    RightPropertyName = null, 
                    ComparisonOperator = LogicOperator.HasValue
                },
            };
            var ruleSet = new RuleSet { tblRules = ruleList, Operation = Convert.ToInt32(BoolOperator.AND) };

            var target = new RuleValidationService(vehicle);
            target.IsSatisfied(ruleSet);
            const bool expected = true;
            var actual = target.IsSatisfied(ruleSet);
            Assert.AreEqual(expected, actual);
        }
    }
}
