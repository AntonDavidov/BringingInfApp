using AutoMapper;
using ClientEntities;
using ILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.Projects
{
    public partial class ProjectsViewModel : INotifyPropertyChanged
    {
        #region Fields
        ICommand dataLoadingCommand;
        #region Filters of Data, loaded into App
        string projNameFilter = "";
        string orgOrderNameFilter = "";
        string orgExecNameFilter = "";
        DateTime? upBeginningDateFilter;
        DateTime? lowBeginningDateFilter;
        DateTime? upEndDateFilter;
        DateTime? lowEndDateFilter;
        bool moreThenOrEqualsFilter;
        bool lessThenFilter;
        string priorityFilter = "";
        #endregion

        #region Filters of Data, loading from Db
        bool loadFromDb;
        ICommand loadFromDbCommand;
        bool loadFromDbInProgress;
        #endregion

        #region Filter Commands
        ICommand projNameChangedCommand;
        ICommand orgOrderNameChangedCommand;
        ICommand orgExecNameChangedCommand;
        ICommand upBeginnigDateChangedCommand;
        ICommand lowBeginnigDateChangedCommand;
        ICommand upEndDateChangedCommand;
        ICommand lowEndDateChangedCommand;
        ICommand priorityChangedCommand;
        ICommand moreThenOrEqualsCheckedCommand;
        ICommand lessThenCheckedCommand;

        #region GotFocus Commands
        ICommand projNameGotFocusCommand;
        ICommand orgOrderNameGotFocusCommand;
        ICommand orgExecNameGotFocusCommand;
        ICommand upBeginnigDateGotFocusCommand;
        ICommand lowBeginnigDateGotFocusCommand;
        ICommand upEndDateGotFocusCommand;
        ICommand lowEndDateGotFocusCommand;
        ICommand moreThenOrEqualsGotFocusCommand;
        ICommand lessThenGotFocusCommand;
        ICommand priorityGotFocusCommand;
        #endregion
        #endregion

        #region ViewModelCollection editing Commands
        ICommand addProjectViewCommand;
        ICommand delProjectViewCommand;
        #endregion

        #region Data editing Commands
        ICommand saveChangesCommand;
        bool saveChangesInProgress;
        ICommand cancelChangesCommand;
        bool cancelChangesInProgress;
        #endregion

        ProjectView currentProjView;
        IList selectedProjViews = null;
        MessageBoxModel messageBoxVM;
        bool isProjectViewsChanged;

		bool needAuthentication = false;
		bool needApplicationShutdown = false;

        #region Supporting Fiedls
        string FilterControlName;
        DateTime? upperDate = null;
        DateTime? lowerDate = null;
        string errCaption = "Проекты: Ошибка";
        #endregion
        #endregion


        #region Properties
        public ICommand DataLoadingCommand
        {
            get
            {
                if (dataLoadingCommand == null)
                    dataLoadingCommand = new RelayCommand(() =>
					{
						Compose();
						DataLoading();
					});
                return dataLoadingCommand;
            }
        }
        public ObservableCollection<ProjectView> ProjectViews { get; set; } = new ObservableCollection<ProjectView>();

        #region Filters of Data, loaded into App
        public string ProjNameFilter
        {
            get
            {
                return projNameFilter;
            }
            set
            {
                projNameFilter = value;
                OnPropertyChanged("ProjNameFilter");
            }
        }
        public string OrgOrderNameFilter
        {
            get { return orgOrderNameFilter; }
            set { orgOrderNameFilter = value; OnPropertyChanged("OrgOrderNameFilter"); }
        }
        public string OrgExecNameFilter
        {
            get { return orgExecNameFilter; }
            set { orgExecNameFilter = value; OnPropertyChanged("OrgExecNameFilter"); }
        }
        public DateTime? UpBeginningDateFilter
        {
            get { return upBeginningDateFilter; }
            set { upBeginningDateFilter = value; OnPropertyChanged("UpBeginningDateFilter"); }
        }
        public DateTime? LowBeginningDateFilter
        {
            get { return lowBeginningDateFilter; }
            set { lowBeginningDateFilter = value; OnPropertyChanged("LowBeginningDateFilter"); }
        }
        public DateTime? UpEndDateFilter
        {
            get { return upEndDateFilter; }
            set { upEndDateFilter = value; OnPropertyChanged("UpEndDateFilter"); }
        }
        public DateTime? LowEndDateFilter
        {
            get { return lowEndDateFilter; }
            set { lowEndDateFilter = value; OnPropertyChanged("LowEndDateFilter"); }
        }
        public bool MoreThenOrEqualsFilter
        {
            get { return moreThenOrEqualsFilter; }
            set { moreThenOrEqualsFilter = value; OnPropertyChanged("MoreThenOrEqualsFilter"); }
        }
        public bool LessThenFilter
        {
            get { return lessThenFilter; }
            set { lessThenFilter = value; OnPropertyChanged("LessThenFilter"); }
        }
        public string PriorityFilter
        {
            get { return priorityFilter; }
            set { priorityFilter = value; OnPropertyChanged("PriorityFilter"); }
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
        public bool LoadFromDbInProgress
        {
            get { return loadFromDbInProgress; }
            set { loadFromDbInProgress = value; OnPropertyChanged("LoadFromDbInProgress"); }
        }
        #endregion

        #region Filter Commands
        public ICommand ProjNameChangedCommand
        {
            get
            {
                if (projNameChangedCommand == null)
                    projNameChangedCommand = new RelayCommand(() => ProjNameChanged());
                return projNameChangedCommand;
            }
        }
        public ICommand OrgOrderNameChangedCommand
        {
            get
            {
                if (orgOrderNameChangedCommand == null)
                    orgOrderNameChangedCommand = new RelayCommand(() => OrgOrderNameChanged());
                return orgOrderNameChangedCommand;
            }
        }
        public ICommand OrgExecNameChangedCommand
        {
            get
            {
                if (orgExecNameChangedCommand == null)
                    orgExecNameChangedCommand = new RelayCommand(() => OrgExecNameChanged());
                return orgExecNameChangedCommand;
            }
        }
        public ICommand UpBeginnigDateChangedCommand
        {
            get
            {
                if (upBeginnigDateChangedCommand == null)
                    upBeginnigDateChangedCommand = new RelayCommand(() => UpBeginnigDateChanged());
                return upBeginnigDateChangedCommand;
            }
        }
        public ICommand LowBeginnigDateChangedCommand
        {
            get
            {
                if (lowBeginnigDateChangedCommand == null)
                    lowBeginnigDateChangedCommand = new RelayCommand(() => LowBeginnigDateChanged());
                return lowBeginnigDateChangedCommand;
            }
        }
        public ICommand UpEndDateChangedCommand
        {
            get
            {
                if (upEndDateChangedCommand == null)
                    upEndDateChangedCommand = new RelayCommand(() => UpEndDateChanged());
                return upEndDateChangedCommand;
            }
        }
        public ICommand LowEndDateChangedCommand
        {
            get
            {
                if (lowEndDateChangedCommand == null)
                    lowEndDateChangedCommand = new RelayCommand(() => LowEndDateChanged());
                return lowEndDateChangedCommand;
            }
        }
        public ICommand MoreThenOrEqualsCheckedCommand
        {
            get
            {
                if (moreThenOrEqualsCheckedCommand == null)
                    moreThenOrEqualsCheckedCommand = new RelayCommand(() => MoreThenOrEqualsChecked());
                return moreThenOrEqualsCheckedCommand;
            }
        }
        public ICommand LessThenCheckedCommand
        {
            get
            {
                if (lessThenCheckedCommand == null)
                    lessThenCheckedCommand = new RelayCommand(() => LessThenChecked());
                return lessThenCheckedCommand;
            }
        }
        public ICommand PriorityChangedCommand
        {
            get
            {
                if (priorityChangedCommand == null)
                    priorityChangedCommand = new RelayCommand(() => PriorityChanged());
                return priorityChangedCommand;
            }
        }
        #region GotFocus Commands
        public ICommand ProjNameGotFocusCommand
        {
            get
            {
                if (projNameGotFocusCommand == null)
                    projNameGotFocusCommand = new RelayCommand(() => ProjNameGotFocus());
                return projNameGotFocusCommand;
            }
        }
        public ICommand OrgOrderNameGotFocusCommand
        {
            get
            {
                if (orgOrderNameGotFocusCommand == null)
                    orgOrderNameGotFocusCommand = new RelayCommand(() => OrgOrderNameGotFocus());
                return orgOrderNameGotFocusCommand;
            }
        }
        public ICommand OrgExecNameGotFocusCommand
        {
            get
            {
                if (orgExecNameGotFocusCommand == null)
                    orgExecNameGotFocusCommand = new RelayCommand(() => OrgExecNameGotFocus());
                return orgExecNameGotFocusCommand;
            }
        }
        public ICommand UpBeginnigDateGotFocusCommand
        {
            get
            {
                if (upBeginnigDateGotFocusCommand == null)
                    upBeginnigDateGotFocusCommand = new RelayCommand(() => UpBeginnigDateGotFocus());
                return upBeginnigDateGotFocusCommand;
            }
        }
        public ICommand LowBeginnigDateGotFocusCommand
        {
            get
            {
                if (lowBeginnigDateGotFocusCommand == null)
                    lowBeginnigDateGotFocusCommand = new RelayCommand(() => LowBeginnigDateGotFocus());
                return lowBeginnigDateGotFocusCommand;
            }
        }
        public ICommand UpEndDateGotFocusCommand
        {
            get
            {
                if (upEndDateGotFocusCommand == null)
                    upEndDateGotFocusCommand = new RelayCommand(() => UpEndDateGotFocus());
                return upEndDateGotFocusCommand;
            }
        }
        public ICommand LowEndDateGotFocusCommand
        {
            get
            {
                if (lowEndDateGotFocusCommand == null)
                    lowEndDateGotFocusCommand = new RelayCommand(() => LowEndDateGotFocus());
                return lowEndDateGotFocusCommand;
            }
        }
        public ICommand MoreThenOrEqualsGotFocusCommand
        {
            get
            {
                if (moreThenOrEqualsGotFocusCommand == null)
                    moreThenOrEqualsGotFocusCommand = new RelayCommand(() => MoreThenOrEqualsGotFocus());
                return moreThenOrEqualsGotFocusCommand;
            }
        }
        public ICommand LessThenGotFocusCommand
        {
            get
            {
                if (lessThenGotFocusCommand == null)
                    lessThenGotFocusCommand = new RelayCommand(() => LessThenGotFocus());
                return lessThenGotFocusCommand;
            }
        }
        public ICommand PriorityGotFocusCommand
        {
            get
            {
                if (priorityGotFocusCommand == null)
                    priorityGotFocusCommand = new RelayCommand(() => PriorityGotFocus());
                return priorityGotFocusCommand;
            }
        }
        #endregion
        #endregion

        #region Table Editing Commands
        public ICommand AddProjectViewCommand
        {
            get
            {
                if (addProjectViewCommand == null)
                    addProjectViewCommand = new RelayCommand(() => AddProjectView());
                return addProjectViewCommand;
            }
        }
        public ICommand DelProjectViewCommand
        {
            get
            {
                if (delProjectViewCommand == null)
                    delProjectViewCommand = new RelayCommand(() => DelProjectView());
                return delProjectViewCommand;
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

        public ProjectView CurrentProjView
        {
            get { return currentProjView; }
            set { currentProjView = value; OnPropertyChanged("CurrentProjView"); }
        }
        public IList SelectedProjViews
        {
            get { return selectedProjViews; }
            set { selectedProjViews = value; OnPropertyChanged("SelectedProjViews"); }
        }
        public MessageBoxModel MessageBoxVM
        {
            get { return messageBoxVM; }
            set { messageBoxVM = value; OnPropertyChanged("MessageBoxVM"); }
        }
		public bool IsProjectViewsChanged
		{
			get { return isProjectViewsChanged; }
			set
			{
				isProjectViewsChanged = value;
				OnPropertyChanged("IsProjectViewsChanged");
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
        public IProjectsLogic ProjectsLogic { get; set; }
		public bool IsAuthenticate { get; set; }
		public bool IsProjectViewsChangedByEmployeesViewModel { get; set; } = false;
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


        #region Methods
		void Compose()
		{
			try
			{
				DIConfig.ComposeContainer.ComposeParts(this);
			}
			catch (CompositionException ex)
			{
				Log.WriteLogAsync(ex);
				MessageBoxVM = new MessageBoxModel()
				{
					Message = ErrorMessage.MakingMessageForMessageBox(ex),
					Caption = errCaption,
				};
                NeedApplicationShutdown = true;
			}
		}
        void DataLoading()
        {
            NeedAuthentication = true;
            if (Credentials.Login == null || Credentials.Password == null)
                DataLoading();
            else
            {
                ProjectsLogic.Login = Credentials.Login;
                ProjectsLogic.Password = Credentials.Password;

                IProgress<bool> queryExecutionProgress = new Progress<bool>(inProgress => LoadFromDbInProgress = inProgress);
                IProgress<ProjectView> GetProjectViewsProgress = new Progress<ProjectView>(projView => ProjectViews.Add(projView));
                IProgress<MessageBoxModel> messageReport = new Progress<MessageBoxModel>(messageBoxVM =>
                {
                    MessageBoxVM = messageBoxVM;

                    if (MessageBoxVM.Result == MessageBoxResult.Yes)
                        DataLoading();
                    else if (MessageBoxVM.Result == MessageBoxResult.No)
                        NeedApplicationShutdown = true;
                });

                Task.Run(() =>
                {
                    queryExecutionProgress.Report(true);
                    List<Project> projects = new List<Project>();
                    try
                    {
                        projects = ProjectsLogic.GetProjects();
                    }
                    catch (LogicDataQueryException ex)
                    {
                        if (ex.StatusCode == 401)
                        {
                            Log.WriteLogAsync(ex);
                            messageReport.Report(new MessageBoxModel()
                            {
                                Message = ErrorMessage.MakingMessageForMessageBox(ex),
                                Caption = errCaption,
                            });
                        }
                        else
                        {
                            messageReport.Report(new MessageBoxModel()
                            {
                                Message = ErrorMessage.MakingMessageForMessageBox(ex),
                                Caption = errCaption,
                            });
                        }
                    }
                    if (projects.Count > 0)
                    {
                        Mapper.Map<List<ProjectView>>(projects)
                            .ForEach(pV =>
                            {
                                pV.PropertyChanged += (s, e) =>
                                        OnProjectViewChanged(pV, e.PropertyName);
                                pV.ProjEmplViews.CollectionChanged += (s, e) =>
                                    OnProjEmplViewsCountChanged(pV, e.NewItems, e.OldItems);
                                GetProjectViewsProgress.Report(pV);
                            });
                    }

                    queryExecutionProgress.Report(false);
                });

                CurrentProjView = ProjectViews.FirstOrDefault();
            }
        }

        #region Filters of Data, loaded into App
        void ProjNameChanged()
        {
            if (LoadFromDb == false)
            {
                if (ProjNameFilter != "")
                {
                    foreach (var pV in ProjectViews)
                        if (pV.ProjName.StartsWith(ProjNameFilter))
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void OrgOrderNameChanged()
        {
            if (LoadFromDb == false)
            {
                if (OrgOrderNameFilter != "")
                {
                    foreach (var pV in ProjectViews)
                        if (pV.OrgOrderName.StartsWith(OrgOrderNameFilter))
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void OrgExecNameChanged()
        {
            if (LoadFromDb == false)
            {
                if (OrgExecNameFilter != "")
                {
                    foreach (var pV in ProjectViews)
                        if (pV.OrgExecuteName.StartsWith(OrgExecNameFilter))
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void UpBeginnigDateChanged()
        {
            if (LoadFromDb == false)
            {
                upperDate = upBeginningDateFilter;
                lowerDate = LowBeginningDateFilter;
                if (upperDate != null && lowerDate != null)
                {
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin <= upperDate &&
                            pV.DateProjExecuteBegin >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    if (upperDate != null && lowerDate == null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin <= upperDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                                        if (upperDate == null && lowerDate != null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void LowBeginnigDateChanged()
        {
            if (LoadFromDb == false)
            {
                upperDate = upBeginningDateFilter;
                lowerDate = LowBeginningDateFilter;
                if (upperDate != null && lowerDate != null)
                {
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin <= upperDate &&
                            pV.DateProjExecuteBegin >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    if (upperDate != null && lowerDate == null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin <= upperDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                                        if (upperDate == null && lowerDate != null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteBegin >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void UpEndDateChanged()
        {
            if (LoadFromDb == false)
            {
                upperDate = upEndDateFilter;
                lowerDate = LowEndDateFilter;
                if (upperDate != null && lowerDate != null)
                {
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd <= upperDate &&
                            pV.DateProjExecuteEnd >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    if (upperDate != null && lowerDate == null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd <= upperDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                                        if (upperDate == null && lowerDate != null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }
        void LowEndDateChanged()
        {
            if (LoadFromDb == false)
            {
                upperDate = upEndDateFilter;
                lowerDate = LowEndDateFilter;
                if (upperDate != null && lowerDate != null)
                {
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd <= upperDate &&
                            pV.DateProjExecuteEnd >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                }
                else
                    if (upperDate != null && lowerDate == null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd <= upperDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                                        if (upperDate == null && lowerDate != null)
                    foreach (var pV in ProjectViews)
                        if (pV.DateProjExecuteEnd >= lowerDate)
                            pV.IsFiltered = false;
                        else
                            pV.IsFiltered = true;
                else
                    foreach (var pV in ProjectViews)
                        pV.IsFiltered = false;
            }
        }

        #region Filter By Prioryty
        void PriorityChanged()
        {
            if (LoadFromDb == false)
            {
                int priority;
                if (PriorityFilter != "")
                    if (int.TryParse(PriorityFilter, out priority))
                    {
                        if (MoreThenOrEqualsFilter == true)
                            foreach (var pV in ProjectViews)
                                if (pV.Priority >= priority)
                                    pV.IsFiltered = false;
                                else
                                    pV.IsFiltered = true;
                        else
                        if (LessThenFilter == true)
                            foreach (var pV in ProjectViews)
                                if (pV.Priority < priority)
                                    pV.IsFiltered = false;
                                else
                                    pV.IsFiltered = true;
                        else
                            foreach (var pV in ProjectViews)
                                if (pV.Priority < priority)
                                    pV.IsFiltered = false;
                    }
                    else
                    {
                        MessageBoxVM = new MessageBoxModel()
                        {
                            Message = "Необходимо ввести цифру.",
                            Caption = "Проекты"
                        };
                        PriorityFilter = "";
                    }
                else
                    foreach (var pV in ProjectViews)
                            pV.IsFiltered = false;
            }
        }
        void MoreThenOrEqualsChecked()
        {
            if (LoadFromDb == false)
            {
                int priority;
                if (int.TryParse(PriorityFilter, out priority))
                {
                    if (MoreThenOrEqualsFilter == true)
                        foreach (var pV in ProjectViews)
                            if (pV.Priority >= priority)
                                pV.IsFiltered = false;
                            else
                                pV.IsFiltered = true;
                    else
                    if (LessThenFilter == true)
                        foreach (var pV in ProjectViews)
                            if (pV.Priority < priority)
                                pV.IsFiltered = false;
                            else
                                pV.IsFiltered = true;
                    else
                        foreach (var pV in ProjectViews)
                            if (pV.Priority < priority)
                                pV.IsFiltered = false;
                }
            }
        }
        void LessThenChecked()
        {
            if (LoadFromDb == false)
            {
                int priority;
                if (int.TryParse(PriorityFilter, out priority))
                {
                    if (MoreThenOrEqualsFilter == true)
                        foreach (var pV in ProjectViews)
                            if (pV.Priority >= priority)
                                pV.IsFiltered = false;
                            else
                                pV.IsFiltered = true;
                    else
                    if (LessThenFilter == true)
                        foreach (var pV in ProjectViews)
                            if (pV.Priority < priority)
                                pV.IsFiltered = false;
                            else
                                pV.IsFiltered = true;
                    else
                        foreach (var pV in ProjectViews)
                            if (pV.Priority < priority)
                                pV.IsFiltered = false;
                }
            }
        }
        #endregion

        #region GotFocus Methods
        void ProjNameGotFocus()
        {
            FilterControlName = "ProjNameFilter";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void OrgOrderNameGotFocus()
        {
            FilterControlName = "OrgOrderNameFilter";
            ProjNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void OrgExecNameGotFocus()
        {
            FilterControlName = "OrgExecNameFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void UpBeginnigDateGotFocus()
        {
            FilterControlName = "upBeginningDateFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void LowBeginnigDateGotFocus()
        {
            FilterControlName = "LowBeginningDateFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void UpEndDateGotFocus()
        {
            FilterControlName = "UpEndDateFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void LowEndDateGotFocus()
        {
            FilterControlName = "LowEndDateFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            PriorityFilter = "";
            MoreThenOrEqualsFilter = false;
            LessThenFilter = false;
        }
        void MoreThenOrEqualsGotFocus()
        {
            FilterControlName = "MoreThenOrEqualsFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
        }
        void LessThenGotFocus()
        {
            FilterControlName = "LessThenFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
        }
        void PriorityGotFocus()
        {
            FilterControlName = "PriorityFilter";
            ProjNameFilter = "";
            OrgOrderNameFilter = "";
            OrgExecNameFilter = "";
            UpBeginningDateFilter = null;
            LowBeginningDateFilter = null;
            UpEndDateFilter = null;
            LowEndDateFilter = null;
            if (MoreThenOrEqualsFilter == false && LessThenFilter == false)
                MoreThenOrEqualsFilter = true;
        }
        #endregion
        #endregion

        #region Filters of Data, loading from Db
        void FilterViewFromDB()
        {
            if (LoadFromDb == true)
            {
                IProgress<bool> progress = new Progress<bool>(inProgress => LoadFromDbInProgress = inProgress);
                IProgress<ProjectView> progressResult = new Progress<ProjectView>(projView => ProjectViews.Add(projView));
                IProgress<MessageBoxModel> progressMessage = new Progress<MessageBoxModel>(messageBoxVM => MessageBoxVM = messageBoxVM);

                if (ProjectViews.Where(pV => pV.IsAdded == true).Count() > 0 ||
                ProjectViews.Where(pV => pV.IsChanged == true).Count() > 0 ||
                ProjectViews.Where(pV => pV.IsDeleted == true).Count() > 0)
                {
                    MessageBoxVM = new MessageBoxModel()
                    {
                        Message = "Все внесённые изменения будут удалены. Продолжить?",
                        Caption = "Проекты",
                        Buttons = MessageBoxButton.YesNo
                    };
                    if (MessageBoxVM.Result == MessageBoxResult.Yes)
                    {
                        while (ProjectViews.Count > 0) ProjectViews.Remove(ProjectViews.First());
                        Task.Run(() =>
                        {
                            try
                            {
                                progress.Report(true);

                                List<Project> projects = new List<Project>();
                                projects = LoadFilteredDataFromDb(FilterControlName);
                                Mapper.Map<List<ProjectView>>(projects)
                                .ForEach(pV =>
                                {
                                    pV.PropertyChanged += (s, e) => OnProjectViewChanged(pV, e.PropertyName);
                                    pV.ProjEmplViews.CollectionChanged += (s, e) =>
                                               OnProjEmplViewsCountChanged(pV, e.NewItems, e.OldItems);
                                    progressResult.Report(pV);
                                });

                                progress.Report(false);
                            }
                            catch (Exception ex)
                            {
                                progressMessage.Report(new MessageBoxModel()
                                {
                                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                                    Caption = errCaption
                                });
                                Log.WriteLogAsync(ex);
                            }
                        });
                    }
                }
                else
                {
                    while (ProjectViews.Count > 0) ProjectViews.Remove(ProjectViews.First());
                    Task.Run(() =>
                    {
                        try
                        {
                            List<Project> projects = new List<Project>();
                            progress.Report(true);
                            projects = LoadFilteredDataFromDb(FilterControlName);
                            Mapper.Map<List<ProjectView>>(projects)
                            .ForEach(pV =>
                            {
                                pV.PropertyChanged += (s, e) => OnProjectViewChanged(pV, e.PropertyName);
                                pV.ProjEmplViews.CollectionChanged += (s, e) =>
                                           OnProjEmplViewsCountChanged(pV, e.NewItems, e.OldItems);
                                progressResult.Report(pV);
                            });
                            progress.Report(false);
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
                    });
                }

                CurrentProjView = ProjectViews.FirstOrDefault();
            }
        }
        #endregion

        #region ViewModelCollection editing Methods
        void AddProjectView()
        {
            ProjectViews.Add(new ProjectView());
            ProjectViews.Last().IsAdded = true;
            if (ProjectViews.FirstOrDefault(pV => pV.IsAdded) != null)
                IsProjectViewsChanged = true;
        }
        void DelProjectView()
        {
            if (SelectedProjViews != null)
            {
                IEnumerable<ProjectView> projViewsToDel = SelectedProjViews.Cast<ProjectView>();
                while (projViewsToDel.Where(i => i.IsAdded).Count() > 0)
                    ProjectViews.Remove(projViewsToDel.First(i => i.IsAdded));

                foreach (var eV in projViewsToDel)
                {
                    if (eV.IsAdded == false)
                        eV.IsDeleted = true;
                }
                if (ProjectViews.FirstOrDefault(pV => pV.IsAdded || pV.IsDeleted) != null)
                    IsProjectViewsChanged = true;
                else
                    IsProjectViewsChanged = false;
            }
        }
        #endregion

        #region Data editing Methods
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = "Проекты",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                List<Project> projsToAdd = Mapper.Map<IEnumerable<ProjectView>, List<Project>>(ProjectViews.Where(pV => pV.IsAdded));
                List<Project> projsToUpdate = Mapper.Map<IEnumerable<ProjectView>, List<Project>>(ProjectViews.Where(pV => pV.IsChanged));
                List<Project> projsToDelete = Mapper.Map<IEnumerable<ProjectView>, List<Project>>(ProjectViews.Where(pV => pV.IsDeleted));

                IProgress<bool> queryExecutionProgress = new Progress<bool>(inProgress => LoadFromDbInProgress = inProgress);
                IProgress<IEnumerable<Project>> setIdsProgress = new Progress<IEnumerable<Project>>(projIds =>
                {
                    IEnumerator<Project> projIdEnumerator = projIds.GetEnumerator();

                    foreach (var pV in ProjectViews)
                    {
                        projIdEnumerator.MoveNext();
                        pV.Id = projIdEnumerator.Current.Id;
                        if (pV.IsAdded == true) pV.ProjEmplViews = new ObservableCollection<ProjEmplView>();
                        pV.IsAdded = false;
                        pV.IsChanged = false;
                    }

                    while (ProjectViews.Where(pV => pV.IsDeleted).Count() > 0)
                    {
                        ProjectView projView = ProjectViews.First(pV => pV.IsDeleted);
                        if (ViewModelsContainer.EmployeesViewModel != null)
                            foreach (var eV in ViewModelsContainer.EmployeesViewModel.EmployeeViews)
                            {
                                eV.EmplProjViews.Remove(Mapper.Map<EmplProjView>(projView));
                                eV.LeadProjViews.Remove(Mapper.Map<EmplProjView>(projView));
                            }
                        ProjectViews.Remove(projView);
                    }
                    IsProjectViewsChanged = false;
                });
                IProgress<ProjectView> progressRemovePV = new Progress<ProjectView>(projView => ProjectViews.Remove(projView));
                IProgress<MessageBoxModel> progressMessage = new Progress<MessageBoxModel>(messageBoxVM => MessageBoxVM = messageBoxVM);

                Task.Run(() =>
                {
                    IEnumerable<Project> projIds;
                    queryExecutionProgress.Report(true);
                    try
                    {
                        if (ProjectsLogic.SetProjects(projsToAdd, projsToUpdate, projsToDelete))
                            projIds = ProjectsLogic.GetProjIDs().OrderBy(p => p.Id);
                        else
                            projIds = null;
                    }
                    catch (Exception ex)
                    {
                        progressMessage.Report(new MessageBoxModel()
                        {
                            Message = ErrorMessage.MakingMessageForMessageBox(ex),
                            Caption = errCaption
                        });
                        Log.WriteLogAsync(ex);
                        projIds = null;
                    }
                    if (projIds != null)
                    {
                        setIdsProgress.Report(projIds);
                    }
                    queryExecutionProgress.Report(false);
                });
            }
        }
        void CancelChanges()
        {
            if (ProjectViews.Where(pV => pV.IsAdded == true).Count() > 0 ||
                ProjectViews.Where(pV => pV.IsChanged == true).Count() > 0 ||
                ProjectViews.Where(pV => pV.IsDeleted == true).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = "Проекты",
                    Buttons = MessageBoxButton.YesNo
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                {
                    int numOfAdded = 0;
                    foreach (var pV in ProjectViews)
                    {
                        if (pV.IsAdded) numOfAdded++;
                        if (pV.IsChanged)
                        {
                            pV.ProjName = pV.Backup.ProjName;
                            if (pV.IsChanged) pV.OrgOrderName = pV.Backup.OrgOrderName;
                            if (pV.IsChanged) pV.OrgExecuteName = pV.Backup.OrgExecuteName;
                            if (pV.IsChanged) pV.DateProjExecuteBegin = pV.Backup.DateProjExecuteBegin;
                            if (pV.IsChanged) pV.DateProjExecuteEnd = pV.Backup.DateProjExecuteEnd;
                            if (pV.IsChanged) pV.Priority = pV.Backup.Priority;
                            if (pV.IsChanged) pV.Comment = pV.Backup.Comment;
                        }
                        if (pV.IsDeleted)
                            pV.IsDeleted = false;
                    }
                    while (numOfAdded > 0)
                    {
                        ProjectViews.Remove(ProjectViews.First(eV => eV.IsAdded));
                        numOfAdded--;
                    }

                    if (ProjectViews.FirstOrDefault(pV => pV.IsAdded || pV.IsDeleted) != null)
                        IsProjectViewsChanged = true;
                    else
                        IsProjectViewsChanged = false;
                }
            }
        }
        #endregion

        #region Supporting Methods
        List<Project> LoadFilteredDataFromDb(string filterControlName)
        {
            switch (filterControlName)
            {
                case "ProjNameFilter":
                    return ProjectsLogic.GetProjectsByNameStartWith(ProjNameFilter);
                case "OrgOrderNameFilter":
                    return ProjectsLogic.GetProjectsByOrgOrderNameStartWith(OrgOrderNameFilter);
                case "OrgExecNameFilter":
                    return ProjectsLogic.GetProjectsByOrgExecuteNameStartWith(OrgExecNameFilter);
                case "upBeginningDateFilter":
                    return ProjectsLogic.GetProjectsByDateProjExecuteBeginRange(
                        LowBeginningDateFilter,
                        upBeginningDateFilter);
                case "LowBeginningDateFilter":
                    return ProjectsLogic.GetProjectsByDateProjExecuteBeginRange(
                        LowBeginningDateFilter,
                        upBeginningDateFilter);
                case "UpEndDateFilter":
                    return ProjectsLogic.GetProjectsByDateProjExecuteEndRange(
                        LowEndDateFilter,
                        UpEndDateFilter);
                case "LowEndDateFilter":
                    return ProjectsLogic.GetProjectsByDateProjExecuteEndRange(
                                LowEndDateFilter,
                                UpEndDateFilter);
                case "MoreThenOrEqualsFilter":
                    {
                        int priority;
                        if (int.TryParse(PriorityFilter, out priority))
                            return ProjectsLogic.GetProjectsByPriorityEqualsOrMoreThen(priority);
                        else
                        {
                            MessageBoxVM = new MessageBoxModel()
                            {
                                Message = "Необходимо ввести числовое значение",
                                Caption = "Проекты",
                            };
                            return null;
                        }
                    }
                case "LessThenFilter":
                    {
                        int priority;
                        if (int.TryParse(PriorityFilter, out priority))
                            return ProjectsLogic.GetProjectsByPriorityLessThen(priority);
                        else
                        {
                            MessageBoxVM = new MessageBoxModel()
                            {
                                Message = "Необходимо ввести числовое значение",
                                Caption = "Проекты",
                            };
                            return null;
                        }
                    }
                case "PriorityFilter":
                    {
                        int priority;
                        if (int.TryParse(PriorityFilter, out priority))
                        {
                            if (MoreThenOrEqualsFilter == true)
                                return ProjectsLogic.GetProjectsByPriorityEqualsOrMoreThen(priority);
                            else
                            if (LessThenFilter == true)
                                return ProjectsLogic.GetProjectsByPriorityLessThen(priority);
                            else
                                return ProjectsLogic.GetProjects();
                        }
                        else
                        {
                            MessageBoxVM = new MessageBoxModel()
                            {
                                Message = "Необходимо ввести числовое значение",
                                Caption = "Проекты",
                            };
                            return null;
                        }
                    }
                default:
                    return ProjectsLogic.GetProjects(); 
            }
        }
        #region Editing of ProjectView
        void OnProjectViewChanged(ProjectView pV, string propertyName)
        {
            if (propertyName != "Id" &&
                propertyName != "ProjEmplViews" &&
                propertyName != "ProjLeadView" &&
                propertyName != "HasALeader" &&
                propertyName != "IsAdded" &&
                propertyName != "IsChanged" &&
                propertyName != "IsDeleted" &&
                propertyName != "IsFiltered" &&
                pV.IsAdded == false)
            {
                if (pV.ProjName == pV.Backup.ProjName &&
                    pV.OrgOrderName == pV.Backup.OrgOrderName &&
                    pV.OrgExecuteName == pV.Backup.OrgExecuteName &&
                    pV.DateProjExecuteBegin == pV.Backup.DateProjExecuteBegin &&
                    pV.DateProjExecuteEnd == pV.Backup.DateProjExecuteEnd &&
                    pV.Priority == pV.Backup.Priority &&
                    pV.Comment == pV.Backup.Comment)
                {
                    pV.IsChanged = false;
                    if (ViewModelsContainer.ProjectsViewModel.ProjectViews.FirstOrDefault(projView => projView.IsChanged) == null)
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChanged = false;
                    pV.Backup = null;
                }
                else
                {
                    pV.IsChanged = true;
                    if (ViewModelsContainer.ProjectsViewModel.ProjectViews.FirstOrDefault(projView => projView.IsChanged) != null)
                        ViewModelsContainer.ProjectsViewModel.IsProjectViewsChanged = true;
                }
            }
            if (propertyName == "ProjLeadView")
            {
                if (ViewModelsContainer.EmployeesViewModel != null && !IsProjectViewsChangedByEmployeesViewModel)
                    if (pV.ProjLeadView != null)
                    {
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = true;
                        ViewModelsContainer.EmployeesViewModel
                                           .EmployeeViews
                                           .First(eV => eV.Id == pV.ProjLeadView.Id)
                                           .LeadProjViews
                                           .Add(Mapper.Map<EmplProjView>(pV));
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = false;
                    }
            }
        }
        void OnProjEmplViewsCountChanged(ProjectView pV, IList newItems, IList oldItems)
        {
            if (ViewModelsContainer.EmployeesViewModel != null && !IsProjectViewsChangedByEmployeesViewModel)
            {
                if (newItems != null)
                    foreach (var item in newItems)
                    {
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = true;
                        ViewModelsContainer.EmployeesViewModel.EmployeeViews
                                                              .FirstOrDefault(eV => eV.Id == ((ProjEmplView)item).Id)
                                                              .EmplProjViews
                                                              .Add(Mapper.Map<ProjectView, EmplProjView>(pV));
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = false;
                    }
                if (oldItems != null)
                    foreach (var item in oldItems)
                    {
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = true;
                        ViewModelsContainer.EmployeesViewModel.EmployeeViews
                                                              .FirstOrDefault(eV => eV.Id == ((ProjEmplView)item).Id)
                                                              .EmplProjViews
                                                              .Remove(Mapper.Map<ProjectView, EmplProjView>(pV));
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = false;
                    }
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
