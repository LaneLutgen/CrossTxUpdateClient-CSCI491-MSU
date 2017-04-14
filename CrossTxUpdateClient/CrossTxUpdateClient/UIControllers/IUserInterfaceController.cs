using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossTxUpdateClient.UIControllers
{
    /// <summary>
    /// Used for passing a controller to the UI
    /// </summary>
    public interface IUserInterfaceController
    {
        //Define all the methods that will need to interface with the UI
        void SetEnableAutoUpdatesConfig(bool value);

        bool GetEnableAutoUpdatesConfig();

        void SetEnableAutoDeactivationsConfig(bool value);

        bool GetEnableAutoDeactivationsConfig();

        void SetTimeBetweenUpdates(int value);

        int GetTimeBetweenUpdates();

        void SetTimeBetweenDeactivations(int value);

        int GetTimeBetweenDeactivations();

        void DownloadFullCSV();

        void DownloadUpdateFile();

        void DownloadDeactivationFile();

        void SetAsBoot(bool value);
    }
}
