using System;
using System.Collections.ObjectModel;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public CourseType CourseType { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public ObservableCollection<Sprint> Sprints { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}