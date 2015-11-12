using System;
using System.Collections.Generic;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public CourseType CourseType { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Sprint> Sprints { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}