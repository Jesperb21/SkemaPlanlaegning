using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Input;
using Models;

namespace ViewModels.CRUDs
{
    //Jeg tager de her VM + dens view ~Jesper
    public class ClassTemplateCRUDViewModel : BaseViewModel
    {
        private DataContext _context;
        private ObservableCollection<ClassTemplate> _templates;
        private ClassTemplate _selectedTemplate;

        public ClassTemplateCRUDViewModel() : this(new DataContext())
        { }
        public ClassTemplateCRUDViewModel(DataContext context)
        {
            SelectedTemplate = new ClassTemplate();
            this._context = context;
        }

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
                NotifyPropertyChanged("Templates");
                _templates = value;
            }
        }

        public ClassTemplate SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;
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
        #endregion

        public void CreateTemplate()
        {
            SelectedTemplate = new ClassTemplate();
        }

        public void RefreshTemplateList()
        {
            //TODO hvafuck
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
            _context.ClassTemplates.AddOrUpdate(SelectedTemplate);
            _context.SaveChanges();
            RefreshTemplateList();
        }
    }
}