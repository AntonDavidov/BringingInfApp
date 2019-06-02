using System;
using System.ComponentModel;

namespace ViewLayerWpfApp.ViewModels.ProjLead
{
    public partial class ProjLeadView : INotifyPropertyChanged, IEquatable<ProjLeadView>
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
                if (propertyName == "IsChanged")
                    if (IsChanged == false)
                        Backup = null;
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
                {
                    if (Backup == null)
                        MakingBackup();
                }
                if (value != false)
                {
                    if (PropertyChanged != null)
                        foreach (var pLV in ProjLeadVM.ProjLeadViews)
                        {
                            if (pLV.isChecked == true)
                            {
                                if (pLV.Backup == null)
                                    pLV.MakingBackup();
                                pLV.IsChecked = false;
                            }
                        }
                }
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
        public ProjLeadView Backup { get; set; }
        public ProjLeadViewModel ProjLeadVM { get; set; }
        #endregion


        #region Methods
        void MakingBackup()
        {
            Backup = new ProjLeadView();
            Backup.id = Id;
            Backup.isChecked = isChecked;
        }
        public void BackupClear()
        {
            Backup = null;
        }
        bool IEquatable<ProjLeadView>.Equals(ProjLeadView pLV)
        {
            return id == pLV.Id && isChecked == pLV.IsChecked;
        }
        #endregion
    }
}
