﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.Configurations;
using CrossTxUpdateClient.UpdateAPI;

namespace CrossTxUpdateClient.UIControllers
{
    /// <summary>
    /// The controller passed to the UI
    /// </summary>
    public class UserInterfaceController : IUserInterfaceController
    {
        private Updater updater;

        public UserInterfaceController()
        {
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

        public void DownloadFullCSV()
        {
            updater.DownloadFullCSV();
        }

        public void DownloadUpdateFile()
        {
            updater.DownloadLatestUpdateFile();
        }

        public void DownloadDeactivationFile()
        {
            updater.DownloadLatestDeactivationFile();
        }
    }
}
