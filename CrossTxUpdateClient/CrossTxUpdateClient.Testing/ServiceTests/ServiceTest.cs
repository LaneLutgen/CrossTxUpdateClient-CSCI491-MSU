using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossTxUpdateClient.Services;

namespace CrossTxUpdateClient.Testing.ServiceTests
{
    [TestClass]
    public class ServiceTest
    {
        [TestInitialize]
        public void StartService()
        {
            ServiceManager.Start(new System.Data.DataTable());
        }

        [TestMethod]
        public void CheckForNewUpdates_Test()
        {
            bool found = ServiceManager.CheckForNewUpdates();
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void CheckForNewDeactivations_Test()
        {
            bool found = ServiceManager.CheckForNewDeactivations();
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void AutoDownloadUpdate_Test()
        {
            ServiceManager.DownloadUpdateFile();

            //No exceptions result
        }

        [TestMethod]
        public void AutoDownloadDeactivation_Test()
        {
            ServiceManager.DownloadDeactivationFile();

            //No exceptions result
        }

        [TestMethod]
        public void StopServiceManager_Test()
        {
            ServiceManager.Stop();

            //No exceptions result
        }
    }
}
