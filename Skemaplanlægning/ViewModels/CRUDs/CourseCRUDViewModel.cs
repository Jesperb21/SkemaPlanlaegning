using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Models;
using ViewModels.Annotations;

namespace ViewModels.CRUDs
{
    public class CourseCRUDViewModel : BaseViewModel
    {
        private DataContext _context;
        private ObservableCollection<Course> _courses;
        private Course _selectedCourse;

        public CourseCRUDViewModel() : this(new DataContext())
        { }
        public CourseCRUDViewModel(DataContext context)
        {
            this._context = context;
        }

        #region Properties

        public ObservableCollection<Course> Courses
        {
            get
            {
                if (_courses != null)
                {
                    return _courses;
                }
                _courses = new ObservableCollection<Course>(_context.Courses);
                return _courses;
            }
            set
            {
                NotifyPropertyChanged();
                _courses = value;
            }
        }

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region ICommands

        public ICommand CreateCommand
        {
            get { return new ActionCommand(a => CreateCourse()); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new ActionCommand(r => RefreshCourseList());
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new ActionCommand(a => UpdateCourse());
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ActionCommand(a => DeleteCourse());
            }
        }
        #endregion

        public void CreateCourse()
        {
            SelectedCourse = new Course();
            
            //add to db
            //_context.Courses.Add(SelectedCourse);
            //_context.SaveChanges();

            //add to Observable Collection
            //Courses.Add(SelectedCourse);
        }

        public void RefreshCourseList()
        {
            Courses = new ObservableCollection<Course>(_context.Courses);
        }

        public void DeleteCourse()
        {
            //remove from db
            _context.Courses.Remove(SelectedCourse);
            _context.SaveChanges();

            //remove from ObservableCollection
            Courses.Remove(SelectedCourse);
            SelectedCourse = null;
        }

        public void UpdateCourse()
        {
            var course = _context.Courses.Find(SelectedCourse.Id);

            _context.Entry(course).CurrentValues.SetValues(SelectedCourse);
            _context.SaveChanges();
        }
    } 
}