using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.UIControllers;
using CrossTxUpdateClient.DB;

namespace CrossTxUpdateClient.UpdateAPI
{
    /// <summary>
    /// This class will be the API for performing both automatic and manual updates
    /// </summary>
    public class Updater
    {
        private string path;
        private string zipPath;

        private DownloadManager downloadMngr;
        private DBManager dbMmgr;
        private UserInterfaceController controller;

        public Updater(UserInterfaceController controller)
        {
            this.controller = controller;
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CrossTxDownloadTest";
            zipPath = path + "\\csv.zip";
            downloadMngr = new DownloadManager(path, zipPath);

            //This is hardcoded for now but ideally they will want to plug in their DB info
<<<<<<< HEAD
            dbMmgr = new DBManager("127.0.0.1", "nppes1", "root", "password");
=======
            dbMmgr = new DBManager("127.0.0.1", "nppes_1", "root", "4ppropri4teP4ssword");
            dbMmgr.OpenConnection();
>>>>>>> 9062ceb2e94d36c8ce91f6b48555388a9661adf0
        }

        public bool DownloadFullCSV()
        {
            bool successfull = false;

            if (downloadMngr.CSVLinkFound)
            {
                CreateDirectory();
                downloadMngr.DownloadFullCSV();
                successfull = true;
            }

            return successfull;
        }

        public bool DownloadLatestUpdateFile()
        {
            bool successfull = false;

            if (downloadMngr.UpdateLinkFound)
            {
                CreateDirectory();
                downloadMngr.DownloadUpdateFile();
                successfull = true;
            }

            return successfull;
        }

        public bool DownloadLatestDeactivationFile()
        {
            bool successfull = false;

            if (downloadMngr.DeactivationLinkFound)
            {
                CreateDirectory();
                downloadMngr.DownloadDeactivationFile();
                successfull = true;
            }

            return successfull;
        }

        /// <summary>
        /// This method should be called whenever we want to do a full upload of the data set
        /// </summary>
        public void AddToDB()
        {
            dbMmgr.SortedInsert(this.path);
        }

        /// <summary>
        /// This method should be called whenever we want to do an update to existing entries in the database
        /// </summary>
        public void UpdateDB()
        {
            dbMmgr.SortedInsert(this.path);
        }

        /// <summary>
        /// This method should be called whenever we want to do a removal of entries in the database,
        /// this includes storing previously deactivated files
        /// </summary>
        public void RemoveFromDB()
        {
            //dbMngr.Remove();   
        }

        public void UnzipFileAsync()
        {
            BackgroundWorker unzipWorker = new BackgroundWorker();
            unzipWorker.DoWork += unzipWorker_DoWork;
            unzipWorker.RunWorkerCompleted += unzipWorker_Complete;

            unzipWorker.RunWorkerAsync();
        }

        private void unzipWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            downloadMngr.ExtractZip();
        }

        private void unzipWorker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            controller.SetProgressLabelValue("Download and Extraction Complete!");
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public int GetProgressValue()
        {
            return downloadMngr.ProgessValue;
        }

        public bool IsDownloading()
        {
            return downloadMngr.DownloadInProgress;
        }
    }
}
