
using System.Data.Entity;
using Architecture_Reminder.DBModels;
using Architecture_Reminder.Migrations;

namespace Architecture_Reminder.DBAdapter
{
    internal class ReminderDBContext : DbContext
    {
        public ReminderDBContext() : base("ReminderDB")//base("NewReminderDB")//
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReminderDBContext, Configuration>(true));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Reminder.ReminderEntityConfiguration());
        }
    }
}
