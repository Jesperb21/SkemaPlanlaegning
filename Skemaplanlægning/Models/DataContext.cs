using System.Data.Entity;

namespace Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("SkemaDbEntities")
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<SprintType> SprintTypes { get; set; }
        public DbSet<Student> Students { get; set;  }
        public DbSet<Teacher> Teachers { get; set; }
    }
}