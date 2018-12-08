
using Architecture_Reminder.ViewModels;


namespace Architecture_Reminder.Views.Reminder
{
    /// <summary>
    /// Interaction logic for ReminderConfigurationView.xaml
    /// </summary>
    public partial class ReminderConfigurationView
    {
        public ReminderConfigurationView(DBModels.Reminder reminder)
        {
            InitializeComponent();
           var reminderModel = new ReminderConfigurationViewModel(reminder);
            DataContext = reminderModel;
        }
    }
}