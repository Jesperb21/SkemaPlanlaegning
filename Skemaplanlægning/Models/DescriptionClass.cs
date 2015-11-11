using System;

namespace Models
{
    /// <summary>
    /// serves to describe the interactions between the different models
    /// </summary>
    public class DescriptionClass
    {
        public DescriptionClass()
        {
            #region Programmering 4

            CourseType courseType = new CourseType();
            courseType.Id = 0;
            courseType.Name = "Programmering";

            #endregion

            #region SprintType Datatekniker med speciale i Programmering H5

            SprintType sprintType = new SprintType();
            sprintType.Id = 0;
            sprintType.Name = "PG5";

            #endregion

            #region Sprint den instance af den type som vi er på

            Sprint sprint = new Sprint();
            sprint.Id = 0;
            sprint.SprintType = sprintType;
            
            #endregion

            #region Student & Teach

            Student student = new Student();
            student.Id = 0;
            student.FirstName = "Jesper";
            student.LastName = "Baunsgaard";

            Teacher teacher = new Teacher();
            teacher.Id = 0;
            teacher.FirstName = "Michael";
            teacher.LastName = "Nielsen";
            teacher.TeachableCourses.Add(courseType);

            #endregion

            #region Programmering 4 faget som kører den dato med de hold på det

            Course course = new Course();
            course.Id = 0;
            course.StartTime = DateTime.Now;
            course.EndTime = DateTime.Now.AddDays(5);
            course.Sprints.Add(sprint);
            course.Teachers.Add(teacher);
            course.CourseType = courseType;

            #endregion


        }
        
        
    }
}
