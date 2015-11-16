using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq.Expressions;
using System.Windows.Input;
using Models;

namespace ViewModels.CRUDs
{
    public class ClassTemplateCRUDViewModel : BaseViewModel
    {
        private DataContext _context;
        private ObservableCollection<ClassTemplate> _templates;
        private ObservableCollection<Course> _courses;
        private ClassTemplate _selectedTemplate;

        public ClassTemplateCRUDViewModel() : this(new DataContext())
        { }
        public ClassTemplateCRUDViewModel(DataContext context)
        {
            SelectedTemplate = new ClassTemplate();
            this._context = context;
        }

        private ObservableCollection<Course> _selectedCourses;

        #region Properties

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

                NotifyPropertyChanged(nameof(SelectedCourses));
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Course> SelectedCourses
        {
            get
            {
                return _selectedCourses;
            }
            set
            {
                _selectedCourses = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region ICommands

        public ICommand CreateCommand
        {
            get { return new ActionCommand(a => CreateTemplate()); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new ActionCommand(r => RefreshTemplateList());
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new ActionCommand(a => SaveTemplate());
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ActionCommand(a => DeleteTemplate());
            }
        }

        public ICommand AddCourseToSelectedCourses
        {
            get
            {
                return new ActionCommand(a => addCourseToSelectedCourses());
            }
        }

        public Course selectedInCourses { get; set; }

        private void addCourseToSelectedCourses()
        {
            SelectedCourses.Add(selectedInCourses);
        }

        #endregion

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
    }
}