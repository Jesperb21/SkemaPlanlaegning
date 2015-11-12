using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// CourseTypes, ex: Programmering 4
    /// </summary>
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// duration in weeks
        /// </summary>
        public int Duration { get; set; }
    }
}
