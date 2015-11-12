namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CourseType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseTypes", t => t.CourseType_Id)
                .Index(t => t.CourseType_Id);
            
            CreateTable(
                "dbo.CourseTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Teacher_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SprintType_Id = c.Int(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SprintTypes", t => t.SprintType_Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.SprintType_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.SprintTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Sprint_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sprints", t => t.Sprint_Id)
                .Index(t => t.Sprint_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseTypes", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Sprints", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Students", "Sprint_Id", "dbo.Sprints");
            DropForeignKey("dbo.Sprints", "SprintType_Id", "dbo.SprintTypes");
            DropForeignKey("dbo.Courses", "CourseType_Id", "dbo.CourseTypes");
            DropIndex("dbo.Teachers", new[] { "Course_Id" });
            DropIndex("dbo.Students", new[] { "Sprint_Id" });
            DropIndex("dbo.Sprints", new[] { "Course_Id" });
            DropIndex("dbo.Sprints", new[] { "SprintType_Id" });
            DropIndex("dbo.CourseTypes", new[] { "Teacher_Id" });
            DropIndex("dbo.Courses", new[] { "CourseType_Id" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.SprintTypes");
            DropTable("dbo.Sprints");
            DropTable("dbo.CourseTypes");
            DropTable("dbo.Courses");
        }
    }
}
