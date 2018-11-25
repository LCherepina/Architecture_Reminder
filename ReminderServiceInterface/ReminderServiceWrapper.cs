using System;
using System.Collections.Generic;
using System.ServiceModel;
using Architecture_Reminder.DBModels;

namespace Architecture_Reminder.ServiceInterface
{
       public class ReminderServiceWrapper
    {
        public static bool UserExists(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                return client.UserExists(login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                return client.GetUserByLogin(login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                return client.GetUserByGuid(guid);
            }
        }

        public static void AddUser(User user)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                client.AddUser(user);
            }
        }

        public static void AddReminder(Reminder reminder)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                client.AddReminder(reminder);
            }
        }

        public static void SaveReminder(Reminder reminder)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                client.SaveReminder(reminder);
            }
        }

        public static List<User> GetAllUsers(Guid reminderGuid)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                return client.GetAllUsers(reminderGuid);
            }
        }

        public static void DeleteReminder(Reminder selectedReminder)
        {
            using (var myChannelFactory = new ChannelFactory<IReminderContract>("Server"))
            {
                IReminderContract client = myChannelFactory.CreateChannel();
                client.DeleteReminder(selectedReminder);
            }
        }
    }
}
