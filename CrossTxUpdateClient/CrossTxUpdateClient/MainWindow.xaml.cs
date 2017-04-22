using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.ComponentModel;

using CrossTxUpdateClient.UIControllers;
using CrossTxUpdateClient.Services;
using CrossTxUpdateClient.DB;
using System.Data;

namespace CrossTxUpdateClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserInterfaceController uiController;
        private DataTable linkTable;

        public MainWindow()
        {
            uiController = new UserInterfaceController(this);
            InitializeComponent();
            InitConfigurations();

            InitSysTrayIcon();

            ServiceManager.Start(linkTable);
        }

        private void updateClientLabel_Loaded(object sender, RoutedEventArgs e)
        {
            //Check if Auto Updates has been enabled by the user
            if (Configurations.ConfigurationManager.EnableAutoUpdates)
            {
                bool found = ServiceManager.CheckForNewUpdates();
                if (!found)
                {
                    ServiceManager.DownloadUpdateFile();
                }
            }

            //Check if Auto deactivations has been enabled by the user
            if (Configurations.ConfigurationManager.EnableAutoDeactivations)
            {
                bool found = ServiceManager.CheckForNewDeactivations();
                if (!found)
                {
                    ServiceManager.DownloadDeactivationFile();
                }
            }
        }

        private void InitSysTrayIcon()
        {
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = Properties.Resources.AppIcon;
            ni.Visible = true;

            ni.DoubleClick += delegate (object sender, EventArgs args)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            };

            System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem showAppItem = new System.Windows.Forms.MenuItem();
            System.Windows.Forms.MenuItem exitAppItem = new System.Windows.Forms.MenuItem();

            showAppItem.Index = 0;
            showAppItem.Text = "Show App";
            showAppItem.Click += delegate (object sender, EventArgs e)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            };

            exitAppItem.Index = 1;
            exitAppItem.Text = "Exit";
            exitAppItem.Click += delegate (object sender, EventArgs e)
            {
                Environment.Exit(1);
            };

            menu.MenuItems.Add(showAppItem);
            menu.MenuItems.Add(exitAppItem);

            ni.ContextMenu = menu;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
            base.OnClosing(e);
        }

        private void InitConfigurations()
        {
            this.checkBoxEnableAutoUpdates.IsChecked = uiController.GetEnableAutoUpdatesConfig();
            this.checkBoxEnableAutoDeactivations.IsChecked = uiController.GetEnableAutoDeactivationsConfig();
            this.updateIntegerUpDown.Value = uiController.GetTimeBetweenUpdates();
            this.checkBoxStartOnBoot.IsChecked = uiController.GetBoot();

            string server = null;
            string db = null;
            string user = null;
            string pass = null;

            uiController.GetDBSettings(ref server, ref db, ref user, ref pass);

            if(server != null && server != null && server != null && server != null)
            {
                this.textBoxServer.Text = server;
                this.textBoxDatabase.Text = db;
                this.textBoxUsername.Text = user;
                this.passwordBox.Password = pass;
            }

            if(linkTable != null)
            {
                this.dataGrid.DataContext = linkTable.DefaultView;
            }
        }

        public void BindDataGrid(List<LinkObject> objects)
        {
            if(objects != null)
            {
                DataTable table = new DataTable();
                table.Columns.Add("Link");
                table.Columns.Add("Date");
                table.Columns.Add("Type");
                foreach (LinkObject obj in objects)
                {
                    table.Rows.Add(obj.Link, obj.Date, obj.Type);
                }

                linkTable = table;
            }  
        }

        private void checkBoxEnableAutoUpdates_Checked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoUpdatesConfig((bool)this.checkBoxEnableAutoUpdates.IsChecked);
        }

        private void checkBoxEnableAutoUpdates_Unchecked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoUpdatesConfig((bool)this.checkBoxEnableAutoUpdates.IsChecked);
        }

        private void checkBoxEnableAutoUpdates_Click(object sender, RoutedEventArgs e)
        {
            if (this.checkBoxEnableAutoUpdates.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Please restart software for the automatic updates to go into effect.");
            }
        }

        private void checkBoxEnableAutoDeactivations_Checked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoDeactivationsConfig((bool)this.checkBoxEnableAutoDeactivations.IsChecked);
        }

        private void checkBoxEnableAutoDeactivations_Unchecked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoDeactivationsConfig((bool)this.checkBoxEnableAutoDeactivations.IsChecked);
        }

        private void checkBoxEnableAutoDeactivations_Click(object sender, RoutedEventArgs e)
        {
            if(this.checkBoxEnableAutoDeactivations.IsChecked == true)
            {
                System.Windows.MessageBox.Show("Please restart software for the automatic deactivations to go into effect.");
            }
        }

        private void updateIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            uiController.SetTimeBetweenUpdates((int)this.updateIntegerUpDown.Value);
        }

        private void buttonManualCSV_Click(object sender, RoutedEventArgs e)
        {
            uiController.DownloadFullCSV();
        }

        private void buttonManualUpdate_Click(object sender, RoutedEventArgs e)
        {
            uiController.DownloadUpdateFile();
        }

        private void buttonManualDeactivation_Click(object sender, RoutedEventArgs e)
        {
            uiController.DownloadDeactivationFile();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceManager.Start(linkTable);
        }

        private void checkBoxStartOnBoot_Checked(object sender, RoutedEventArgs e)
        {
            uiController.SetAsBoot((bool)this.checkBoxStartOnBoot.IsChecked);
        }

        private void checkBoxStartOnBoot_Unchecked(object sender, RoutedEventArgs e)
        {
            uiController.SetAsBoot((bool)this.checkBoxStartOnBoot.IsChecked);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            uiController.SetDBSettings(this.textBoxServer.Text, this.textBoxDatabase.Text, this.textBoxUsername.Text, this.passwordBox.Password);
            System.Windows.MessageBox.Show("DB Settings Saved. Restart software to reconnect to the database.");
        }
    }
}
