namespace Architecture_Reminder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReminderDBMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reminder",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        RemDate = c.DateTime(nullable: false),
                        RemTimeHour = c.Int(nullable: false),
                        RemTimeMin = c.Int(nullable: false),
                        RemText = c.String(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reminder", "UserGuid", "dbo.Users");
            DropIndex("dbo.Reminder", new[] { "UserGuid" });
            DropTable("dbo.Users");
            DropTable("dbo.Reminder");
        }
    }
}
