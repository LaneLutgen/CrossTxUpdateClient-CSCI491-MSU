using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Updater()
        {
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
