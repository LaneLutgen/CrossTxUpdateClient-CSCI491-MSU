using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.Configurations;


namespace CrossTxUpdateClient.Testing.ConfigurationTests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        public bool EnableAutoUpdates_Test()
        {
            var value = ConfigurationManager.EnableAutoUpdates;
            Assert.IsTrue(value == true);

            if (value == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EnableAutoDeactivations_Test()
        {
            var value = ConfigurationManager.EnableAutoDeactivations;
            Assert.IsTrue(value == true);

            if (value == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TimeBetweenUpdates_Test()
        {
            var TimeSetting = ConfigurationManager.TimeBetweenUpdates;

            Assert.IsTrue(TimeSetting == ConfigurationManager.TimeBetweenUpdates);
        }

        public void TimeBetweenDeactivations_Test()
        {
            var DeactivationTime = ConfigurationManager.TimeBetweenDeactivations;
            Assert.IsTrue(DeactivationTime == ConfigurationManager.TimeBetweenDeactivations);
        }

        public void SaveSettings_Test()
        {
            //Have all other methods return a bool, check to see if they are all true???

            var EnableAutoUpdate = ConfigurationManager.EnableAutoUpdates;
            var EnableAutoDeactivate = ConfigurationManager.EnableAutoDeactivations;

            var IsCorrect = false;

            if ((EnableAutoUpdate == true) || (EnableAutoDeactivate == true))
            {
                IsCorrect = true;
            }
            else 
            {
                IsCorrect = false;
            }

            Assert.IsTrue(IsCorrect);
        }
    }
}
