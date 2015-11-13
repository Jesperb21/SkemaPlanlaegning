using System.Data.Entity;

namespace Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=SkemaDbConnectionString")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            SetupManyToManyRelationships(modelBuilder);
        }

        private void SetupManyToManyRelationships(DbModelBuilder modelBuilder)
        {

            #region Teachers' Many-to-Many config

            modelBuilder.Entity<Teacher>().
                HasMany(t => t.TeachableCourses).
                WithMany(c => c.TaughtByTeachers).
                Map(m =>
                {
                    m.MapLeftKey("TeacherId");
                    m.MapRightKey("CourseId");
                    m.ToTable("TeachableCourses");
                });

            modelBuilder.Entity<Teacher>().
                HasMany(t => t.Teaching).
                WithMany(ci => ci.Teachers).
                Map(m =>
                {
                    m.MapLeftKey("TeacherId");
                    m.MapRightKey("CourseId");
                    m.ToTable("TeachingInCourses");
                });

            #endregion

            modelBuilder.Entity<ClassTemplate>().
                HasMany(ct => ct.Courses).
                WithMany(c => c.IsInClassTemplates);

            modelBuilder.Entity<Class>().
                HasMany(c => c.CourseInstances).
                WithMany(ci => ci.Classes);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseInstance> CourseInstances { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassTemplate> ClassTemplates { get; set; }
        public DbSet<Student> Students { get; set;  }
        public DbSet<Teacher> Teachers { get; set; }
    }
}