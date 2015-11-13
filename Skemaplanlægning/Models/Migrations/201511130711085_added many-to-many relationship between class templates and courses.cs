namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmanytomanyrelationshipbetweenclasstemplatesandcourses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "ClassTemplate_Id", "dbo.ClassTemplates");
            DropIndex("dbo.Courses", new[] { "ClassTemplate_Id" });
            CreateTable(
                "dbo.ClassTemplateCourses",
                c => new
                    {
                        ClassTemplate_Id = c.Int(nullable: false),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassTemplate_Id, t.Course_Id })
                .ForeignKey("dbo.ClassTemplates", t => t.ClassTemplate_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.ClassTemplate_Id)
                .Index(t => t.Course_Id);
            
            DropColumn("dbo.Courses", "ClassTemplate_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "ClassTemplate_Id", c => c.Int());
            DropForeignKey("dbo.ClassTemplateCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ClassTemplateCourses", "ClassTemplate_Id", "dbo.ClassTemplates");
            DropIndex("dbo.ClassTemplateCourses", new[] { "Course_Id" });
            DropIndex("dbo.ClassTemplateCourses", new[] { "ClassTemplate_Id" });
            DropTable("dbo.ClassTemplateCourses");
            CreateIndex("dbo.Courses", "ClassTemplate_Id");
            AddForeignKey("dbo.Courses", "ClassTemplate_Id", "dbo.ClassTemplates", "Id");
        }
    }
}
