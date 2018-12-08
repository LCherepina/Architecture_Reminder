using System.Linq;
using System.Windows;
using Architecture_Reminder.ViewModels;
using System.Threading;
using System;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.Views.Reminder;

namespace Architecture_Reminder.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private int _countChildren;
        private MainViewViewModel _mainViewViewModel;
        private ReminderConfigurationView _currentReminderConfigurationView;

        public MainView()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            _mainViewViewModel = new MainViewViewModel();
            _mainViewViewModel.ReminderChanged += OnReminderChanged;
            DataContext = _mainViewViewModel;
        }

        private void OnReminderChanged(DBModels.Reminder reminder)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate {
                ListBoxMain.Items.Clear();
                _mainViewViewModel.Reminders.Sort();
                _countChildren = _mainViewViewModel.Reminders.Count;

                for (int i = 0; i < (_countChildren); i++)
                {
                    _currentReminderConfigurationView =
                        new ReminderConfigurationView(_mainViewViewModel.Reminders.ElementAt(i));
                    DBModels.Reminder currRem = new DBModels.Reminder(DateTime.Today.Date, DateTime.Now.Hour, DateTime.Now.Minute, "", new User("0", "0", "0", "0", "0"));
                    if (_mainViewViewModel.Reminders.ElementAt(i).CompareTo(currRem) <= 0)
                    {
                        _currentReminderConfigurationView.DatePicker.IsEnabled = false;
                        _currentReminderConfigurationView.ComboBoxHours.IsEnabled = false;
                        _currentReminderConfigurationView.ComboBoxMinutes.IsEnabled = false;
                        _currentReminderConfigurationView.Text.IsEnabled = false;
                    }
                    ListBoxMain.Items.Add(_currentReminderConfigurationView);
                }
            }));
        }

    }
}
