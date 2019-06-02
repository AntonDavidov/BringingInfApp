using AutoMapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.ProjLead;

namespace ViewLayerWpfApp.ViewModels.Employees
{
    /// <summary>
    /// Класс сущности пользовательского интерфейса "Сотрудник"
    /// </summary>
    public partial class EmployeeView : INotifyPropertyChanged
    {
        #region Fields
        int id;
        string name = "";
        string surname = "";
        string middleName = "";
        string eMail = "";
        ObservableCollection<EmplProjView> emplProjViews;
        ObservableCollection<EmplProjView> leadProjViews;
        bool isAdded;
        bool isChanged;
        bool isDeleted;
        bool isFiltered;
        #endregion


        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                name = value == null ? "" : value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                surname = value == null ? "" : value;
                OnPropertyChanged("Surname");
            }
        }
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                middleName = value == null ? "" : value;
                OnPropertyChanged("MiddleName");
            }
        }
        public string EMail
        {
            get
            {
                return eMail;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                eMail = value == null ? "" : value;
                OnPropertyChanged("EMail");
            }
        }
        public ObservableCollection<EmplProjView> EmplProjViews
        {
            get { return emplProjViews; }
            set
            {
                emplProjViews = value;
                OnPropertyChanged("EmplProjViews");
            }
        }
        public ObservableCollection<EmplProjView> LeadProjViews
        {
            get { return leadProjViews; }
            set
            {
                leadProjViews = value;
                OnPropertyChanged("LeadProjViews");
            }
        }
        public bool IsAdded
        {
            get
            {
                return isAdded;
            }
            set
            {
                isAdded = value;
                OnPropertyChanged("IsAdded");
            }
        }
        public bool IsChanged
        {
            get
            {
                return isChanged;
            }
            set
            {
                isChanged = value;
                OnPropertyChanged("IsChanged");
            }
        }
        public bool IsDeleted
        {
            get { return isDeleted; }
            set
            {
                isDeleted = value;
                OnPropertyChanged("IsDeleted");
            }
        }
        public bool IsFiltered
        {
            get { return isFiltered; }
            set
            {
                isFiltered = value;
                OnPropertyChanged("IsFiltered");
            }
        }
        public EmployeeView Backup { get; set; }
        #endregion


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region Methods
        void MakingBackup()
        {
            Backup = new EmployeeView();
            Backup.name = name;
            Backup.surname = surname;
            Backup.middleName = middleName;
            Backup.eMail = eMail;
        }
        public void BackupClear()
        {
            Backup = null;
        }
        #endregion
    }
}
