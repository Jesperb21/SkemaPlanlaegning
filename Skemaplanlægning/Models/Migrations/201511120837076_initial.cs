namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassTemplate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTemplates", t => t.ClassTemplate_Id)
                .Index(t => t.ClassTemplate_Id);
            
            CreateTable(
                "dbo.ClassTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClassTemplate_Id = c.Int(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTemplates", t => t.ClassTemplate_Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.ClassTemplate_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.CourseInstances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Course_Id = c.Int(),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CourseInstance_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseInstances", t => t.CourseInstance_Id)
                .Index(t => t.CourseInstance_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.CourseInstances", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Teachers", "CourseInstance_Id", "dbo.CourseInstances");
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.CourseInstances", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Classes", "ClassTemplate_Id", "dbo.ClassTemplates");
            DropForeignKey("dbo.Courses", "ClassTemplate_Id", "dbo.ClassTemplates");
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropIndex("dbo.Teachers", new[] { "CourseInstance_Id" });
            DropIndex("dbo.CourseInstances", new[] { "Class_Id" });
            DropIndex("dbo.CourseInstances", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.Courses", new[] { "ClassTemplate_Id" });
            DropIndex("dbo.Classes", new[] { "ClassTemplate_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
            DropTable("dbo.CourseInstances");
            DropTable("dbo.Courses");
            DropTable("dbo.ClassTemplates");
            DropTable("dbo.Classes");
        }
    }
}
