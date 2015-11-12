using System.Data.Entity;

namespace Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("SkemaDbEntities")
        {
            
        }

        public DbSet<Course> Courses { get; }
        public DbSet<CourseType> CourseTypes { get; }
        public DbSet<Sprint> Sprints { get; }
        public DbSet<SprintType> SprintTypes { get; }
        public DbSet<Student> Students { get; }
        public DbSet<Teacher> Teachers { get; }
    }
}