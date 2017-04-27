using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossTxUpdateClient.Configurations;
using CrossTxUpdateClient.UpdateAPI;
using CrossTxUpdateClient.UIControllers;
using System.Windows;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrossTxUpdateClient.Testing.UITests
{
    [TestClass]
    public class UITest
    {
        private static MainWindow window;

        [TestInitialize]
        public void Init()
        {
            window = new MainWindow();
        }

        [TestMethod]
        public void SetEnableAutoDeactivateConfig_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetEnableAutoDeactivationsConfig(true);
            Assert.IsTrue(ConfigurationManager.EnableAutoDeactivations == true);

            LocalTestInstance.SetEnableAutoDeactivationsConfig(false);
            Assert.IsTrue(ConfigurationManager.EnableAutoDeactivations == false);
        }
        
        [TestMethod]
        public void GetEnableAutoDeactivationsConfig_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetEnableAutoDeactivationsConfig(true);
            var LogicalAssignment = LocalTestInstance.GetEnableAutoDeactivationsConfig();

            Assert.IsTrue(LogicalAssignment);

            LocalTestInstance.SetEnableAutoDeactivationsConfig(false);
            LogicalAssignment = LocalTestInstance.GetEnableAutoDeactivationsConfig();

            Assert.IsFalse(LogicalAssignment);
        }

        [TestMethod]
        public void SetEnableAutoUpdatesConfig_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetEnableAutoUpdatesConfig(true);
            Assert.IsTrue(ConfigurationManager.EnableAutoUpdates == true);

            LocalTestInstance.SetEnableAutoUpdatesConfig(false);
            Assert.IsTrue(ConfigurationManager.EnableAutoUpdates == false);
        }

        [TestMethod]
        public void GetEnableAutoUpdatesConfig_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetEnableAutoUpdatesConfig(true);
            var LogicalAssignment = LocalTestInstance.GetEnableAutoUpdatesConfig();

            Assert.IsTrue(LogicalAssignment);

            LocalTestInstance.SetEnableAutoUpdatesConfig(false);
            LogicalAssignment = LocalTestInstance.GetEnableAutoUpdatesConfig();

            Assert.IsFalse(LogicalAssignment);
        }

        [TestMethod]
        public void SetTimeBetweenDeactivations_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetTimeBetweenDeactivations(5);

            var TimeBetweenDeactivations = LocalTestInstance.GetTimeBetweenDeactivations();

            Assert.IsTrue(TimeBetweenDeactivations == 5);
        }

        [TestMethod]
        public void GetTimeBetweenDeactivations_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetTimeBetweenDeactivations(5);

            var TimeBetweenDeactivations = LocalTestInstance.GetTimeBetweenDeactivations();

            Assert.IsTrue(TimeBetweenDeactivations == 5);
        }

        [TestMethod]
        public void SetTimeBetweenUpdates_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetTimeBetweenUpdates(5);

            var TimeBetweenUpdates = LocalTestInstance.GetTimeBetweenUpdates();

            Assert.IsTrue(TimeBetweenUpdates == 5);
        }

        [TestMethod]
        public void GetTimeBetweenUpdates_Test()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.SetTimeBetweenUpdates(5);

            var TimeBetweenUpdates = LocalTestInstance.GetTimeBetweenUpdates();

            Assert.IsTrue(TimeBetweenUpdates == 5);
        }

        [TestMethod]
        public void DownloadCSV_NoExceptions()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.DownloadFullCSV();
        }

        [TestMethod]
        public void DownloadUpdate_NoExceptions()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.DownloadUpdateFile();
        }

        [TestMethod]
        public void DownloadDeactivation_NoExceptions()
        {
            UserInterfaceController LocalTestInstance = new UserInterfaceController(window);
            LocalTestInstance.DownloadDeactivationFile();
        }
    }
}
