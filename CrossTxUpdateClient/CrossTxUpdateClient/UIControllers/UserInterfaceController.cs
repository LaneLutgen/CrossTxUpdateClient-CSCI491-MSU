using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.Configurations;
using CrossTxUpdateClient.UpdateAPI;
using System.ComponentModel;
using System.Windows;
using System.Threading;

namespace CrossTxUpdateClient.UIControllers
{
    /// <summary>
    /// The controller passed to the UI
    /// </summary>
    public class UserInterfaceController : IUserInterfaceController
    {
        private MainWindow mainWindow;
        private Updater updater;

        public UserInterfaceController(MainWindow window)
        {
            mainWindow = window;
            updater = new Updater();
        }

        public void SetEnableAutoDeactivationsConfig(bool value)
        {
            ConfigurationManager.EnableAutoDeactivations = value;
        }

        public bool GetEnableAutoDeactivationsConfig()
        {
            return ConfigurationManager.EnableAutoDeactivations;
        }

        public void SetEnableAutoUpdatesConfig(bool value)
        {
            ConfigurationManager.EnableAutoUpdates = value;
        }

        public bool GetEnableAutoUpdatesConfig()
        {
            return ConfigurationManager.EnableAutoUpdates;
        }

        public void SetTimeBetweenDeactivations(int value)
        {
            ConfigurationManager.TimeBetweenDeactivations = value;
        }

        public int GetTimeBetweenDeactivations()
        {
            return ConfigurationManager.TimeBetweenDeactivations;
        }

        public void SetTimeBetweenUpdates(int value)
        {
            ConfigurationManager.TimeBetweenUpdates = value;
        }

        public int GetTimeBetweenUpdates()
        {
            return ConfigurationManager.TimeBetweenUpdates;
        }

        public void InitBackgroundWorker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_Complete;

            worker.RunWorkerAsync();
        }

        public void DownloadFullCSV()
        {
            mainWindow.progressBarLabel.Content = "Downloading Full Data Set...";
            updater.DownloadFullCSV();
            InitBackgroundWorker();
        }

        public void DownloadUpdateFile()
        {
            mainWindow.progressBarLabel.Content = "Downloading Latest Update File...";
            updater.DownloadLatestUpdateFile();
            InitBackgroundWorker();
        }

        public void DownloadDeactivationFile()
        {
            mainWindow.progressBarLabel.Content = "Downloading Latest Deactivation File...";
            updater.DownloadLatestDeactivationFile();
            InitBackgroundWorker();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(updater.IsDownloading())
            {
                (sender as BackgroundWorker).ReportProgress(updater.GetProgressValue());
                Thread.Sleep(100);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mainWindow.progressBar.Value = e.ProgressPercentage;
        }

        private void worker_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            mainWindow.progressBarLabel.Content = "Download Complete!";
            mainWindow.progressBar.Value = 0;
        }
    }
}
