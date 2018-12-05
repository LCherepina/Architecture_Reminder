using System;
using System.Collections.Generic;
using System.Linq;

using Architecture_Reminder.ViewModels;
using Architecture_Reminder.Models;

namespace Architecture_Reminder.Views.Reminder
{
    /// <summary>
    /// Interaction logic for ReminderConfigurationView.xaml
    /// </summary>
    public partial class ReminderConfigurationView
    {
        #region Fields

        private int currentHour = DateTime.Now.Hour;
        private int currentMinute = DateTime.Now.Minute;
        //private int _id;
        #endregion
        public ReminderConfigurationView(DBModels.Reminder reminder)
        {
            InitializeComponent();
           var reminderModel = new ReminderConfigurationViewModel(reminder);
            DataContext = reminderModel;
        }

        private void TextBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {

        }
    }
}