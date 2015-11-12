namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedmanytomanytables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeacherCourses", newName: "TeachableCourses");
            RenameTable(name: "dbo.TeacherCourseInstances", newName: "TeachingInCourses");
            RenameColumn(table: "dbo.TeachableCourses", name: "Teacher_Id", newName: "TeacherId");
            RenameColumn(table: "dbo.TeachableCourses", name: "Course_Id", newName: "CourseId");
            RenameColumn(table: "dbo.TeachingInCourses", name: "Teacher_Id", newName: "TeacherId");
            RenameColumn(table: "dbo.TeachingInCourses", name: "CourseInstance_Id", newName: "CourseId");
            RenameIndex(table: "dbo.TeachableCourses", name: "IX_Teacher_Id", newName: "IX_TeacherId");
            RenameIndex(table: "dbo.TeachableCourses", name: "IX_Course_Id", newName: "IX_CourseId");
            RenameIndex(table: "dbo.TeachingInCourses", name: "IX_Teacher_Id", newName: "IX_TeacherId");
            RenameIndex(table: "dbo.TeachingInCourses", name: "IX_CourseInstance_Id", newName: "IX_CourseId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TeachingInCourses", name: "IX_CourseId", newName: "IX_CourseInstance_Id");
            RenameIndex(table: "dbo.TeachingInCourses", name: "IX_TeacherId", newName: "IX_Teacher_Id");
            RenameIndex(table: "dbo.TeachableCourses", name: "IX_CourseId", newName: "IX_Course_Id");
            RenameIndex(table: "dbo.TeachableCourses", name: "IX_TeacherId", newName: "IX_Teacher_Id");
            RenameColumn(table: "dbo.TeachingInCourses", name: "CourseId", newName: "CourseInstance_Id");
            RenameColumn(table: "dbo.TeachingInCourses", name: "TeacherId", newName: "Teacher_Id");
            RenameColumn(table: "dbo.TeachableCourses", name: "CourseId", newName: "Course_Id");
            RenameColumn(table: "dbo.TeachableCourses", name: "TeacherId", newName: "Teacher_Id");
            RenameTable(name: "dbo.TeachingInCourses", newName: "TeacherCourseInstances");
            RenameTable(name: "dbo.TeachableCourses", newName: "TeacherCourses");
        }
    }
}
