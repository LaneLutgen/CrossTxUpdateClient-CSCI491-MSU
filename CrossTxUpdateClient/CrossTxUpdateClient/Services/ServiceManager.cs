using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hardcodet.Wpf.TaskbarNotification;
using System.Timers;

namespace CrossTxUpdateClient.Services
{
    /// <summary>
    /// This class will be responsible for running the background process for automatic updates
    /// </summary>
    public static class ServiceManager
    {
        private static Timer timer;

        public static void Start()
        {
            //24 hour timer (final implementation)
            //timer = new Timer(24 * 60 * 60 * 1000);

            //5 minutes for testing purposes
            timer = new Timer(5 * 60 * 1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //Code to check for new links
        }

        public static void Stop()
        {
            timer.Stop();
            timer = null;
        }
    }
}
