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
            updater = new Updater(this);
        }
        //covered
        public void SetEnableAutoDeactivationsConfig(bool value)
        {
            ConfigurationManager.EnableAutoDeactivations = value;
        }
        //covered
        public bool GetEnableAutoDeactivationsConfig()
        {
            return ConfigurationManager.EnableAutoDeactivations;
        }
        //covered
        public void SetEnableAutoUpdatesConfig(bool value)
        {
            ConfigurationManager.EnableAutoUpdates = value;
        }
        //covered
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

        public void StartDownloadAsync()
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
            if(updater.DownloadFullCSV())
            {
                StartDownloadAsync();
                updater.AddToDB();
            }
            else
            {
                ClearProgressLabel();
                MessageBox.Show("Full Data set was not found.");
            }
        }

        public void DownloadUpdateFile()
        {
            mainWindow.progressBarLabel.Content = "Downloading Latest Update File...";
            if (updater.DownloadLatestUpdateFile())
            {
                StartDownloadAsync();
                updater.UpdateDB();
            }
            else
            {
                ClearProgressLabel();
                MessageBox.Show("No Update Files found.");
            }
        }

        public void DownloadDeactivationFile()
        {
            mainWindow.progressBarLabel.Content = "Downloading Latest Deactivation File...";
            if(updater.DownloadLatestDeactivationFile())
            {
                StartDownloadAsync();
                updater.RemoveFromDB();
            }
            else
            {
                ClearProgressLabel();
                MessageBox.Show("No Deactivation Files found.");
            }
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
            mainWindow.progressBarLabel.Content = "Extracting File Contents. Please Wait...";
            mainWindow.progressBar.Value = 0;
            updater.UnzipFileAsync();
        }

        public void SetProgressLabelValue(string value)
        {
            mainWindow.progressBarLabel.Content = value;
        }

        public bool GetBoot()
        {
            return ConfigurationManager.IsBootupSequence;
        }

        public void SetAsBoot(bool value)
        {
            ConfigurationManager.IsBootupSequence = value;
        }

        private void ClearProgressLabel()
        {
            mainWindow.progressBarLabel.Content = "";
        }
    }
}
