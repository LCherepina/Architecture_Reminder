using System;
using System.ServiceModel;
using System.ServiceProcess;
using Architecture_Reminder.Tools;

namespace Architecture_Reminder.ReminderService
{
    partial class ReminderSimulatorWindowsService : ServiceBase
    {
        internal const string CurrentServiceName = "ReminderSimulatorService1";
        internal const string CurrentServiceDisplayName = "Reminder Simulator Service1";
        internal const string CurrentServiceSource = "ReminderSimulatorServiceSource1";
        internal const string CurrentServiceLogName = "ReminderSimulatorServiceLogName1";
        internal const string CurrentServiceDescription = "Reminder Simulator for learning purposes1.";
        private ServiceHost _serviceHost = null;

        public ReminderSimulatorWindowsService()
        {
            ServiceName = CurrentServiceName;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;
                Logger.Log("Initialization");
            }
            catch (Exception ex)
            {
                Logger.Log("Initialization", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            RequestAdditionalTime(120 * 1000);
#if DEBUG
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1000);
            //}
#endif
            try
            {
                if (_serviceHost != null)
                    _serviceHost.Close();
            }
            catch
            {
            }
            try
            {
                _serviceHost = new ServiceHost(typeof(ReminderSimulatorService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                Logger.Log("OnStart", ex);
                throw;
            }
            Logger.Log("Service Started");
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("Trying To Stop The Host Listener", ex);
            }
            Logger.Log("Service Stopped");
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;

            Logger.Log("UnhandledException", ex);
        }
    }
}
