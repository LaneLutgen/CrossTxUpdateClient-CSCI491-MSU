using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bytescout.Spreadsheet;
using CrossTxUpdateClient.UIControllers;
using CrossTxUpdateClient.DB;
using CrossTxUpdateClient.Configurations;


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
        public UserInterfaceController Controller;

        private NPI_TYPE npiType = NPI_TYPE.None;

        private Updater(UserInterfaceController controller)
        {
            this.Controller = controller;
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CrossTxDownloadTest";
            zipPath = path + "\\csv.zip";
            downloadMngr = DownloadManager.CreateAndGetInstance(path, zipPath);
            dbMmgr = new DBManager(ConfigurationManager.ServerName, ConfigurationManager.DBName, ConfigurationManager.Username, ConfigurationManager.Password);
            controller.BindDataGrid(dbMmgr.GetLinksFromDB());
        }

        private static Updater instance;

        private static object mutex = new object();

        public static Updater CreateAndGetInstance(UserInterfaceController controller)
        {
            if (instance == null)
            {
                //Added lock incase we'd ever be creating a Updater from multiple threads (doubt it)
                lock (mutex)
                {
                    if (instance == null)
                    {
                        instance = new Updater(controller);
                    }
                }
            }

            return instance;
        }

        public static Updater Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    return null;
                }
            }
        }

        public object MainUI { get; internal set; }

        public bool DownloadFullCSV()
        {
            bool successfull = false;

            if (downloadMngr.CSVLinkFound)
            {
                CreateDirectory();
                downloadMngr.DownloadFullCSV();
                npiType = NPI_TYPE.Full;
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
                npiType = NPI_TYPE.Update;
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
                npiType = NPI_TYPE.Deactivation;
                successfull = true;
            }

            return successfull;
        }

        /// <summary>
        /// This method should be called whenever we want to do a full upload of the data set
        /// </summary>
        public void AddToDB(string filePath)
        {
            dbMmgr.SortedInsert(filePath, NPI_TYPE.Full);
        }

        /// <summary>
        /// This method should be called whenever we want to do an update to existing entries in the database
        /// </summary>
        public void UpdateDB(string filePath)
        {
            dbMmgr.SortedInsert(filePath, NPI_TYPE.Update);
        }

        /// <summary>
        /// This method should be called whenever we want to do a removal of entries in the database,
        /// this includes storing previously deactivated files
        /// </summary>
        public void RemoveFromDB(string filePath)
        {
            int numDeavtivated = dbMmgr.Remove(filePath);
            if (numDeavtivated > 0) {
                Controller.SetProgressLabelValue("Sucessfully Deactivated "+ numDeavtivated + " Entries!");
            }else
            {
                Controller.SetProgressLabelValue("No Entries Deactivated!");
            }
        }

        private void PublishLink(string link, NPI_TYPE type)
        {
            dbMmgr.AddLinkToDB(link, type.ToString());
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
            Controller.SetProgressLabelValue("Applying to database, please wait...");

            //Delete all files that aren't used
            string csvFile = CleanupDownloadFolder();

            switch (npiType)
            {
                case NPI_TYPE.Full:
                    AddToDB(csvFile);
                    break;
                case NPI_TYPE.Update:
                    UpdateDB(csvFile);
                    break;
                case NPI_TYPE.Deactivation:
                    RemoveFromDB(csvFile);
                    break;
            }
            
        }

        private string CleanupDownloadFolder()
        {
            DirectoryInfo di = new DirectoryInfo(this.path);
            FileInfo[] pdfFiles = di.GetFiles("*.pdf")
                                 .Where(p => p.Extension == ".pdf").ToArray();
            foreach (FileInfo file in pdfFiles)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }

            FileInfo[] deactivationFile = di.GetFiles("*.xlsx")
                                 .Where(p => p.Extension == ".xlsx").ToArray();

            foreach (FileInfo file in deactivationFile)
                try
                {
                    Spreadsheet document = new Spreadsheet();
 
                    document.LoadFromFile(file.FullName);
                    string csvFile = this.path + "NPPESDeactivatedNPIReport.csv";

                    // Save the document as CSV file
                    document.Workbook.Worksheets[0].SaveAsCSV(csvFile);
                    document.Close();
                }
                catch { }

            FileInfo[] headerFiles = di.GetFiles("*FileHeader.csv")
                                     .Where(p => p.Extension == ".csv").ToArray();

            foreach (FileInfo file in headerFiles)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch { }

            FileInfo[] actualFile = di.GetFiles("*.csv")
                                 .Where(p => p.Extension == ".csv").ToArray();

            return actualFile[0].FullName;
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

    public enum NPI_TYPE
    {
        Full,
        Update,
        Deactivation,
        None
    }
}
