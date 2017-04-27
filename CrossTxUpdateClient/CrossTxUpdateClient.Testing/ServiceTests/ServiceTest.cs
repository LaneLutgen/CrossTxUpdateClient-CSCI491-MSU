using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossTxUpdateClient.Services;
using CrossTxUpdateClient.UpdateAPI;

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
        public void CheckForNewUpdates_Test_WithLink()
        {
            DownloadManager mngr = DownloadManager.CreateAndGetInstance("blah", "blah"); 
            DataTable table = new DataTable();
            table.Columns.Add("Link");
            table.Columns.Add("Date");
            table.Columns.Add("Type");
            table.Rows.Add(DownloadManager.UpdateURL, DateTime.Now.ToString(), "Update");

            ServiceManager.Start(table);
            bool found = ServiceManager.CheckForNewUpdates();
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void CheckForNewDeactivations_Test()
        {
            bool found = ServiceManager.CheckForNewDeactivations();
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void CheckForNewDeactivations_Test_WithLink()
        {
            DownloadManager mngr = DownloadManager.CreateAndGetInstance("blah", "blah");
            DataTable table = new DataTable();
            table.Columns.Add("Link");
            table.Columns.Add("Date");
            table.Columns.Add("Type");
            table.Rows.Add(DownloadManager.DeactivationURL, DateTime.Now.ToString(), "Update");

            ServiceManager.Start(table);
            bool found = ServiceManager.CheckForNewDeactivations();
            Assert.IsTrue(found);
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
