using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;
using System.Timers;
using System.Data;
using CrossTxUpdateClient.UpdateAPI;
using System.Windows;

namespace CrossTxUpdateClient.Services
{
    /// <summary>
    /// This class will be responsible for running the background process for automatic updates
    /// </summary>
    public static class ServiceManager
    {
        private static Timer timer;
        private static DataTable linkTable;

        public static void Start(DataTable pastLinks)
        {
            linkTable = pastLinks;

            //24 hour timer (final implementation)
            //timer = new Timer(24 * 60 * 60 * 1000);

            //5 minutes for testing purposes
            timer = new Timer(Configurations.ConfigurationManager.TimeBetweenUpdates * 60 * 1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Start();
        }

        public static bool CheckForNewUpdates()
        {
            bool found = false;

            DownloadManager mngr = DownloadManager.Instance;

            //If the Download Manager hasn't been initialized by this point, that's an issue
            if(mngr != null)
            {
                mngr.ParseHTMLForLatestLinks();

                string link = DownloadManager.UpdateURL;

                //Check if the link pulled from the HTML is already in the database
                foreach(DataRow row in linkTable.Rows)
                {
                    if(row["Link"].Equals(link) && row["Type"].Equals("Update"))
                    {
                        found = true;
                    }
                }

            }

            return found;
        }

        public static bool CheckForNewDeactivations()
        {
            bool found = false;

            DownloadManager mngr = DownloadManager.Instance;

            //If the Download Manager hasn't been initialized by this point, that's an issue
            if (mngr != null)
            {
                mngr.ParseHTMLForLatestLinks();

                string link = DownloadManager.DeactivationURL;

                //Check if the link pulled from the HTML is already in the database
                foreach (DataRow row in linkTable.Rows)
                {
                    if (row["Link"].Equals(link) && row["Type"].Equals("Deactivation"))
                    {
                        found = true;
                    }
                }
            }

            return found;
        }

        public static void DownloadUpdateFile()
        {
            Updater updater = Updater.Instance;

            updater.Controller.MainUI.progressBarLabel.Content = "Auto downloading newest update file...";
            if (updater.DownloadLatestUpdateFile())
            {
                updater.Controller.StartDownloadAsync();
            }
            else
            {
                updater.Controller.ClearProgressLabel();
                MessageBox.Show("No Update Files found.");
            }
        }

        public static void DownloadDeactivationFile()
        {
            Updater updater = Updater.Instance;

            updater.Controller.MainUI.progressBarLabel.Content = "Downloading Latest Deactivation File...";
            if (updater.DownloadLatestDeactivationFile())
            {
                updater.Controller.StartDownloadAsync();
            }
            else
            {
                updater.Controller.ClearProgressLabel();
                MessageBox.Show("No Deactivation Files found.");
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timed event was called successfully");

            //Check if Auto Updates has been enabled by the user
            if (Configurations.ConfigurationManager.EnableAutoUpdates)
            {
                bool found = CheckForNewUpdates();
                if(!found)
                {
                    DownloadUpdateFile();
                }
            }

            //Check if Auto deactivations has been enabled by the user
            if (Configurations.ConfigurationManager.EnableAutoDeactivations)
            {
                bool found = CheckForNewDeactivations();
                if(!found)
                {
                    DownloadDeactivationFile();
                }
            }
        }

        public static void Stop()
        {
            timer.Stop();
            timer = null;
        }
    }
}
