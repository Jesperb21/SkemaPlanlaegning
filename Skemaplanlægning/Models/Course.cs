using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    /// <summary>
    /// an instance of a course, 
    /// ex: Programming 4 which runs in November 2015
    /// this contains teachers, a start/end date, general Course Type, and what sprints will contain the course 
    /// </summary>
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public CourseType CourseType { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Sprint> Sprints { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}