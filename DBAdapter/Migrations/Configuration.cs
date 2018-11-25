
using System.Data.Entity.Migrations;
using Architecture_Reminder.DBAdapter;

namespace Architecture_Reminder.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<ReminderDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReminderDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
