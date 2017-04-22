using Microsoft.Win32;
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

        public static bool IsBootupSequence
        {
            get
            {
                return Properties.Settings.Default.Bootup;
            }
            set
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                //Yes, start on boot
                if(value)
                {
                    rk.SetValue("StartupWithWindows", System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
                //No, don't start on boot
                else
                {
                    rk.DeleteValue("StartupWithWindows", false);
                }
                Properties.Settings.Default.Bootup = value;
                SaveSettings();
            }
        }

        public static string ServerName
        {
            get
            {
                return Properties.Settings.Default.ServerName;
            }
            set
            {
                Properties.Settings.Default.ServerName = value;
                SaveSettings();
            }
        }

        public static string DBName
        {
            get
            {
                return Properties.Settings.Default.DatabaseName;
            }
            set
            {
                Properties.Settings.Default.DatabaseName = value;
                SaveSettings();
            }
        }

        public static string Username
        {
            get
            {
                return Properties.Settings.Default.UserName;
            }
            set
            {
                Properties.Settings.Default.UserName = value;
                SaveSettings();
            }
        }

        public static string Password
        {
            get
            {
                return Properties.Settings.Default.Password;
            }
            set
            {
                Properties.Settings.Default.Password = value;
                SaveSettings();
            }
        }

        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
