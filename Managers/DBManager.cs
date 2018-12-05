using Architecture_Reminder.DBModels;
using Architecture_Reminder.ServiceInterface;

namespace Architecture_Reminder.Managers
{
    public class DBManager
    {

        public static bool UserExists(string login)
        {
            return ReminderServiceWrapper.UserExists(login);
        }

        public static User GetUserByLogin(string login)
        {
            return ReminderServiceWrapper.GetUserByLogin(login);
        }

        public static void AddUser(User user)
        {
            ReminderServiceWrapper.AddUser(user);
        }

        internal static User CheckCachedUser(User userCandidate)
        {
            var userInStorage = ReminderServiceWrapper.GetUserByGuid(userCandidate.Guid);
            if (userInStorage != null && userInStorage.CheckPassword(userCandidate))
                return userInStorage;
            return null;
        }

        public static void DeleteReminder(Reminder selectedReminder)
        {
            ReminderServiceWrapper.DeleteReminder(selectedReminder);
        }

        public static void AddReminder(Reminder reminder)
        {
            ReminderServiceWrapper.AddReminder(reminder);
        }

        public static void SaveReminder(Reminder reminder)
        {
            ReminderServiceWrapper.SaveReminder(reminder);
        }

    }
}
