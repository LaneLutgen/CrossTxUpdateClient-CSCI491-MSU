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


namespace CrossTxUpdateClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserInterfaceController uiController;

        public MainWindow()
        {
            uiController = new UserInterfaceController(this);
            InitializeComponent();
            InitConfigurations();

            InitSysTrayIcon();
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
            this.deactivationUpDown.Value = uiController.GetTimeBetweenDeactivations();
        }

        private void checkBoxEnableAutoUpdates_Checked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoUpdatesConfig((bool)this.checkBoxEnableAutoUpdates.IsChecked);
        }

        private void checkBoxEnableAutoUpdates_Unchecked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoUpdatesConfig((bool)this.checkBoxEnableAutoUpdates.IsChecked);
        }


        private void checkBoxEnableAutoDeactivations_Checked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoDeactivationsConfig((bool)this.checkBoxEnableAutoDeactivations.IsChecked);
        }

        private void checkBoxEnableAutoDeactivations_Unchecked(object sender, RoutedEventArgs e)
        {
            uiController.SetEnableAutoDeactivationsConfig((bool)this.checkBoxEnableAutoDeactivations.IsChecked);
        }

        private void updateIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            uiController.SetTimeBetweenUpdates((int)this.updateIntegerUpDown.Value);
        }

        private void deactivationUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            uiController.SetTimeBetweenDeactivations((int)this.deactivationUpDown.Value);
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
            ServiceManager.Start();
        }
    }
}
