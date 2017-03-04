using System;
using System.Collections.Generic;
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
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CrossTxDownloadTest";

        private DownloadManager downloadMngr;

        public Updater()
        {
            downloadMngr = new DownloadManager(path);
        }

        public void DownloadFullCSV()
        {

        }

        public void DownloadLatestUpdateFile()
        {

        }

        public void DownloadLatestDeactivationFile()
        {

        }
    }
}
