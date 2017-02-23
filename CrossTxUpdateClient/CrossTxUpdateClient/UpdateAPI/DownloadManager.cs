using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace CrossTxUpdateClient.UpdateAPI
{
    public class DownloadManager
    {
        
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        private string filePath;

        //http://download.cms.gov/nppes/NPI_Files.html Original file location

        //Note: these are hard coded right now but we will have to write the logic to change them to most recent
        private const string csvURL = "http://download.cms.gov/nppes/NPPES_Data_Dissemination_January_2017.zip";
        private const string updateURL = "http://download.cms.gov/nppes/NPPES_Data_Dissemination_020617_021217_Weekly.zip";
        private const string deactivationURL = "http://download.cms.gov/nppes/NPPES_Deactivated_NPI_Report_011017.zip";   

        public DownloadManager()
        {
            filePath = null;
        }

        public DownloadManager(string path)
        {
            filePath = path;
        }

        public void DownloadFullCSV()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CSVDownload_Complete);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(CSVDownload_ProgressChanged);

            //This can throw a WebException but we don't want to catch it here
            webClient.DownloadFile(new Uri(csvURL), filePath);
        }

        private void CSVDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            //Handler for download completion
            Console.WriteLine("CSV File Download Complete");
        }

        private void CSVDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //This function can be used for a progress bar
            int progress = e.ProgressPercentage;
            Console.WriteLine(progress);
            System.Diagnostics.Debug.WriteLine(progress);
        }

        public void DownloadUpdateFile()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateFileDownload_Complete);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateFileDownload_ProgressChanged);

            //This can throw a WebException but we don't want to catch it here
            webClient.DownloadFile(new Uri(updateURL), filePath);
        }

        private void UpdateFileDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Update File Download Complete");
        }

        private void UpdateFileDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            Console.WriteLine(progress);
        }

        public void DownloadDeactivationFile()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DeactivationDownload_Complete);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DeactivationDownload_ProgressChanged);

            //This can throw a WebException but we don't want to catch it here
            webClient.DownloadFile(new Uri(deactivationURL), filePath);
        }

        private void DeactivationDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Deactivation File Download Complete");
        }

        private void DeactivationDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            Console.WriteLine(progress);
        }
        
        public void ExtractZIPToDirectory(string path, string destination)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile(Path.Combine(destination, entry.FullName));
                }
            }
        }

        public void ExtractZIPToStream(Stream stream)
        {
            using (ZipArchive archive = new ZipArchive(stream))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Stream outputStream = entry.Open();
                    //Here we can return a list of the file streams or something
                }
            }
        }
    }
}
