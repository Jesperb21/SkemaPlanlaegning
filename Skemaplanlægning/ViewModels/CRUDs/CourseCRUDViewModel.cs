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
        #region Single-line Private Fields

        private DataContext _context;
        private ObservableCollection<Course> _courses;
        private Course _selectedCourse;

        #endregion

        #region Constructors

        public CourseCRUDViewModel() : this(new DataContext())
        {
        }

        public CourseCRUDViewModel(DataContext context)
        {
            SelectedCourse = new Course();
            this._context = context;
        }

        #endregion

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
                _courses = value;
                NotifyPropertyChanged();
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

        #region Support Methods

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

        #endregion

    } 
}