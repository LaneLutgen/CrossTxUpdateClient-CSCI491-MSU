using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.Configurations
{
    /// <summary>
    /// This class will work with the project properties to save and load configurations stored in the app data
    /// </summary>
    public static class ConfigurationManager
    {
        //covered
        public static bool EnableAutoUpdates
        {
            get
            {
                return Properties.Settings.Default.EnableAutoUpdates;
            }
            set
            {
                Properties.Settings.Default.EnableAutoUpdates = value;
                SaveSettings();
            }
        }
        //covered
        public static bool EnableAutoDeactivations
        {
            get
            {
                return Properties.Settings.Default.EnableAutoDeactivations;
            }
            set
            {
                Properties.Settings.Default.EnableAutoDeactivations = value;
                SaveSettings();
            }
        }
        //covered
        public static int TimeBetweenUpdates
        {
            get
            {
                return Properties.Settings.Default.TimeBetweenUpdates;
            }
            set
            {
                Properties.Settings.Default.TimeBetweenUpdates = value;
                SaveSettings();
            }
        }
        //covered
        public static int TimeBetweenDeactivations
        {
            get
            {
                return Properties.Settings.Default.TimeBetweenDeactivations;
            }
            set
            {
                Properties.Settings.Default.TimeBetweenDeactivations = value;
                SaveSettings();
            }
        }
        //covered, unsure about test
        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
