using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Input;
using Models;

namespace ViewModels.CRUDs
{
    public class TeacherCRUDViewModel : BaseViewModel
    {
        #region Single-line Private Fields

        private DataContext _context;
        private ObservableCollection<Teacher> _teachers;
        private Teacher _selectedTeacher;
        private Course _selectedCourseInAvailableCourses;
        private Course _selectedCourseInSelectedCourses;
        private ObservableCollection<Course> _coursesAvailable;
        private ObservableCollection<Course> _coursesSelected;
         
        #endregion

        #region Constructors

        public TeacherCRUDViewModel() : this(new DataContext())
        {
        }

        public TeacherCRUDViewModel(DataContext context)
        {
            _context = context;
            SelectedTeacher = new Teacher();
        }

        #endregion

        #region Properties

        #region Teacher CRUD Properties

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
                    _context.Entry(_selectedTeacher).Collection(s => s.TeachableCourses).Load();
                    CoursesSelected = new ObservableCollection<Course>(value.TeachableCourses);

                }
                else
                {
                    CoursesSelected = new ObservableCollection<Course>();
                }
                
                SelectedCourseInSelectedCourses = null;

                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Course Selection Properties

        public Course SelectedCourseInAvailableCourses
        {
            get { return _selectedCourseInAvailableCourses; }
            set
            {
                _selectedCourseInAvailableCourses = value;
                NotifyPropertyChanged();
            }
        }

        public Course SelectedCourseInSelectedCourses
        {
            get { return _selectedCourseInSelectedCourses; }
            set
            {
                _selectedCourseInSelectedCourses = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Course> CoursesAvailable
        {
            get
            {
                if (_coursesAvailable == null)
                {
                    _coursesAvailable = new ObservableCollection<Course>(_context.Courses);
                }

                return _coursesAvailable;
            }
            set
            {
                _coursesAvailable = value;
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

        #endregion

        #endregion

        #region ICommands

        #region Teacher CRUD ICommands

        public ICommand CreateCommand
        {
            get { return new ActionCommand(a => CreateTeacher()); }
        }

        public ICommand RefreshCommand
        {
            get { return new ActionCommand(r => RefreshTeacherList()); }
        }

        public ICommand SaveCommand
        {
            get { return new ActionCommand(a => SaveTeacher()); }
        }

        public ICommand DeleteCommand
        {
            get { return new ActionCommand(a => DeleteTeacher()); }
        }

        #endregion

        #region CourseSelection ICommands

        public ICommand AddCourseToSelectedCourses
        {
            get { return new ActionCommand(a => addCourseToSelectedCourses()); }
        }


        public ICommand RemoveCourseFromSelectedCourses
        {
            get { return new ActionCommand(a => removeCourseFromSelectedCourses()); }
        }


        #endregion

        #endregion

        #region Support Methods

        #region Teacher CRUD Methods

        public void CreateTeacher()
        {
            SelectedTeacher = new Teacher();
        }

        public void RefreshTeacherList()
        {
            Teachers = new ObservableCollection<Teacher>(_context.Teachers);
        }

        public void DeleteTeacher()
        {
            //remove from db
            _context.Teachers.Remove(SelectedTeacher);
            _context.SaveChanges();

            //remove from ObservableCollection
            Teachers.Remove(SelectedTeacher);
            SelectedTeacher = new Teacher();
        }

        public void SaveTeacher()
        {
            if (CoursesSelected != null)
            {
                SelectedTeacher.TeachableCourses = CoursesSelected.ToList();
            }
            _context.Teachers.AddOrUpdate(SelectedTeacher);
            _context.SaveChanges();
            RefreshTeacherList();
        }

        #endregion

        #region Course Selection Methods

        private void addCourseToSelectedCourses()
        {
            if(SelectedCourseInAvailableCourses != null)
                CoursesSelected.Add(SelectedCourseInAvailableCourses);
        }

        private void removeCourseFromSelectedCourses()
        {
            if(SelectedCourseInSelectedCourses != null)
                CoursesSelected.Remove(SelectedCourseInSelectedCourses);
        }

        #endregion


        #endregion

    }
}