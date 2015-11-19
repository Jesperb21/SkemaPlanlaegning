using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Collections.ObjectModel;
namespace ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        private DataContext _context;
        private ObservableCollection<Teacher> _teachers;
        private Teacher _selectedTeacher;
        private ObservableCollection<Course> _coursesSelected;


        public CourseViewModel() : this(new DataContext())
        {
        }

        public CourseViewModel(DataContext context)
        {
            SelectedTeacher = new Teacher();
            this._context = context;
        }

        public ObservableCollection<Teacher> Teachers
        {
            get
            {
                if (_teachers != null)
                {
                    return _teachers;
                }
                _teachers = new ObservableCollection<Teacher>(_context.Teachers);
                return _teachers;
            }
            set
            {
                _teachers = value;
                NotifyPropertyChanged();
            }
        }

        public Teacher SelectedTeacher
        {
            get { return _selectedTeacher; }
            set
            {
                _selectedTeacher = value;
                if (_selectedTeacher != null && _selectedTeacher.Id != 0)
                {
                    _context.Teachers.Include("Teaching").Include("Teaching.Course").ToList();
                }
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Course> CoursesSelected
        {
            get { return _coursesSelected; }
            set
            {
                _coursesSelected = value;
                NotifyPropertyChanged();
            }
        }


    }


}
