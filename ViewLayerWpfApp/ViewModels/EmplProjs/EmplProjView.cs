using System;
using System.ComponentModel;
using System.Linq;
using ViewLayerWpfApp.ViewModels.Employees;

namespace ViewLayerWpfApp.ViewModels.EmplProjs
{
    public partial class EmplProjView : INotifyPropertyChanged, IEquatable<EmplProjView>
    {
        #region Fields
        int id;
        string projName;
        bool isChecked;
        bool isChanged;
        #endregion


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
        public string ProjName
        {
            get { return projName; }
            set
            {
                projName = value;
                OnPropertyChanged("ProjName");
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
        public EmplProjView Backup { get; set; }
        #endregion


        #region Methods
        void MakingBackup()
        {
            Backup = new EmplProjView();
            Backup.isChecked = isChecked;
        }
        public void BackupClear()
        {
            Backup = null;
        }
        bool IEquatable<EmplProjView>.Equals(EmplProjView ePV)
        {
            return Id == ePV.Id;
        }
        #endregion
    }
}