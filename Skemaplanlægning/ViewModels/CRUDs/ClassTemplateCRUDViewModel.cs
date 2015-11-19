using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Windows.Input;
using Models;

namespace ViewModels.CRUDs
{
    /// <summary>
    /// The viewmodel for the ClassTemplateCRUDView, 
    /// the "demands" of the class were to facilitate basic CRUD actions for Class Templates (Class in DB)
    /// and support selection / de-selection of Courses for the template
    /// </summary>
    public class ClassTemplateCRUDViewModel : BaseViewModel
    {
        #region single-line private fields

        private DataContext _context;
        private ObservableCollection<ClassTemplate> _templates;
        private ObservableCollection<Course> _courses;
        private ObservableCollection<Course> _selectedCourses;
        private ClassTemplate _selectedTemplate;

        #endregion


        #region Constructors

        public ClassTemplateCRUDViewModel() : this(new DataContext())
        {
        }

        public ClassTemplateCRUDViewModel(DataContext context)
        {
            SelectedTemplate = new ClassTemplate();
            this._context = context;
        }

        #endregion

        #region Properties

        #region Course Selection

        public Course selectedInCourses { get; set; }

        public Course selectedInSelectedCourses { get; set; }


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

        public ObservableCollection<Course> SelectedCourses
        {
            get { return _selectedCourses; }
            set
            {
                _selectedCourses = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Template CRUD bindable variables

        public ObservableCollection<ClassTemplate> Templates
        {
            get
            {
                if (_templates != null)
                {
                    return _templates;
                }
                _templates = new ObservableCollection<ClassTemplate>(_context.ClassTemplates);
                return _templates;
            }
            set
            {
                _templates = value;
                NotifyPropertyChanged();
            }
        }

        public ClassTemplate SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;

                if (value.Id != 0)
                {
                    //wait... entityframework is supposed to automatically load this when i havn't disabled lazyloading???
                    //well... it works, will probably have to add a TODO check up with michael about this
                    _context.Entry(SelectedTemplate).Collection(s => s.Courses).Load();

                    if (SelectedTemplate.Courses != null)
                        SelectedCourses = new ObservableCollection<Course>(SelectedTemplate.Courses);
                }

                NotifyPropertyChanged("SelectedCourses");
                NotifyPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region ICommands

        #region course selection ICommands

        public ICommand AddCourseToSelectedCourses
        {
            get { return new ActionCommand(a => addCourseToSelectedCourses()); }
        }


        public ICommand RemoveCourseToSelectedCourses
        {
            get { return new ActionCommand(a => removeCourseToSelectedCourses()); }
        }

        #endregion

        #region Template CRUD ICommands

        public ICommand CreateCommand
        {
            get { return new ActionCommand(a => CreateTemplate()); }
        }

        public ICommand RefreshCommand
        {
            get { return new ActionCommand(r => RefreshTemplateList()); }
        }

        public ICommand SaveCommand
        {
            get { return new ActionCommand(a => SaveTemplate()); }
        }

        public ICommand DeleteCommand
        {
            get { return new ActionCommand(a => DeleteTemplate()); }
        }

        #endregion

        #endregion

        #region support methods

        #region Course Selection

        private void addCourseToSelectedCourses()
        {
            SelectedCourses.Add(selectedInCourses);
        }

        private void removeCourseToSelectedCourses()
        {
            SelectedCourses.Remove(selectedInSelectedCourses);
        }


        #endregion

        #region Template CRUD methods

        public void CreateTemplate()
        {
            SelectedTemplate = new ClassTemplate();
        }

        public void RefreshTemplateList()
        {
            Templates = new ObservableCollection<ClassTemplate>(_context.ClassTemplates);
        }

        public void DeleteTemplate()
        {
            //remove from db
            _context.ClassTemplates.Remove(SelectedTemplate);
            _context.SaveChanges();

            //remove from ObservableCollection
            Templates.Remove(SelectedTemplate);
            SelectedTemplate = null;
        }

        public void SaveTemplate()
        {
            var sel = SelectedCourses;
            SelectedTemplate.Courses = new List<Course>(sel);
            _context.ClassTemplates.AddOrUpdate(SelectedTemplate);
            _context.SaveChanges();

            RefreshTemplateList();
        }

        #endregion


        #endregion

    }
}