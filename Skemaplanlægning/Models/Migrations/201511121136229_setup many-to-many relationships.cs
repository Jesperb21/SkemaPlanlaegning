namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setupmanytomanyrelationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "CourseInstance_Id", "dbo.CourseInstances");
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.Teachers", new[] { "CourseInstance_Id" });
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Course_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.TeacherCourseInstances",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        CourseInstance_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.CourseInstance_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.CourseInstances", t => t.CourseInstance_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.CourseInstance_Id);
            
            DropColumn("dbo.Courses", "Teacher_Id");
            DropColumn("dbo.Teachers", "CourseInstance_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "CourseInstance_Id", c => c.Int());
            AddColumn("dbo.Courses", "Teacher_Id", c => c.Int());
            DropForeignKey("dbo.TeacherCourseInstances", "CourseInstance_Id", "dbo.CourseInstances");
            DropForeignKey("dbo.TeacherCourseInstances", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.TeacherCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.TeacherCourses", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.TeacherCourseInstances", new[] { "CourseInstance_Id" });
            DropIndex("dbo.TeacherCourseInstances", new[] { "Teacher_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "Course_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "Teacher_Id" });
            DropTable("dbo.TeacherCourseInstances");
            DropTable("dbo.TeacherCourses");
            CreateIndex("dbo.Teachers", "CourseInstance_Id");
            CreateIndex("dbo.Courses", "Teacher_Id");
            AddForeignKey("dbo.Teachers", "CourseInstance_Id", "dbo.CourseInstances", "Id");
            AddForeignKey("dbo.Courses", "Teacher_Id", "dbo.Teachers", "Id");
        }
    }
}
