using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Architecture_Reminder.Annotations;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.Managers;

namespace Architecture_Reminder.ViewModels
{
    internal class ReminderConfigurationViewModel : INotifyPropertyChanged
    {
        #region Fields

        private readonly Reminder _currentReminder;
        private string[] _hours;
        private string[] _minutes;

        #endregion

        #region Properties


        public DateTime RemDate
        {
            get { return _currentReminder.RemDate.Date; }
            set
            {
                if (((_currentReminder.RemTimeHour >= DateTime.Now.Hour && _currentReminder.RemTimeMin > DateTime.Now.Minute)  
                     && value == DateTime.Today) ||
                    value > DateTime.Today)
                {
                    _currentReminder.RemDate = value;
                    OnPropertyChanged();
                }

                DBManager.SaveReminder(_currentReminder);
               
            }
        }

        public int RemTimeHours
        {
            get { return _currentReminder.RemTimeHour; }
            set
            {
               if ((((value == DateTime.Now.Hour && _currentReminder.RemTimeMin > DateTime.Now.Minute )
                                       || (value > DateTime.Now.Hour ) ) && _currentReminder.RemDate == DateTime.Today) || _currentReminder.RemDate > DateTime.Today)
               { 

                    _currentReminder.RemTimeHour = value;
                    OnPropertyChanged();
                }
               else
               {
                   _currentReminder.RemTimeHour = _currentReminder.RemTimeHour;
                   OnPropertyChanged();
                }
                DBManager.SaveReminder(_currentReminder);
            }
        }
        public int RemTimeMinutes
        {
            get { return _currentReminder.RemTimeMin; }
            set
            {
                var oldTime = _currentReminder.RemTimeMin;
                if(((( _currentReminder.RemTimeHour == DateTime.Now.Hour && value > DateTime.Now.Minute) || (_currentReminder.RemTimeHour > DateTime.Now.Hour))
                   && _currentReminder.RemDate == DateTime.Today) || _currentReminder.RemDate > DateTime.Today)
                {
                    _currentReminder.RemTimeMin = value;
                    OnPropertyChanged();
                }
               else
               {
                   _currentReminder.RemTimeMin = oldTime;
                   OnPropertyChanged();
                }
                DBManager.SaveReminder(_currentReminder);
            }
        }

        public string RemText
        {
            get { return _currentReminder.RemText; }
            set
            {
                _currentReminder.RemText = value;
                DBManager.SaveReminder(_currentReminder);
                OnPropertyChanged();
            }
        }
        public string[] FillHours
        {
            get
            {
                _hours = new string[24];
                for (int i = 0; i < _hours.Length; i++)
                {
                    if (i < 10) _hours[i] = "0" + i;
                    else _hours[i] = i + "";
                }

                return _hours;
            }

        }
        public string[] FillMinutes
        {
            get
            {
                _minutes = new string[60];
                for (int i = 0; i < _minutes.Length; i++)
                {
                    if (i < 10) _minutes[i] = "0" + i;
                    else _minutes[i] = i + "";
                }

                return _minutes;
            }
        }


        #endregion

        #region Constructor

        public ReminderConfigurationViewModel(Reminder reminder)
        {
            _currentReminder = reminder;
        }
        
        #endregion
        
        #region EventsAndHandlers

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #endregion

    }
}