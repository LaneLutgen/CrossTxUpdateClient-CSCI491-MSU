using System;
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

using CrossTxUpdateClient.UIControllers;

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
            uiController = new UserInterfaceController();
            InitializeComponent();
            InitConfigurations();
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
    }
}
