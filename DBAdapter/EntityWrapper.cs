using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Architecture_Reminder.DBModels;


namespace Architecture_Reminder.DBAdapter
{
    public static class EntityWrapper
    {
        public static bool UserExists(string login)
        {
            using (var context = new ReminderDBContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        public static User GetUserByLogin(string login)
        {
            using (var context = new ReminderDBContext())
            {
                return context.Users.Include(u => u.Reminders).FirstOrDefault(u => u.Login == login);
            }
        }

        public static User GetUserByGuid(Guid guid)
        {
            using (var context = new ReminderDBContext())
            {
                return context.Users.Include(u => u.Reminders).FirstOrDefault(u => u.Guid == guid);
            }
        }

        public static List<User> GetAllUsers(Guid reminderGuid)
        {
            using (var context = new ReminderDBContext())
            {
                return context.Users.Where(u => u.Reminders.All(r => r.Guid != reminderGuid)).ToList();
            }
        }

        public static void AddUser(User user)
        {
            using (var context = new ReminderDBContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void AddReminder(Reminder reminder)
        {
            using (var context = new ReminderDBContext())
            {
                reminder.DeleteDatabaseValues();
                context.Reminders.Add(reminder);
                context.SaveChanges();
            }
        }

        public static void SaveReminder(Reminder reminder)
        {
            using (var context = new ReminderDBContext())
            {
                context.Entry(reminder).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        
        public static void DeleteReminder(Reminder selectedReminder)
        {
            using (var context = new ReminderDBContext())
            {
                selectedReminder.DeleteDatabaseValues();
                context.Reminders.Attach(selectedReminder);
                context.Reminders.Remove(selectedReminder);
                context.SaveChanges();
            }
        }
    }
}
