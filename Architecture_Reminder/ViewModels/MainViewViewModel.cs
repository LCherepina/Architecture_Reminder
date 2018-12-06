using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Architecture_Reminder.Tools;
using System.Threading;
using System.Threading.Tasks;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.Managers;
using Architecture_Reminder.Annotations;

namespace Architecture_Reminder.ViewModels
{
    class MainViewViewModel : INotifyPropertyChanged
    {
        #region Fields

        private int _indexSelected;
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

        public int SelectedReminderIndex
        {
            get { return _indexSelected; }
            set
            {
                _indexSelected = value;

            }
        }

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
            var result = await Task.Run(() =>
            {
                Reminder reminder = new Reminder(DateTime.Today.Date, DateTime.Now.Hour + 1, DateTime.Now.Minute, "",
                    StationManager.CurrentUser);
                Reminders.Add(reminder);
                SelectedReminder = reminder;
                DBManager.AddReminder(reminder);
                //Reminders.Sort();
                RunReminderExecute(reminder.Guid);
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
        }

        private async void DeleteReminderExecute(KeyEventArgs args)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                if (Reminders.Count == 0) return false;
                if (SelectedReminderIndex < 0) return false;
                DBManager.DeleteReminder(Reminders.ElementAt(SelectedReminderIndex));
                Reminders.RemoveAt(SelectedReminderIndex);
                //Reminders.Sort();
                return true;
            });
            LoaderManager.Instance.HideLoader();
            OnPropertyChanged();
        }

        private async void LogOutExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Thread.Sleep(100);
                StationManager.CurrentUser = null;
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
            {
                foreach (Thread t in _myThreads)
                    t.Abort();
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
        
        /*
        private void DeleteReminderByGuid(Guid g)
        {
            Reminder r = GetReminderByGuid(g);
            if (r != null)
            {
                int i = -1;
                int n = -1;
                foreach (var rem in Reminders)
                {
                    n++;
                    if (rem.Guid == g)
                    {
                        i = n;
                        break;
                    }
                }
                Reminders.RemoveAt(i);
                DBManager.DeleteReminder(r);
            }
            OnPropertyChanged(); 
        }*/


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
                    MessageBox.Show(r.RemTimeHour + " : " + r.RemTimeMin + "\n" +
                        + r.RemDate.Day + "." + r.RemDate.Month + "." + r.RemDate.Year + "\n " + r.RemText );
                    GetReminderByGuid((Guid)g).IsHappened = true;
                    //DeleteReminderByGuid((Guid)g);
                    OnPropertyChanged();
                    return;
                }
                else if (r.RemDate < DateTime.Today.Date || (r.RemDate == DateTime.Today.Date && r.RemTimeHour < DateTime.Now.Hour)
               || (r.RemDate == DateTime.Today.Date && r.RemTimeHour == DateTime.Now.Hour && r.RemTimeMin < DateTime.Now.Minute))
                {
                    GetReminderByGuid((Guid)g).IsHappened = true;
                    OnPropertyChanged();
                    //DeleteReminderByGuid((Guid)g);
                    return;
                }
                Thread.Sleep(1000);
            }
        }

        /*public void SaveRemindersToDB()
        {
            foreach (Reminder r in Reminders)
                DBManager.SaveReminder(r);
        }*/


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
 