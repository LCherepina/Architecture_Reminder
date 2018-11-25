
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Architecture_Reminder.ReminderService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
            private void InitializeComponent()
            {
                _serviceProcessInstaller = new ServiceProcessInstaller();
                _serviceInstaller = new ServiceInstaller();
                _serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
                _serviceProcessInstaller.Password = null;
                _serviceProcessInstaller.Username = null;
                _serviceInstaller.ServiceName = ReminderSimulatorWindowsService.CurrentServiceName;
                _serviceInstaller.DisplayName = ReminderSimulatorWindowsService.CurrentServiceDisplayName;
                _serviceInstaller.Description = ReminderSimulatorWindowsService.CurrentServiceDescription;
                _serviceInstaller.StartType = ServiceStartMode.Automatic;
                Installers.AddRange(new Installer[]
                {
                    _serviceProcessInstaller,
                    _serviceInstaller
                });
            }

            public ProjectInstaller()
            {
                InitializeComponent();
            }

            private ServiceProcessInstaller _serviceProcessInstaller;
            private ServiceInstaller _serviceInstaller;

            private void _serviceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void _serviceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
