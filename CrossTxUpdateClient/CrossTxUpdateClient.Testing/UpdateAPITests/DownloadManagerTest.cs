using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrossTxUpdateClient.UpdateAPI;

namespace CrossTxUpdateClient.Testing.UpdateAPITests
{
    [TestClass]
    public class DownloadManagerTest
    {
        public string testPath;

        [TestInitialize]
        public void InitTestComponents()
        {
            //Using Desktop for now to test but we can change this to something else like AppData
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CrossTxDownloadTest";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            testPath = path;
        }

        /// <summary>
        /// This method is for testing that that csv files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadCSVFile_FileDownloadSuccessful()
        {
            string path = testPath + "\\csv.zip";

            DownloadManager dlManager = new DownloadManager(testPath, path);
            dlManager.DownloadFullCSV();

            Assert.IsTrue(File.Exists(path));
        }

        /// <summary>
        /// This method is for testing that that update files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadUpdateFile_FileDownloadSuccessful()
        {
            string path = testPath + "\\update.zip";

            DownloadManager dlManager = new DownloadManager(testPath, path);
            dlManager.DownloadUpdateFile();

            Assert.IsTrue(File.Exists(path));
        }

        /// <summary>
        /// This method is for testing that a deactivation zip can be extracted
        /// </summary>
        [TestMethod]
        public void ExtractUpdateFile_FileExtractsSuccessfully()
        {
            string path = testPath + "\\update.zip";

            DownloadManager dlManager = new DownloadManager(testPath, path);
            dlManager.ExtractZIPToDirectory(path, testPath);

            //Not sure how to assert this, I just look in the folder
        }

        /// <summary>
        /// This method is for testing that that deactivation files are downloaded successfully 
        /// </summary>
        [TestMethod]
        public void DownloadDeactivationFile_FileDownloadSuccessful()
        {
            string path = testPath + "\\deactivation.zip";

            DownloadManager dlManager = new DownloadManager(testPath, path);
            dlManager.DownloadDeactivationFile();

            Assert.IsTrue(File.Exists(path));
        }

        /// <summary>
        /// This method is for testing that a deactivation zip can be extracted
        /// </summary>
        [TestMethod]
        public void ExtractDeactivationFile_FileExtractsSuccessfully()
        {
            string path = testPath + "\\deactivation.zip";

            DownloadManager dlManager = new DownloadManager(testPath, path);
            dlManager.ExtractZIPToDirectory(path, testPath);

            //Not sure how to assert this, I just look in the folder
        }
    }
}
