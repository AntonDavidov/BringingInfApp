using AutoMapper;
using ClientEntities;
using ILogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewLayerWpfApp.ViewModels;
using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.ProjLead
{
    public partial class ProjLeadViewModel : INotifyPropertyChanged
    {
        #region Fields
        string title = "";
        ICommand dataLoadingCommand;
        ICommand saveChangesCommand;
        ICommand cancelChangesCommand;

        MessageBoxModel messageBoxVM;
        bool isProjLeadViewsChanged;
        #region Supporting Fields
        string errCaption = "Сотрудники проекта: {0}: Ошибка";
        #endregion
        #endregion


        #region Properties
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        public ObservableCollection<ProjLeadView> ProjLeadViews { get; set; } = new ObservableCollection<ProjLeadView>();
        public bool IsProjLeadViewsChanged
        {
            get { return isProjLeadViewsChanged; }
            set
            {
                isProjLeadViewsChanged = value;
                OnPropertyChanged("IsProjLeadViewsChanged");
            }
        }
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

        public MessageBoxModel MessageBoxVM
        {
            get { return messageBoxVM; }
            set { messageBoxVM = value; OnPropertyChanged("MessageBoxVM"); }
        }

        #region SupportingProperties
        [Import]
        IProjLeadLogic ProjLeadLogic { get; set; }
        public ProjectsViewModel ProjectsViewModel { get; set; }
        public ProjectView ProjectView { get; set; }
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
            }
        }
        void DataLoading()
        {
            ProjLeadLogic.Login = Credentials.Login;
            ProjLeadLogic.Password = Credentials.Password;

            Title = ProjectView.ProjName + ": Выбор руководителя";
            errCaption = "Выбор руководителя: " + ProjectView.ProjName + " - Ошибка";
            List<Employee> employees = new List<Employee>();
            IEnumerable<ProjLeadView> projLeadViews;
            try
            {
				employees = ProjLeadLogic.GetEmployees();
            }
            catch (Exception ex)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                    Caption = errCaption
                };
                Log.WriteLogAsync(ex);
                projLeadViews = new List<ProjLeadView>();
            }

            projLeadViews = Mapper.Map<IEnumerable<ProjLeadView>>(employees);
            foreach (var projLeadView in projLeadViews)
            {
                if (ProjectView.ProjLeadView == null || ProjectView.ProjLeadView.Id != projLeadView.Id)
                    projLeadView.IsChecked = false;
                projLeadView.ProjLeadVM = this;
            }

            foreach (var projLeadView in projLeadViews)
            {
                projLeadView.PropertyChanged += (s, e) => OnProjLeadViewChanged(projLeadView, e.PropertyName);
                ProjLeadViews.Add(projLeadView);
            }
        }
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = ProjectView.ProjName + ": Выбор руководителя",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                Project project = Mapper.Map<ProjectView, Project>(ProjectView);
                ProjLeadView selectedProjLeadView = ProjLeadViews.FirstOrDefault(pLV=>pLV.IsChanged && pLV.IsChecked);
                ProjLeadView previousProjLeadView = ProjLeadViews.FirstOrDefault(pLV => pLV.IsChanged && !pLV.IsChecked);
                Employee selectedLeader = Mapper.Map<ProjLeadView, Employee>(selectedProjLeadView);

                bool IsChangesSaved = false;
                try
                {
                    IsChangesSaved = ProjLeadLogic.SetLeaderToProject(project, selectedLeader);
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
                    
                    if (ViewModelsContainer.EmployeesViewModel != null)
                    {
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = true;
                        if(previousProjLeadView != null)
                        ViewModelsContainer.EmployeesViewModel
                            .EmployeeViews
                            .First(eV => eV.Id == previousProjLeadView.Id)
                            .LeadProjViews.Remove(Mapper.Map<EmplProjs.EmplProjView>(ProjectsViewModel.CurrentProjView));
                        ViewModelsContainer.EmployeesViewModel.IsEmployeeViewsChangedByProjectsViewModel = false;
                    }
                    ProjectsViewModel.CurrentProjView.ProjLeadView = selectedProjLeadView;

                    foreach (var projLeadView in ProjLeadViews)
                    {
                        projLeadView.IsChanged = false;
                    }
                    IsProjLeadViewsChanged = false;
                }
            }
        }
        void CancelChanges()
        {
            if (ProjLeadViews.Where(v => v.IsChanged == true).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = ProjectView.ProjName + ": Выбор руководителя",
                    Buttons = MessageBoxButton.YesNo
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                    foreach (var pEV in ProjLeadViews)
                        if (pEV.Backup != null)
                            pEV.IsChecked = pEV.Backup.IsChecked;
            }
        }
        #region Supporting Methods
        #region Editing ProjLeadView
        void OnProjLeadViewChanged(ProjLeadView projLeadView, string propertyName)
        {
            if (propertyName == "IsChecked")
                if (projLeadView.IsChecked == projLeadView.Backup.IsChecked)
                {
                    projLeadView.IsChanged = false;
                    if (ViewModelsContainer.ProjLeadViewModel.ProjLeadViews.FirstOrDefault(pLV => pLV.IsChanged) == null)
                        ViewModelsContainer.ProjLeadViewModel.IsProjLeadViewsChanged = false;
                    projLeadView.Backup = null;
                }
                else
                {
                    projLeadView.IsChanged = true;
                    if (ViewModelsContainer.ProjLeadViewModel.ProjLeadViews.FirstOrDefault(pLV => pLV.IsChanged) != null)
                        ViewModelsContainer.ProjLeadViewModel.IsProjLeadViewsChanged = true;
                }
        }
        #endregion
        #endregion
        #endregion
    }
}
