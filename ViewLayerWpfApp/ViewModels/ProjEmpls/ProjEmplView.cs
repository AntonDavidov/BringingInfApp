using System;
using System.ComponentModel;
using System.Linq;
using ViewLayerWpfApp.ViewModels.ProjLead;

namespace ViewLayerWpfApp.ViewModels.ProjEmpls
{
    public partial class ProjEmplView : INotifyPropertyChanged, IEquatable<ProjEmplView>
    {
        #region Fields
        int id;
        string name;
        string surname;
        string middleName;
        bool isChecked;
        bool isChanged;
        #endregion


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (propertyName == "IsChecked")
                    if (isChecked == Backup.isChecked)
                    {
                        IsChanged = false;
                        if (ViewModelsContainer.ProjEmplsViewModel.ProjEmplViews.FirstOrDefault(pEV => pEV.IsChanged) == null)
                            ViewModelsContainer.ProjEmplsViewModel.IsProjEmplViewsChanged = false;
                        Backup = null;
                    }
                    else
                    {
                        IsChanged = true;
                        if (ViewModelsContainer.ProjEmplsViewModel.ProjEmplViews.FirstOrDefault(pEV => pEV.IsChanged) != null)
                            ViewModelsContainer.ProjEmplsViewModel.IsProjEmplViewsChanged = true;
                    }
            }
        }
        #endregion


        #region Properties
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (PropertyChanged != null)
                    if (Backup == null)
                        MakingBackup();
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                OnPropertyChanged("IsChanged");
            }
        }
        public ProjEmplView Backup { get; set; }
        #endregion


        #region Methods
        void MakingBackup()
        {
            Backup = new ProjEmplView();
            Backup.isChecked = isChecked;
        }
        public void BackupClear()
        {
            Backup = null;
        }
        bool IEquatable<ProjEmplView>.Equals(ProjEmplView pEV)
        {
            return Id == pEV.Id;
        }
        #endregion
    }
}