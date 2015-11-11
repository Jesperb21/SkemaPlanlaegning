using System.Collections.Generic;

namespace Models
{
    public class Teacher
    {
        /// <summary>
        /// the unique identifier of the specific teacher
        /// </summary>
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
        public List<CourseType> TeachableCourses { get; set; }
    }
}   