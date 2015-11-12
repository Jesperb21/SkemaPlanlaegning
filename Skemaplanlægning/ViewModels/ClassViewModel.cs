using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Models;

namespace ViewModels
{
    public class ClassViewModel
    {
        public ObservableCollection<Class> Classes { get; set; }
        private static DataContext context;

        public ClassViewModel() : this(new DataContext())
        {
        }

        public ClassViewModel(DataContext dataContext)
        {
            context = dataContext;
            Classes = new ObservableCollection<Class>(dataContext.Classes);
        }

        public ICommand Save
        {
            get
            {
                return new ActionCommand(o => Add());
            }
        }

        public void Add()
        {
            context.Classes.Add(new Class()
            {
                CourseInstances = new List<CourseInstance>()
                {
                    new CourseInstance()
                    {
                        StartTime = DateTime.Now,
                        Course = new Course()
                        {
                            Name = "testcourse"
                        },
                        Teachers = new List<Teacher>()
                        {
                            new Teacher()
                            {
                                FirstName = "test",
                                LastName = "teacher"
                            }
                        }
                    }
                }
            });

            context.SaveChanges();
        }
    }
}