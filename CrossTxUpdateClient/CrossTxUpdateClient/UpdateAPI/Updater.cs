using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.UIControllers;

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
        private UserInterfaceController controller;

        public Updater(UserInterfaceController controller)
        {
            this.controller = controller;
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CrossTxDownloadTest";
            zipPath = path + "\\csv.zip";
            downloadMngr = new DownloadManager(path, zipPath);
        }

        public void DownloadFullCSV()
        {
            CreateDirectory();
            downloadMngr.DownloadFullCSV();
        }

        public void DownloadLatestUpdateFile()
        {
            CreateDirectory();
            downloadMngr.DownloadUpdateFile();
        }

        public void DownloadLatestDeactivationFile()
        {
            CreateDirectory();
            downloadMngr.DownloadDeactivationFile();
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
