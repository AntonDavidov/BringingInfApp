using AutoMapper;
using ClientEntities;
using ILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using ViewLayerWpfApp.ViewModels.SupportingClasses;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.ProjLead;
using ViewLayerWpfApp.ViewModels.Projects;


namespace ViewLayerWpfApp.ViewModels.Employees
{
    public partial class EmployeesViewModel: INotifyPropertyChanged
    {
        #region Fields
        ICommand dataLoadingCommand;
        #region Filters of Data, loaded into App
        string nameFilter;
        string surnameFilter;
        string middleNameFilter;
        #endregion

        #region Filters of Data, loading from Db
        bool loadFromDb;
        ICommand loadFromDbCommand;
        #endregion

        #region Filter Commands
        ICommand nameFilterChangedCommand;
        ICommand surnameFilterChangedCommand;
        ICommand middleNameFilterChangedCommand;

        #region GotFocus Commands
        ICommand nameFilterGotFocusCommand;
        ICommand surnameFilterGotFocusCommand;
        ICommand middleNameFilterGotFocusCommand;
        #endregion
        #endregion

        #region ViewModelCollection editing Commands
        ICommand addEmployeeCommand;
        ICommand delEmployeeCommand;
        #endregion

        #region Data editing Commands
        ICommand saveChangesCommand;
        ICommand cancelChangesCommand;
        #endregion

        EmployeeView currentEmplView;
        IList selectedEmplViews = null;
        MessageBoxModel messageBoxVM;
        bool isEmployeeViewsChanged;

		bool needAuthentication = false;
		bool needApplicationShutdown = false;

		#region Supporting Fields
		string FilterControlName;
        string errCaption = "Сотрудники: Ошибка";
        #endregion
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
        public ObservableCollection<EmployeeView> EmployeeViews { get; set; } = new ObservableCollection<EmployeeView>();
        public ICommand DataLoadingCommand
        {
            get
            {
                if (dataLoadingCommand == null)
                    dataLoadingCommand = new RelayCommand(() => DataLoading());
                return dataLoadingCommand;
            }
        }
        #region Filters of Data, loaded into App
        public string NameFilter
        {
            get { return nameFilter; }
            set
            {
                nameFilter = value;
                OnPropertyChanged("NameFilter");
            }
        }
        public string SurnameFilter
        {
            get { return surnameFilter; }
            set
            {
                surnameFilter = value;
                OnPropertyChanged("SurnameFilter");
            }
        }
        public string MiddleNameFilter
        {
            get { return middleNameFilter; }
            set
            {
                middleNameFilter = value;
                OnPropertyChanged("MiddleNameFilter");
            }
        }
        #endregion

        #region Filters of Data, loading from Db
        public bool LoadFromDb
        {
            get { return loadFromDb; }
            set { loadFromDb = value; OnPropertyChanged("LoadFromDb"); }
        }
        public ICommand FilterViewFromDBCommand
        {
            get
            {
                if (loadFromDbCommand == null)
                    loadFromDbCommand = new RelayCommand(() => FilterViewFromDB());
                return loadFromDbCommand;
            }
        }
        #endregion

        #region Filter Commands
        public ICommand NameFilterChangedCommand
        {
            get
            {
                if (nameFilterChangedCommand == null)
                    nameFilterChangedCommand = new RelayCommand(() => NameFilterChanged());
                return nameFilterChangedCommand;
            }
        }
        public ICommand SurnameFilterChangedCommand
        {
            get
            {
                if (surnameFilterChangedCommand == null)
                    surnameFilterChangedCommand = new RelayCommand(() => SurnameFilterChanged());
                return surnameFilterChangedCommand;
            }
        }
        public ICommand MiddleNameFilterChangedCommand
        {
            get
            {
                if (middleNameFilterChangedCommand == null)
                    middleNameFilterChangedCommand = new RelayCommand(() => MiddleNameFilterChanged());
                return middleNameFilterChangedCommand;
            }
        }

        #region GotFocus Commands
        public ICommand NameFilterGotFocusCommand
        {
            get
            {
                if (nameFilterGotFocusCommand == null)
                    nameFilterGotFocusCommand = new RelayCommand(() => NameFilterGotFocus());
                return nameFilterGotFocusCommand;
            }
        }
        public ICommand SurnameFilterGotFocusCommand
        {
            get
            {
                if (surnameFilterGotFocusCommand == null)
                    surnameFilterGotFocusCommand = new RelayCommand(() => SurnameFilterGotFocus());
                return surnameFilterGotFocusCommand;
            }
        }
        public ICommand MiddleNameFilterGotFocusCommand
        {
            get
            {
                if (middleNameFilterGotFocusCommand == null)
                    middleNameFilterGotFocusCommand = new RelayCommand(() => MiddleNameFilterGotFocus());
                return middleNameFilterGotFocusCommand;
            }
        }
        #endregion
        #endregion

        #region ViewModelCollection Editing Commands
        public ICommand AddEmployeeViewCommand
        {
            get
            {
                if (addEmployeeCommand == null)
                    addEmployeeCommand = new RelayCommand(() => AddEmployeeView());
                return addEmployeeCommand;
            }
        }
        public ICommand DelEmployeeViewCommand
        {
            get
            {
                if (delEmployeeCommand == null)
                    delEmployeeCommand = new RelayCommand(() => DelEmployeeView());
                return delEmployeeCommand;
            }
        }
        #endregion

        #region Data Editing Commands
        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                    saveChangesCommand = new RelayCommand(() => SaveChanges());
                return saveChangesCommand;
            }
        }
        public ICommand CancelChangesCommand
        {
            get
            {
                if (cancelChangesCommand == null)
                    cancelChangesCommand = new RelayCommand(() => CancelChanges());
                return cancelChangesCommand;
            }
        }
        #endregion

        public EmployeeView CurrentEmplView
        {
            get { return currentEmplView; }
            set { currentEmplView = value; OnPropertyChanged("CurrentEmplView"); }
        }
        public IList SelectedEmplViews
        {
            get { return selectedEmplViews; }
            set { selectedEmplViews = value; OnPropertyChanged("SelectedEmplViews"); }
        }
        public MessageBoxModel MessageBoxVM
        {
            get { return messageBoxVM; }
            set { messageBoxVM = value; OnPropertyChanged("MessageBoxVM"); }
        }
		public bool IsEmployeeViewsChanged
		{
			get { return isEmployeeViewsChanged; }
			set
			{
				isEmployeeViewsChanged = value;
				OnPropertyChanged("IsEmployeeViewsChanged");
			}
		}

		public bool NeedAuthentication
		{
			get { return needAuthentication; }
			set
			{
				needAuthentication = value;
				OnPropertyChanged("NeedAuthentication");
			}
		}
		public bool NeedApplicationShutdown
		{
			get { return needApplicationShutdown; }
			set
			{
				needApplicationShutdown = value;
				OnPropertyChanged("NeedApplicationShutdown");
			}
		}
		#region Supporting Properties
		[Import]
        IEmployeesLogic EmployeeLogic { get; set; }
        public ProjectsViewModel ProjectsViewModel { get; set; }
		public bool IsEmployeeViewsChangedByProjectsViewModel { get; set; } = false;
        #endregion
        #endregion


        #region Methods
        void DataLoading()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                DIConfig.ComposeContainer.ComposeParts(this);
				EmployeeLogic.Login = Credentials.Login;
				EmployeeLogic.Password = Credentials.Password;
				employees = EmployeeLogic.GetEmployees();
            }
            catch(Exception ex) 
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                    Caption = errCaption
                };
                Log.WriteLogAsync(ex);
            }

            Mapper.Map<List<EmployeeView>>(employees)
                        .ForEach(e =>
                        {
                            e.PropertyChanged += (s, eArgs) =>
                                OnEmployeeViewChnaged(e, eArgs.PropertyName);
                            e.EmplProjViews.CollectionChanged += (s, eArgs) =>
                                OnEmplProjViewsCountChanged(e, eArgs.NewItems, eArgs.OldItems);
                            e.LeadProjViews.CollectionChanged += (s, eArgs) =>
                                OnLeadProjViewsCountChanged(e, eArgs.NewItems, eArgs.OldItems);
                            EmployeeViews.Add(e);
                        });
            CurrentEmplView = EmployeeViews.FirstOrDefault();
        }

        #region Filters of Data, loaded into App
        void NameFilterChanged()
        {
            if (loadFromDb == false)
            {
                if (nameFilter != "")
                    foreach (var eV in EmployeeViews)
                        if (eV.Name.StartsWith(nameFilter))
                            eV.IsFiltered = false;
                        else
                            eV.IsFiltered = true;
                else
                    foreach (var eV in EmployeeViews)
                        eV.IsFiltered = false;
            }
        }
        void SurnameFilterChanged()
        {
            if (loadFromDb == false)
            {
                
                if (surnameFilter != "")
                    foreach (var eV in EmployeeViews)
                        if (eV.Surname.StartsWith(surnameFilter))
                            eV.IsFiltered = false;
                        else
                            eV.IsFiltered = true;
                else
                    foreach (var eV in EmployeeViews)
                        eV.IsFiltered = false;
            }
        }
        void MiddleNameFilterChanged()
        {
            if (loadFromDb == false)
            {
                if (middleNameFilter != "")
                    foreach (var eV in EmployeeViews)
                        if (eV.MiddleName.StartsWith(middleNameFilter))
                            eV.IsFiltered = false;
                        else
                            eV.IsFiltered = true;
                else
                    foreach (var eV in EmployeeViews)
                        eV.IsFiltered = false;
            }
        }

        #region GotFocus Methods
        void NameFilterGotFocus()
        {
            FilterControlName = "NameFilter";
            SurnameFilter = "";
            MiddleNameFilter = "";
        }
        void SurnameFilterGotFocus()
        {
            FilterControlName = "SurnameFilter";
            NameFilter = "";
            MiddleNameFilter = "";
        }
        void MiddleNameFilterGotFocus()
        {
            FilterControlName = "MiddleNameFilter";
            NameFilter = "";
            SurnameFilter = "";
        }
        #endregion
        #endregion

        #region Filters of Data, loading from Db
        void FilterViewFromDB()
        {
            if (LoadFromDb == true)
            {
                List<Employee> employees = new List<Employee>();
                try
                {
                    if (EmployeeViews.Where(eV => eV.IsAdded).Count() > 0 ||
                       EmployeeViews.Where(eV => eV.IsChanged).Count() > 0 ||
                       EmployeeViews.Where(eV => eV.IsDeleted).Count() > 0)
                    {
                        MessageBoxVM = new MessageBoxModel()
                        {
                            Message = "Все внесённые изменения будут удалены. Продолжить?",
                            Caption = "сотрудники",
                            Buttons = MessageBoxButton.YesNo
                        };
                        if (MessageBoxVM.Result == MessageBoxResult.Yes)
                        {
                            while (EmployeeViews.Count > 0)
                                EmployeeViews.Remove(EmployeeViews.First());
                            employees = LoadFilteredDataFromDb(FilterControlName);
                        }
                    }
                    else
                    {
                        while (EmployeeViews.Count > 0)
                            EmployeeViews.Remove(EmployeeViews.First());
                        employees = LoadFilteredDataFromDb(FilterControlName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxVM = new MessageBoxModel()
                    {
                        Message = ErrorMessage.MakingMessageForMessageBox(ex),
                        Caption = errCaption
                    };
                    Log.WriteLogAsync(ex);
                }
                Mapper.Map<List<EmployeeView>>(employees)
                   .ForEach(e =>
                   {
                       e.PropertyChanged += (s, eArgs) =>
                           OnEmployeeViewChnaged(e, eArgs.PropertyName);
                       e.EmplProjViews.CollectionChanged += (s, eArgs) =>
                           OnEmplProjViewsCountChanged(e, eArgs.NewItems, eArgs.OldItems);
                       e.LeadProjViews.CollectionChanged += (s, eArgs) =>
                             OnLeadProjViewsCountChanged(e, eArgs.NewItems, eArgs.OldItems);
                       EmployeeViews.Add(e);
                   });
            }
        }
        #endregion

        #region ViewModelCollection editing Methods
        void AddEmployeeView()
        {
            EmployeeViews.Add(new EmployeeView());
            EmployeeViews.Last().IsAdded = true;
            if (EmployeeViews.FirstOrDefault(eV => eV.IsAdded) != null)
                IsEmployeeViewsChanged = true;
        }
        void DelEmployeeView()
        {
            if (SelectedEmplViews != null)
            {
                IEnumerable<EmployeeView> emplViewsToDel = SelectedEmplViews.Cast<EmployeeView>();
                while (emplViewsToDel.Where(i => i.IsAdded).Count() > 0)
                    EmployeeViews.Remove(emplViewsToDel.First(i => i.IsAdded));

                foreach (var eV in emplViewsToDel)
                {
                    if (eV.IsAdded == false)
                        eV.IsDeleted = true;
                }
                if (EmployeeViews.FirstOrDefault(eV => eV.IsAdded || eV.IsDeleted) != null)
                    IsEmployeeViewsChanged = true;
                else
                    IsEmployeeViewsChanged = false;
            }
        }
        #endregion

        #region Data editing Methods
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = "сотрудники",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                List<Employee> emplsToAdd = Mapper.Map<IEnumerable<EmployeeView>, List<Employee>>(EmployeeViews.Where(eV => eV.IsAdded));
                List<Employee> emplsToUpdate = Mapper.Map<IEnumerable<EmployeeView>, List<Employee>>(EmployeeViews.Where(eV => eV.IsChanged));
                List<Employee> emplsToDelete = Mapper.Map<IEnumerable<EmployeeView>, List<Employee>>(EmployeeViews.Where(eV => eV.IsDeleted));

                bool IsChangesSaved = false;
                try
                {
                    IsChangesSaved = EmployeeLogic.SetEmployees(emplsToAdd, emplsToUpdate, emplsToDelete);
                }
                catch (Exception ex)
                {
                    MessageBoxVM = new MessageBoxModel()
                    {
                        Message = ErrorMessage.MakingMessageForMessageBox(ex),
                        Caption = errCaption
                    };
                    Log.WriteLogAsync(ex);
                }
                if (IsChangesSaved)
                {
                    if (emplsToDelete.Count > 0)
                        while (EmployeeViews.Where(eV => eV.IsDeleted).Count() > 0)
                        {
                            EmployeeView emplView = EmployeeViews.First(eV => eV.IsDeleted);
                            foreach (var pV in ViewModelsContainer.ProjectsViewModel.ProjectViews)
                            {
                                pV.ProjEmplViews.Remove(Mapper.Map<ProjEmplView>(emplView));
                                if (pV.ProjLeadView != null && pV.ProjLeadView.Id == emplView.Id)
                                    pV.ProjLeadView = null;
                            }
                            EmployeeViews.Remove(emplView);
                        }

                    if (emplsToAdd.Count > 0)
                    {
                        IEnumerator<Employee> emplEnumerator = EmployeeLogic.GetEmplIDs().GetEnumerator();
                        foreach (var eV in EmployeeViews)
                        {
                            emplEnumerator.MoveNext();
                            eV.Id = emplEnumerator.Current.Id;
                            if (eV.IsAdded == true)
                            {
                                eV.LeadProjViews = new ObservableCollection<EmplProjView>();
                                eV.EmplProjViews = new ObservableCollection<EmplProjView>();
                            }
                            eV.IsAdded = false;
                        }
                    }

                    if (emplsToUpdate.Count > 0)
                        foreach (var eV in EmployeeViews)
                        {
                            eV.IsChanged = false;
                            eV.BackupClear();
                        }
                    IsEmployeeViewsChanged = false;
                }
            }
        }
        void CancelChanges()
        {
            if (EmployeeViews.Where(e => e.IsAdded).Count() > 0 ||
                EmployeeViews.Where(e => e.IsChanged).Count() > 0 ||
                EmployeeViews.Where(e => e.IsDeleted).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = "сотрудники",
                    Buttons = MessageBoxButton.YesNo  
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                {
                    int numOfAdded = 0;
                    foreach (var eV in EmployeeViews)
                    {
                        if (eV.IsAdded) numOfAdded++;
                        if (eV.IsChanged)
                        {
                            eV.Name = eV.Backup.Name;
                            if (eV.IsChanged) eV.Surname = eV.Backup.Surname;
                            if (eV.IsChanged) eV.MiddleName = eV.Backup.MiddleName;
                            if (eV.IsChanged) eV.EMail = eV.Backup.EMail;
                        }
                        if (eV.IsDeleted)
                            eV.IsDeleted = false;
                    }
                    while (numOfAdded > 0)
                    {
                        EmployeeViews.Remove(EmployeeViews.First(eV => eV.IsAdded));
                        numOfAdded--;
                    }

                    if (EmployeeViews.FirstOrDefault(eV => eV.IsAdded || eV.IsDeleted) != null)
                        IsEmployeeViewsChanged = true;
                    else
                        IsEmployeeViewsChanged = false;
                }
            }
        }
        #endregion

        #region Supporting Methods
        List<Employee> LoadFilteredDataFromDb(string filterControlName)
        {
            switch (FilterControlName)
            {
                case "NameFilter":
                    return EmployeeLogic.GetEmployeesByNameStartWith(NameFilter);
                case "SurnameFilter":
                    return EmployeeLogic.GetEmployeesBySurnameStartWith(SurnameFilter);
                case "MiddleNameFilter":
                    return EmployeeLogic.GetEmployeesByMiddleNameStartWith(MiddleNameFilter);
                default:
                    return EmployeeLogic.GetEmployees();
            }
        }
        #region EmployeeView Editing
        void OnEmployeeViewChnaged(EmployeeView emplView, string propertyName)
        {
            if (propertyName != "Id" &&
                propertyName != "EmplProjViews" &&
                propertyName != "LeadProjViews" &&
                propertyName != "IsAdded" &&
                propertyName != "IsChanged" &&
                propertyName != "IsDeleted" &&
                propertyName != "IsFiltered" &&
                emplView.IsAdded == false)

                if (emplView.Name == emplView.Backup.Name &&
                    emplView.Surname == emplView.Backup.Surname &&
                    emplView.MiddleName == emplView.Backup.MiddleName &&
                    emplView.EMail == emplView.Backup.EMail)
                {
                    emplView.IsChanged = false;
                    if (ViewModelsContainer.EmployeesViewModel.EmployeeViews.FirstOrDefault(eV => eV.IsChanged) == null)
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChanged = false;
                    emplView.BackupClear();
                }
                else
                {
                    emplView.IsChanged = true;
                    if (ViewModelsContainer.EmployeesViewModel.EmployeeViews.FirstOrDefault(eV => eV.IsChanged) != null)
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChanged = true;
                }
        }
        void OnEmplProjViewsCountChanged(EmployeeView emplView, IList newItems, IList oldItems)
        {
            if (!IsEmployeeViewsChangedByProjectsViewModel)
            {
                if (newItems != null)
                    foreach (var item in newItems)
                    {
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = true;
                        ViewModelsContainer.ProjectsViewModel
                                           .ProjectViews
                                           .FirstOrDefault(pV => pV.Id == ((EmplProjView)item).Id)
                                           .ProjEmplViews.Add(Mapper.Map<EmployeeView, ProjEmplView>(emplView));
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = false;
                    }
                if (oldItems != null)
                    foreach (var item in oldItems)
                    {
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = true;
                        ViewModelsContainer.ProjectsViewModel
                                           .ProjectViews
                                           .FirstOrDefault(pV => pV.Id == ((EmplProjView)item).Id)
                                           .ProjEmplViews.Remove(Mapper.Map<EmployeeView, ProjEmplView>(emplView));
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = false;
                    }
            }
        }
        void OnLeadProjViewsCountChanged(EmployeeView leaderView, IList newItems, IList oldItems)
        {
            if (!IsEmployeeViewsChangedByProjectsViewModel)
            {
                if (newItems != null)
                    foreach (var item in newItems)
                    {
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = true;
                        ViewModelsContainer.ProjectsViewModel
                                           .ProjectViews
                                           .First(pV => pV.Id == ((EmplProjView)item).Id)
                                           .ProjLeadView = Mapper.Map<ProjLeadView>(leaderView);
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = false;
                    }
                if (oldItems != null)
                    foreach (var item in oldItems)
                    {
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = true;
                        ViewModelsContainer.ProjectsViewModel
                                           .ProjectViews
                                           .First(pV => pV.Id == ((EmplProjView)item).Id)
                                           .ProjLeadView = null;
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChangedByEmployeesViewModel = false;
                    }
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
