using System;
using System.Collections.Generic;
using Architecture_Reminder.DBAdapter;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.ServiceInterface;


namespace Architecture_Reminder.ReminderService
{
    class ReminderSimulatorService : IReminderContract
    {
        public bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        public User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        public User GetUserByGuid(Guid guid)
        {
            return EntityWrapper.GetUserByGuid(guid);
        }

        public void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        public void AddReminder(Reminder reminder)
        {
            EntityWrapper.AddReminder(reminder);
        }

        public List<User> GetAllUsers(Guid reminderGuid)
        {
            return EntityWrapper.GetAllUsers(reminderGuid);
        }

        public void DeleteReminder(Reminder selectedReminder)
        {
            EntityWrapper.DeleteReminder(selectedReminder);
        }


        public void SaveReminder(Reminder reminder)
        {
           EntityWrapper.SaveReminder(reminder);
        }
    }
}
