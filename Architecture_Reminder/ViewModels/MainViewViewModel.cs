using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Architecture_Reminder.Tools;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.Managers;
using Architecture_Reminder.Annotations;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace Architecture_Reminder.ViewModels
{
    class MainViewViewModel : INotifyPropertyChanged
    {

        #region Fields
        private Reminder _selectedReminder;
        private List<Reminder> _reminders;
        private List<Thread> _myThreads;

        #region Commands
        private ICommand _addReminderCommand;
        private ICommand _deleteReminderCommand;
        private ICommand _sortReminderCommand;
        private ICommand _logOutCommand;
        #endregion
        #endregion

        #region Properties
        #region Commands

        public ICommand AddReminderCommand
        {
            get
            {
                return _addReminderCommand ?? (_addReminderCommand = new RelayCommand<object>(AddReminderExecute));
            }
        }
        public ICommand DeleteReminderCommand
        {
            get
            {
                return _deleteReminderCommand ?? (_deleteReminderCommand = new RelayCommand<KeyEventArgs>(DeleteReminderExecute));
            }
        }
        
        public ICommand SortReminderCommand
        {
            get
            {
                return _sortReminderCommand ?? (_sortReminderCommand = new RelayCommand<object>(SortReminderExecute));
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand = new RelayCommand<object>(LogOutExecute));
            }
        }

        #endregion

        public List<Reminder> Reminders
        {
            get
            {
                return _reminders;
                //return StationManager.CurrentUser.Reminders;
            }
            set
            {
                _reminders = value;
                //StationManager.CurrentUser.Reminders = value;
            }
        }

        public int SelectedReminderIndex { get; set; }

        public Reminder SelectedReminder { get { return _selectedReminder; } set { _selectedReminder = value; } }
        #endregion

        #region Constructor

        public MainViewViewModel()
        {
            FillReminders();
            PropertyChanged += OnPropertyChanged;
        }
        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if(SelectedReminder != null)
                OnReminderChanged(SelectedReminder);
        }

        private async void FillReminders()
        {
            _myThreads = new List<Thread>();
            var result = await Task.Run(() =>
            {
                Reminders = new List<Reminder>();
                Reminder curr_rem = new Reminder(DateTime.Today.Date, DateTime.Now.Hour, DateTime.Now.Minute, "", new User("0", "0", "0", "0", "0"));
                foreach(var rem in DBManager.GetUserByLogin(StationManager.CurrentUser.Login).Reminders)
                {
                    if (rem.CompareTo(curr_rem) < 0)
                        rem.IsHappened = true;

                    Reminders.Add(rem);
                    if (!rem.IsHappened)
                        RunReminderExecute(rem.Guid);
                }
                if (Reminders.Count != 0)
                    SelectedReminder = Reminders[0];
                
                return true;
            });
            OnPropertyChanged();
        }

        private async void SortReminderExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Reminders.Sort();
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
        }

        private async void AddReminderExecute(object o)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Reminder reminder;
                int hourNow = DateTime.Now.Hour;
                var today = DateTime.Today.Date;
                if (hourNow != 23)
                {
                    reminder = new Reminder(today, hourNow + 1, DateTime.Now.Minute, "Reminder",
                        StationManager.CurrentUser);
                }
                else
                {
                    reminder = new Reminder(today.AddDays(1), 00, DateTime.Now.Minute, "Reminder",
                        StationManager.CurrentUser);
                }
                Reminders.Add(reminder);
                SelectedReminder = reminder;
                DBManager.AddReminder(reminder);
                RunReminderExecute(reminder.Guid);
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
            Logger.Log("Created new reminder");
            

        }

        private async void DeleteReminderExecute(KeyEventArgs args)
        {
            LoaderManager.Instance.ShowLoader();
             await Task.Run(() =>
            {
                if (Reminders.Count == 0) return false;
                if (SelectedReminderIndex < 0) return false;
                DBManager.DeleteReminder(Reminders.ElementAt(SelectedReminderIndex));
                Reminders.RemoveAt(SelectedReminderIndex);
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
            Logger.Log("Delete reminder");
        }

        private async void LogOutExecute(object obj)
        {
            
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    if (System.IO.File.Exists(FileFolderHelper.LastUserFilePath))
                    {
                        System.IO.File.Delete(FileFolderHelper.LastUserFilePath);   
                    }
                   
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Log($"Update user exception ", ex);
                    return false;
                }
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                foreach (Thread t in _myThreads)
                    t.Abort();
                StationManager.CurrentUser = null;
              
                NavigationManager.Instance.Navigate(ModesEnum.SignIn);
            }
        }

        public Reminder GetReminderByGuid(Guid g)
        {
            foreach (var rem in Reminders)
            {
                if (rem.Guid == g)
                    return rem;
            }
            return null;
        }


        private void RunReminderExecute(Guid g)
        {
            Thread myThread = new Thread(new ParameterizedThreadStart(CheckIfRun));
            myThread.IsBackground = true;
            myThread.Start(g);
            _myThreads.Add(myThread);
        }

        
        private void CheckIfRun(Object g)
        {
            while (true)
            {
                Reminder r = GetReminderByGuid((Guid) g);
                if (r == null) return;
                
                if (r.RemDate == DateTime.Today.Date && r.RemTimeHour == DateTime.Now.Hour && r.RemTimeMin == DateTime.Now.Minute)
                {
                    string message = r.RemTimeHour + " : " + r.RemTimeMin + "                                     " +
                                     +r.RemDate.Day + "." + r.RemDate.Month + "." + r.RemDate.Year + "\n"+ "__________________________________________" + "\n" + "\n" + r.RemText;
                    string caption = "Reminder";
                    MessageBox.Show(message,caption,MessageBoxButton.OK);


                    GetReminderByGuid((Guid)g).IsHappened = true;
                    Logger.Log("Reminder happened");
                    OnPropertyChanged();
                    return;
                }
                else if (r.RemDate < DateTime.Today.Date || (r.RemDate == DateTime.Today.Date && r.RemTimeHour < DateTime.Now.Hour)
               || (r.RemDate == DateTime.Today.Date && r.RemTimeHour == DateTime.Now.Hour && r.RemTimeMin < DateTime.Now.Minute))
                {
                    GetReminderByGuid((Guid)g).IsHappened = true;
                    OnPropertyChanged();
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        #region EventsAndHandlers
        #region Loader
        internal event ReminderChangedHandler ReminderChanged;
        internal delegate void ReminderChangedHandler(Reminder reminder);

        internal virtual void OnReminderChanged(Reminder reminder)
        {
            ReminderChanged?.Invoke(reminder);
            //ReminderChanged(reminder);
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }



}
 