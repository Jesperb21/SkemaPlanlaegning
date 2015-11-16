using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<Teacher> TaughtByTeachers { get; set; }
        public List<ClassTemplate> IsInClassTemplates { get; set; }

        [NotMapped] public bool? isSelected { get; set; }
    }
}
