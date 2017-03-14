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
            downloadMngr = new DownloadManager(zipPath);
        }

        public void DownloadFullCSV()
        {
            CreateDirectory();
            downloadMngr.DownloadFullCSV();
            downloadMngr.ExtractZIPToDirectory(zipPath, path);
            DeleteOldZip();
        }

        public void DownloadLatestUpdateFile()
        {
            CreateDirectory();
            downloadMngr.DownloadUpdateFile();
            downloadMngr.ExtractZIPToDirectory(zipPath, path);
            DeleteOldZip();
        }

        public void DownloadLatestDeactivationFile()
        {
            CreateDirectory();
            downloadMngr.DownloadDeactivationFile();
            downloadMngr.ExtractZIPToDirectory(zipPath, path);
            DeleteOldZip();
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void DeleteOldZip()
        {
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }
        }
    }
}
