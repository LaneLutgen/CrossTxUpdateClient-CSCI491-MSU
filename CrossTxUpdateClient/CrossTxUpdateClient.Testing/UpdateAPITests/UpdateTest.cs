using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CrossTxUpdateClient.UpdateAPI;

namespace CrossTxUpdateClient.Testing.UpdateAPITests
{
    [TestClass]
    public class UpdateTest
    {
        [TestMethod]
        public void DownloadCSV_FoundTrue()
        {
            Updater updater = Updater.CreateAndGetInstance(new UIControllers.UserInterfaceController(new MainWindow()));

            bool successful = updater.DownloadFullCSV();

            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void DownloadUpdate_FoundTrue()
        {
            Updater updater = Updater.CreateAndGetInstance(new UIControllers.UserInterfaceController(new MainWindow()));

            bool successful = updater.DownloadLatestUpdateFile();

            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void DownloadDeactivation_FoundTrue()
        {
            Updater updater = Updater.CreateAndGetInstance(new UIControllers.UserInterfaceController(new MainWindow()));

            bool successful = updater.DownloadLatestDeactivationFile();

            Assert.IsTrue(successful);
        }
    }
}
