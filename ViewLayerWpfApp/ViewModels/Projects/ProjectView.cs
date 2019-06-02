using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.ProjLead;
//using ViewLayerWpfApp.ViewModels.Employees;
//using ViewLayerWpfApp.ViewModels.EmplProjs;

namespace ViewLayerWpfApp.ViewModels.Projects
{
    /// <summary>
    /// Класс сущности пользовательского интерфейса "Сотрудник"
    /// </summary>
    public class ProjectView : INotifyPropertyChanged
    {
        #region Fields
        int id;
        string projName = "";
        string orgOrderName = "";
        string orgExecuteName = "";
        DateTime? dateProjExecuteBegin;
        DateTime? dateProjExecuteEnd;
        int? priority;
        string comment = "";
        ObservableCollection<ProjEmplView> projEmplViews;
        ProjLeadView projLeadView;
        bool hasALeader;
        bool isChanged;
        bool isAdded;
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
        public string ProjName
        {
            get
            {
                return projName;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                projName = value == null ? "" : value;
                OnPropertyChanged("ProjName");
            }
        }
        public string OrgOrderName
        {
            get
            {
                return orgOrderName;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                orgOrderName = value == null ? "" : value;
                OnPropertyChanged("OrgOrderName");
            }
        }
        public string OrgExecuteName
        {
            get
            {
                return orgExecuteName;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                orgExecuteName = value == null ? "" : value;
                OnPropertyChanged("OrgExecuteName");
            }
        }
        public DateTime? DateProjExecuteBegin
        {
            get
            {
                return dateProjExecuteBegin;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                dateProjExecuteBegin = value;
                OnPropertyChanged("DateProjExecuteBegin");
            }
        }
        public DateTime? DateProjExecuteEnd
        {
            get
            {
                return dateProjExecuteEnd;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                dateProjExecuteEnd = value;
                OnPropertyChanged("dateProjExecuteEnd");
            }
        }
        public int? Priority
        {
            get
            {
                return priority;
            }
            set
            {
                if (value >= 0)
                {
                    if (PropertyChanged != null)
                        if (Backup == null)
                            MakingBackup();
                    priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                comment = value == null ? "" : value;
                OnPropertyChanged("Comment");
            }
        }
        public bool HasALeader
        {
            get
            {
                return hasALeader;
            }
            set
            {
                hasALeader = value;
                OnPropertyChanged("HasALeader");
            }
        }
        public ObservableCollection<ProjEmplView> ProjEmplViews
        {
            get { return projEmplViews; }
            set
            {
                projEmplViews = value;
                OnPropertyChanged("ProjEmplViews");
            }
        }
        public ProjLeadView ProjLeadView
        {
            get { return projLeadView; }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                projLeadView = value;
                HasALeader = projLeadView != null;
                OnPropertyChanged("ProjLeadView");
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
        public ProjectView Backup { get; set; }
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
            Backup = new ProjectView();
            Backup.projName = projName;
            Backup.orgOrderName = orgOrderName;
            Backup.orgExecuteName = orgExecuteName;
            Backup.dateProjExecuteBegin = dateProjExecuteBegin;
            Backup.dateProjExecuteEnd = dateProjExecuteEnd;
            Backup.priority = priority;
            Backup.comment = comment;
            Backup.ProjLeadView = ProjLeadView;
        }
        public void BackupClear()
        {
            Backup = null;
        }
        #endregion
    }
}
