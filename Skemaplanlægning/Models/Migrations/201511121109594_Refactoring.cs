namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Name", c => c.String());
            AddColumn("dbo.Courses", "Duration", c => c.Int(nullable: false));
            DropColumn("dbo.CourseInstances", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseInstances", "EndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Courses", "Duration");
            DropColumn("dbo.Classes", "Name");
        }
    }
}
