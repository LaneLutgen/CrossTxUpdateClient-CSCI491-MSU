using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossTxUpdateClient.Configurations;

namespace CrossTxUpdateClient.UIControllers
{
    /// <summary>
    /// The controller passed to the UI
    /// </summary>
    public class UserInterfaceController : IUserInterfaceController
    {
        private ConfigurationManager configManager;

        public UserInterfaceController()
        {
            configManager = new ConfigurationManager();
        }

        public void SetEnableAutoDeactivationsConfig(bool value)
        {
            configManager.EnableAutoDeactivations = value;
        }

        public void SetEnableAutoUpdatesConfig(bool value)
        {
            configManager.EnableAutoUpdates = value;
        }

        public void SetTimeBetweenDeactivations(int value)
        {
            configManager.TimeBetweenDeactivations = value;
        }

        public void SetTimeBetweenUpdates(int value)
        {
            configManager.TimeBetweenUpdates = value;
        }
    }
}
