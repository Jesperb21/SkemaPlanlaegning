using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// a basic POCO of a Teacher,
    /// ex: Michael Nielsen
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// the unique identifier of the specific teacher
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// FirstName of the teacher
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the teacher
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// A list of courses the Teacher is eligable to teach in
        /// </summary>
        public List<Course> TeachableCourses { get; set; }
        public List<CourseInstance> Teaching { get; set; } 
    }
}   