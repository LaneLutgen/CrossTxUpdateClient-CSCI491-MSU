﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CrossTxUpdateClient.UpdateAPI
{
    public class DownloadManager
    {
        public int ProgessValue { get; private set; }

        public bool DownloadInProgress { get; private set; }

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

        public bool CSVLinkFound { get; set; }
        public bool UpdateLinkFound { get; set; }
        public bool DeactivationLinkFound { get; set; }

        private string filePath;

        private const string url = "http://download.cms.gov/nppes";
        private const string baseURL = "http://download.cms.gov/nppes/NPI_Files.html";

        public static string CsvURL;
        public static string UpdateURL;
        public static string DeactivationURL;

        private string zipPath;

        private DownloadManager(string filePath, string zipPath)
        {
            this.filePath = filePath;
            this.zipPath = zipPath;
            
            ParseHTMLForLatestLinks();
        }

        private static DownloadManager instance;

        private static object mutex = new object();

        public static DownloadManager CreateAndGetInstance(string filePath, string zipPath)
        {
            if(instance == null)
            {
                //Added lock incase we'd ever be creating a DownloadManager from multiple threads (doubt it)
                lock(mutex)
                {
                    if(instance == null)
                    {
                        instance = new DownloadManager(filePath, zipPath);
                    }
                }
            }

            return instance;
        }

        public static DownloadManager Instance
        {
            get
            {
                if(instance != null)
                {
                    return instance;
                }
                else
                {
                    return null;
                }
            }
        }

        public void ParseHTMLForLatestLinks()
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(baseURL);

            List<HtmlNode> links = new List<HtmlNode>();

            foreach(HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                links.Add(link);
            }

            string csvLink = FindLatestCSVDownloadURL(links);
            string updateLink = FindLatestUpdateDownloadURL(links);
            string deactivationLink = FindLatestDeactivationDownloadURL(links);

            CSVLinkFound = csvLink == null ? false : true;
            UpdateLinkFound= updateLink == null ? false : true;
            DeactivationLinkFound = deactivationLink == null ? false : true;

            CsvURL = url + csvLink;
            UpdateURL = url + updateLink;
            DeactivationURL = url + deactivationLink;
        }

        private string FindLatestCSVDownloadURL(List<HtmlNode> links)
        {
            DateTime curTime = DateTime.Now;

            foreach(HtmlNode node in links)
            {
                string hrefVal = node.GetAttributeValue("href", string.Empty);

                string curMonth = DateTime.Now.ToString("MMMM");
                DateTime lastMonth = curTime.AddMonths(-1);
                string prevMonth = lastMonth.ToString("MMMM");

                string curYear = DateTime.Now.ToString("yyyy");

                if (hrefVal.Contains("./NPPES_Data_Dissemination_" + curMonth + "_" + curYear) || hrefVal.Contains("NPPES_Data_Dissemination_" + prevMonth + "_" + curYear))
                {
                    return hrefVal.Substring(1);
                }
            }
            return null;
        }

        private string FindLatestUpdateDownloadURL(List<HtmlNode> links)
        {
            int daySeperation = int.MaxValue;

            string retval = null;

            DateTime curTime = DateTime.Now;

            //Regex pattern for a update file download link
            string basePattern = "(./NPPES_Data_Dissemination_)\\d{6}(?!\\d)";
            string datePattern = "(?<!\\d)\\d{6}(?!\\d)";

            foreach (HtmlNode node in links)
            {
                string hrefVal = node.GetAttributeValue("href", string.Empty);

                Match match = Regex.Match(hrefVal, basePattern, RegexOptions.IgnoreCase);

                //Check if the regex was matched for an update link
                if (match.Success)
                {
                    match = Regex.Match(hrefVal, datePattern, RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string date = match.Value;

                        int linkMonth = Int32.Parse(date.Substring(0, 2));
                        int linkDay = Int32.Parse(date.Substring(2, 2));
                        int linkYear = Int32.Parse(date.Substring(4, 2));

                        DateTime linkDate = new DateTime(linkYear, linkMonth, linkDay);

                        //Compare the days between links
                        TimeSpan diff = curTime.Subtract(linkDate);

                        //If this link is more recent, set it to the return value and update the max days seperated variable
                        if(diff.Days < daySeperation)
                        {
                            daySeperation = diff.Days;
                            retval = hrefVal.Substring(1);
                        }
                    }
                }
            }
            return retval;
        }

        private string FindLatestDeactivationDownloadURL(List<HtmlNode> links)
        {
            DateTime curTime = DateTime.Now;

            //Regex pattern for deactivation file (there should only be one so we don't need to check the date)
            string basePattern = "(./NPPES_Deactivated_NPI_Report_)\\d{6}(?!\\d)";

            foreach (HtmlNode node in links)
            {
                string hrefVal = node.GetAttributeValue("href", string.Empty);

                Match match = Regex.Match(hrefVal, basePattern, RegexOptions.IgnoreCase);

                //Check if the regex was matched
                if (match.Success)
                {
                    //If this date is closer
                    return hrefVal.Substring(1);
                }
            }

            return null;
        }


        public void DownloadFullCSV()
        {
            if (!DownloadInProgress)
            {
                DownloadInProgress = true;

                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CSVDownload_Complete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(CSVDownload_ProgressChanged);

                //This can throw a WebException but we don't want to catch it here
                webClient.DownloadFileAsync(new Uri(CsvURL), zipPath);
            }
        }

        private void CSVDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            HandleDownloadCompletion();
        }

        private void CSVDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //This function can be used for a progress bar
            ProgessValue = e.ProgressPercentage;
        }

        public void DownloadUpdateFile()
        {
            if (!DownloadInProgress)
            {
                DownloadInProgress = true;

                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(UpdateFileDownload_Complete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(UpdateFileDownload_ProgressChanged);

                //This can throw a WebException but we don't want to catch it here
                webClient.DownloadFileAsync(new Uri(UpdateURL), zipPath); 
            }
        }

        private void UpdateFileDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            HandleDownloadCompletion();
        }

        private void UpdateFileDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //This function can be used for a progress bar
            ProgessValue = e.ProgressPercentage;
        }

        public void DownloadDeactivationFile()
        {
            if (!DownloadInProgress)
            {
                DownloadInProgress = true;

                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DeactivationDownload_Complete);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DeactivationDownload_ProgressChanged);

                //This can throw a WebException but we don't want to catch it here
                webClient.DownloadFileAsync(new Uri(DeactivationURL), zipPath);
            }
        }

        private void DeactivationDownload_Complete(object sender, AsyncCompletedEventArgs e)
        {
            HandleDownloadCompletion();
        }

        private void DeactivationDownload_ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //This function can be used for a progress bar
            ProgessValue = e.ProgressPercentage;
        }

        public void ExtractZip()
        {
            ExtractZIPToDirectory(zipPath, filePath);
            DeleteOldZip();
        }
        
        public void ExtractZIPToDirectory(string path, string destination)
        {
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string curPath = Path.Combine(destination, entry.FullName);

                    if (!File.Exists(curPath))
                    {
                        entry.ExtractToFile(curPath);
                    }
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

        private void HandleDownloadCompletion()
        {
            DownloadInProgress = false;
            ProgessValue = 0;
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
