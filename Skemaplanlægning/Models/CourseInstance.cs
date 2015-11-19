using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// an instance of a course, 
    /// ex: Programming 4 which runs in November 2015
    /// this contains teachers, a start/end date, general Course Type, and what sprints will contain the course 
    /// </summary>
    public class CourseInstance
    {
        [Key]
        public int Id { get; set; }
        public  List<Teacher> Teachers { get; set; }
        public List<Class> Classes { get; set; } 
        public Course Course { get; set; }
        public DateTime StartTime { get; set; }
    }
}