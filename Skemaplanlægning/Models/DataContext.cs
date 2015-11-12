using System.Data.Entity;

namespace Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=SkemaDbConnectionString")
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstance> CourseInstances { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassTemplate> ClassTemplates { get; set; }
        public DbSet<Student> Students { get; set;  }
        public DbSet<Teacher> Teachers { get; set; }
    }
}