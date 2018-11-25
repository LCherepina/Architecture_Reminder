using System;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace Architecture_Reminder.DBModels
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Reminder : IComparable<Reminder>
    {
        #region Fields
        [DataMember]
        private Guid _guid;
        [DataMember]
        private Guid _userGuid;
        [DataMember]
        private DateTime _dateTime;
        [DataMember]
        private int _minutes;
        [DataMember]
        private int _hours;
        [DataMember]
        private string _text;
        [DataMember]
        private User _user;
        #endregion

        #region Properties
        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }

        public DateTime RemDate
        {
            get { return _dateTime.Date; }
            set { _dateTime = value; }
        }
        public int RemTimeHour
        {
            get { return _hours; }
            set { _hours = value; }
        }
        public int RemTimeMin
        {
            get { return _minutes; }
            set { _minutes = value; }
        }
        public string RemText
        {
            get { return _text; }
            set { _text = value; }
        }

        public User User
        {
            get { return _user; }
            private set { _user = value; }
        }
        public Guid UserGuid
        {
            get { return _userGuid; }
            private set { _userGuid = value; }
        }
        #endregion

        #region Constructor
        //    public Reminder(DateTime dateTime, string text, User user)
        public Reminder(DateTime dateTime, int hours, int minutes, string text, User user)
        {
            _guid = Guid.NewGuid();
            _dateTime = dateTime.Date;
            _hours = hours;
            _minutes = minutes;
            _text = text;
            _user = user;
            _userGuid = user.Guid;
            user.Reminders.Add(this);
            user.Reminders.Sort();
        }

        private Reminder() { }
        #endregion

        public int CompareTo(Reminder other)
        {
            if (RemDate > other.RemDate)
                return 1;
            else if (RemDate < other.RemDate)
                return -1;

            if (RemTimeHour > other.RemTimeHour)
            {
                return 1; 
            }
            else if (RemTimeHour < other.RemTimeHour)
                return -1;

            if (RemTimeMin > other.RemTimeMin)
                return 1;
            else if (RemTimeMin < other.RemTimeMin)
                return -1;

            return 0;
        }
        #region EntityFrameworkConfiguration
        public class ReminderEntityConfiguration : EntityTypeConfiguration<Reminder>
        {
            public ReminderEntityConfiguration()
            {
                ToTable("Reminder");
                HasKey(s => s.Guid);

                Property(p => p.UserGuid)
                    .HasColumnName("UserGuid")
                    .IsRequired();
                Property(p => p.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(p => p.RemDate)
                    .HasColumnName("RemDate")
                    .IsRequired();
                Property(s => s.RemTimeHour)
                    .HasColumnName("RemTimeHour")
                    .IsRequired();
                Property(s => s.RemTimeMin)
                    .HasColumnName("RemTimeMin")
                    .IsRequired();
                Property(s => s.RemText)
                    .HasColumnName("RemText")
                    .IsRequired();
            }
        }
        #endregion

        public void DeleteDatabaseValues()
        {
            _user = null;
        }
    }
}