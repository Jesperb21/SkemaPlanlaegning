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
            SelectedCourse = new Course();
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

        public ICommand SaveCommand
        {
            get
            {
                return new ActionCommand(a => SaveCourse());
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

        public void SaveCourse()
        {
            _context.Courses.AddOrUpdate(SelectedCourse);
            _context.SaveChanges();
            RefreshCourseList();
        }
    } 
}