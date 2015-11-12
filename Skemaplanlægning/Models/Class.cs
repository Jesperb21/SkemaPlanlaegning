using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// An Instance of a SprintType, 
    /// ex the DT-P-H5 that runs from now to tomorrow
    /// </summary>
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public ClassTemplate ClassTemplate{ get; set; }
        public List<CourseInstance> CourseInstances { get; set; }
        public List<Student> Students { get; set; }
    }
}