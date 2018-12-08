using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Architecture_Reminder.Annotations;
using Architecture_Reminder.DBModels;

namespace Architecture_Reminder.Models
{
    public class ReminderUIModel : INotifyPropertyChanged, IComparable<ReminderUIModel>
    {
        #region Fields
        private Reminder _reminder;
        #endregion

        #region Properties
        internal Reminder Reminder
        {
            get { return _reminder; }
            set
            {
                _reminder = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime Date
        {
            get { return _reminder.RemDate; }
            set
            {
                _reminder.RemDate = value;
                OnPropertyChanged();
            }
        }
        public int TimeMin
        {
            get { return _reminder.RemTimeMin; }
            set
            {
                _reminder.RemTimeMin = value;
                OnPropertyChanged();
            }
        }
        public int TimeHour
        {
            get { return _reminder.RemTimeHour; }
            set
            {
                _reminder.RemTimeHour = value;
                OnPropertyChanged();
            }

        }

        public string Text
        {
            get { return _reminder.RemText;}
            set {
                _reminder.RemText = value;
                OnPropertyChanged();
            }

        }

        public Guid Guid
        {
            get { return _reminder.Guid; }
        }

        #endregion

        public ReminderUIModel(Reminder reminder)
        {
            _reminder = reminder;
        }

        #region EventsAndHandlers
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(ReminderUIModel other)
        {
            return Reminder.CompareTo(other.Reminder);
        }
        #endregion
        #endregion
    }

}
