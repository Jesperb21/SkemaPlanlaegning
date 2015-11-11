using System;

namespace Models
{
    public class Test
    {
        public Test()
        {
            CourseType courseType = new CourseType();
            courseType.Id = 0;
            courseType.Name = "Programmering";

            SprintType sprintType = new SprintType();
            sprintType.Id = 0;
            sprintType.Name = "PG5";

            Sprint sprint = new Sprint();
            sprint.Id = 0;
            sprint.SprintType = sprintType;
            sprint.StartTime = DateTime.Now;
            sprint.EndTime = DateTime.Now;

            Student student = new Student();
            student.Id = 0;
            student.FirstName = "Jesper";
            student.LastName = "Baunsgaard";

            Teacher teacher = new Teacher();
            teacher.Id = 0;
            teacher.FirstName = "Michael";
            teacher.LastName = "Nielsen";
            teacher.TeachableCourses.Add(courseType);

            Course course = new Course();
            course.Id = 0;
            course.StartTime = DateTime.Now;
            course.EndTime = DateTime.Now.AddDays(5);
            course.Sprints.Add(sprint);
            course.Teachers.Add(teacher);
            course.CourseType = courseType;           


        }
        
        
    }
}
