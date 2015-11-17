using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using Models;

namespace ViewModels.CRUDs
{
    /// <summary>
    /// heeeeeeh... venter lidt med den her... den ser ikke umidbart ud til at kunne tilføjes uden alle dependencies er der
    /// TODO remake this description, load CourseInstances, select students? somehow need a mechanic for this, 
    /// TODO somehow in the autofill function make it so that course instances gets added if they already exist in db, based on startdate,
    /// TODO i dont know how that'll work... havn't figured out anything with dates yet
    /// The viewmodel for the ClassTemplateCRUDView, 
    /// the "demands" of the class were to facilitate basic CRUD actions for Class Templates (Class in DB)
    /// and support selection / de-selection of Courses for the template
    /// </summary>
    public class ClassCRUDViewModel : BaseViewModel
    {
        #region single-line private fields

        private DataContext _context;
        private ObservableCollection<ClassTemplate> _templates;
        private ObservableCollection<Class> _classes;
        private Class _selectedClass;
        private ClassTemplate _selectedTemplate;
        private CourseInstance _selectedCourseInstance;
        private ObservableCollection<CourseInstance> _courseInstances;

        #endregion

        #region Constructors

        public ClassCRUDViewModel() : this(new DataContext())
        {
        }

        public ClassCRUDViewModel(DataContext context)
        {
            SelectedTemplate = new ClassTemplate();
            SelectedClass = new Class();
            this._context = context;
        }

        #endregion

        #region Properties

        #region Classes & SelectedClass

        public ObservableCollection<Class> Classes
        {
            get
            {
                if (_classes != null)
                {
                    return _classes;
                }
                _classes = new ObservableCollection<Class>(_context.Classes);

                return _classes;
            }
            set
            {
                _classes = value;
                NotifyPropertyChanged();
            }
        }

        public Class SelectedClass
        {
            get { return _selectedClass; }
            set
            {
                _selectedClass = value;

                if (value.Id != 0)
                {
                    if (_selectedClass.CourseInstances != null)
                    {
                        _selectedClass.CourseInstances.Clear();
                        //wait... entityframework is supposed to automatically load this when i havn't disabled lazyloading???
                        //well... it works, will probably have to add a TODO check up with michael about this
                        _context.Entry(SelectedClass).Collection(s => s.CourseInstances).Load();
                    }
                }
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Templates & SelectedTemplate

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
                    //damn i love the elvis operator
                    if (_selectedTemplate.Courses != null)
                    {
                        _selectedTemplate.Courses.Clear();

                        //wait... entityframework is supposed to automatically load this when i havn't disabled lazyloading???
                        //well... it works, will probably have to add a TODO check up with michael about this
                        _context.Entry(SelectedTemplate).Collection(s => s.Courses).Load();
                    }
                }
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Course Instance Manipulation

        public ObservableCollection<CourseInstance> CourseInstances
        {
            get { return _courseInstances; }
            set
            {
                _courseInstances = value;
                NotifyPropertyChanged();
            }
        }

        public CourseInstance SelectedCourseInstance
        {
            get
            {
                return _selectedCourseInstance;
            }
            set
            {
                _selectedCourseInstance = value;
                NotifyPropertyChanged();
            }
        }


        #endregion

        #endregion

        #region ICommands

        #region Class CRUD ICommands

        public ICommand CreateCommand
        {
            get { return new ActionCommand(a => CreateNewClass()); }
        }

        public ICommand RefreshCommand
        {
            get { return new ActionCommand(r => RefreshClassList()); }
        }

        public ICommand SaveCommand
        {
            get { return new ActionCommand(a => SaveClass()); }
        }

        public ICommand DeleteCommand
        {
            get { return new ActionCommand(a => DeleteClass()); }
        }

        #endregion

        #region Template ICommands

        public ICommand AutoFIllFromTemplate
        {
            get { return new ActionCommand(a => autoFillFromTemplate()); }
        }


        #endregion

        #endregion

        #region support methods

        #region Class CRUD methods

        public void CreateNewClass()
        {
            SelectedClass = new Class();
        }

        public void RefreshClassList()
        {
            Classes = new ObservableCollection<Class>(_context.Classes);
        }

        public void DeleteClass()
        {
            //remove from db
            _context.Classes.Remove(SelectedClass);
            _context.SaveChanges();

            //remove from ObservableCollection
            Classes.Remove(SelectedClass);
            SelectedClass = new Class();
        }

        public void SaveClass()
        {
            SelectedClass.CourseInstances = CourseInstances.ToList();

            SaveCourseInstances();

            SelectedClass.Students = _context.Students.ToList();

            _context.Classes.AddOrUpdate(SelectedClass);
            _context.SaveChanges();

            RefreshClassList();
        }

        #endregion

        #region Template Methods

        private void autoFillFromTemplate()
        {
            if (CourseInstances == null)
            {
                CourseInstances = new ObservableCollection<CourseInstance>();
            }
            foreach (var course in SelectedTemplate.Courses)
            {
                if (!CourseInstances.ToList().Exists(instance => instance.Course == course))
                {
                    var inst = new CourseInstance() {Course = course};
                    CourseInstances.Add(inst);
                }
            }
        }

        #endregion

        #region CourseInstance Methods

        private void SaveCourseInstances()
        {
            //to reduce number of db calls i store all the course instances in the database
            var CInstInDb = _context.CourseInstances.ToList();

            foreach (var cInst in CourseInstances)
            {
                if (//if cInst is not in db
                    !CInstInDb.Exists(
                        inst => inst.StartTime == cInst.StartTime && 
                                inst.Course == cInst.Course))
                {
                    cInst.Teachers = _context.Teachers.ToList();
                    cInst.Classes = new List<Class>() { SelectedClass };
                    _context.CourseInstances.Add(cInst);
                }
            }
        }

        #endregion

        #endregion

    }
}