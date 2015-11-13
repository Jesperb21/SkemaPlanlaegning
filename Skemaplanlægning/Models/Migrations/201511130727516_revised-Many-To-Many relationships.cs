namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revisedManyToManyrelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseInstances", "Class_Id", "dbo.Classes");
            DropIndex("dbo.CourseInstances", new[] { "Class_Id" });
            CreateTable(
                "dbo.ClassCourseInstances",
                c => new
                    {
                        Class_Id = c.Int(nullable: false),
                        CourseInstance_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Class_Id, t.CourseInstance_Id })
                .ForeignKey("dbo.Classes", t => t.Class_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseInstances", t => t.CourseInstance_Id, cascadeDelete: true)
                .Index(t => t.Class_Id)
                .Index(t => t.CourseInstance_Id);
            
            DropColumn("dbo.CourseInstances", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseInstances", "Class_Id", c => c.Int());
            DropForeignKey("dbo.ClassCourseInstances", "CourseInstance_Id", "dbo.CourseInstances");
            DropForeignKey("dbo.ClassCourseInstances", "Class_Id", "dbo.Classes");
            DropIndex("dbo.ClassCourseInstances", new[] { "CourseInstance_Id" });
            DropIndex("dbo.ClassCourseInstances", new[] { "Class_Id" });
            DropTable("dbo.ClassCourseInstances");
            CreateIndex("dbo.CourseInstances", "Class_Id");
            AddForeignKey("dbo.CourseInstances", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
