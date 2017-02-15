using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossTxUpdateClient.UpdateAPI;

namespace CrossTxUpdateClient.Testing.UpdateAPITests
{
    [TestClass]
    public class DownloadManagerTest
    {
        /// <summary>
        /// This method is for testing that that csv files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadCSVFile_FileDownloadSuccessful()
        {
            DownloadManager dlManager = new DownloadManager("C:\\Users\\Lane\\Desktop\\csv.zip");
            dlManager.DownloadFullCSV();
        }

        /// <summary>
        /// This method is for testing that that update files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadUpdateFile_FileDownloadSuccessful()
        {
            DownloadManager dlManager = new DownloadManager("C:\\Users\\Lane\\Desktop\\update.zip");
            dlManager.DownloadUpdateFile();
        }

        /// <summary>
        /// This method is for testing that that deactivation files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadDeactivationFile_FileDownloadSuccessful()
        {
            DownloadManager dlManager = new DownloadManager("C:\\Users\\Lane\\Desktop\\deactivation.zip");
            dlManager.DownloadDeactivationFile();
        }
    }
}
