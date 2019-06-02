using AutoMapper;
using ClientEntities;
using ILogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewLayerWpfApp.ViewModels.Employees;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.LeadProjs
{
    public partial class LeadProjsViewModel : INotifyPropertyChanged
    {
        #region Fields
        string title = "";
        ICommand dataLoadingCommand;
        ICommand saveChangesCommand;
        ICommand cancelChangesCommand;

        MessageBoxModel messageBoxVM;
        bool isLeadProjViewsChanged;
        #region Supporting Fields
        string errCaption = "{0} {1} {2}: Руководимые проекты: Ошибка";
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
        public ObservableCollection<EmplProjView> LeadProjViews { get; set; } = new ObservableCollection<EmplProjView>();
        public bool IsLeadProjViewsChanged
        {
            get { return isLeadProjViewsChanged; }
            set
            {
                isLeadProjViewsChanged = value;
                OnPropertyChanged("IsLeadProjViewsChanged");
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
        ILeadProjsLogic LeadProjsLogic { get; set; }
        public EmployeesViewModel EmployeesViewModel { get; set; }
        public EmployeeView LeaderView { get; set; }
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
            LeadProjsLogic.Login = Credentials.Login;
            LeadProjsLogic.Password = Credentials.Password;

            Title = LeaderView.Surname + " " + 
                    LeaderView.Name + " " + 
                    LeaderView.MiddleName + ": Руководимые проекты";
            errCaption = LeaderView.Surname + " " +
                         LeaderView.Name + " " +
                         LeaderView.MiddleName + ": Руководимые проекты - Ошибка";
            List<Project> projects = new List<Project>();
            IEnumerable<EmplProjView> leadProjViews;
            try
            {
				projects = LeadProjsLogic.GetProjects();
            }
            catch (Exception ex)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                    Caption = errCaption
                };
                Log.WriteLogAsync(ex);
                leadProjViews = new List<EmplProjView>();
            }

            leadProjViews = Mapper.Map<IEnumerable<EmplProjView>>(projects);
            foreach (var leadProjView in leadProjViews)
                if (LeaderView.LeadProjViews.FirstOrDefault(lPV => lPV.Id == leadProjView.Id) == null)
                    leadProjView.IsChecked = false;

            foreach (var leadProjView in leadProjViews)
            {
                leadProjView.PropertyChanged += (s, e) => OnLeadProjViewChanged(leadProjView, e.PropertyName);
                LeadProjViews.Add(leadProjView);
            }
        }
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = LeaderView.Surname + " " + LeaderView.Name + " " + LeaderView.MiddleName + ": Руководимые проекты",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                Employee leader = Mapper.Map<EmployeeView, Employee>(LeaderView);
                IEnumerable<EmplProjView> leadProjViewsToAdd = LeadProjViews.Where(lPV => lPV.IsChanged &&
                                                                                          lPV.IsChecked);
                IEnumerable<EmplProjView> leadProjViewsToDelete = LeadProjViews.Where(lPV => lPV.IsChanged &&
                                                                                      lPV.IsChecked == false);
                List<Project> projsToAdd = Mapper.Map<IEnumerable<EmplProjView>, List<Project>>(leadProjViewsToAdd);
                List<Project> projsToDelete = Mapper.Map<IEnumerable<EmplProjView>, List<Project>>(leadProjViewsToDelete);

                bool IsChangesSaved = false;
                try
                {
                    List<Project> projsHasLeaders = LeadProjsLogic.OtherLeadOnProjExistance(projsToAdd);
                    string projsHasLeadersString = "";
                    if (projsHasLeaders.Count > 0)
                    {
                        foreach (var projHasLeader in projsHasLeaders)
                            projsHasLeadersString += "- " + projHasLeader.ProjName + "\n";
                        MessageBoxVM = new MessageBoxModel()
                        {
                            Message = "У проектов:\n\n" + projsHasLeadersString + " уже есть руководитель. Переназначить им руководителя?",
                            Caption = LeaderView.Surname + " " + LeaderView.Name + " " + LeaderView.MiddleName + ":Руководимые проекты",
                            Buttons = MessageBoxButton.YesNo
                        };
                        if (MessageBoxVM.Result == MessageBoxResult.Yes)
                            IsChangesSaved = LeadProjsLogic.SetLeadProjectsToEmployee(leader, projsToAdd, projsToDelete);
                        else
                            IsChangesSaved = false;
                    }
                    else
                        IsChangesSaved = LeadProjsLogic.SetLeadProjectsToEmployee(leader, projsToAdd, projsToDelete);
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
                    foreach (var leadProjView in leadProjViewsToAdd)
                    {
                        foreach (var eV in EmployeesViewModel.EmployeeViews)
                            eV.LeadProjViews.Remove(eV.LeadProjViews.FirstOrDefault(lPV => lPV.Id == leadProjView.Id));
                        EmployeesViewModel.CurrentEmplView
                                            .LeadProjViews
                                            .Add(leadProjView);
                    }
                    foreach (var leadProjView in leadProjViewsToDelete)
                        EmployeesViewModel.CurrentEmplView
                                          .LeadProjViews
                                          .Remove(leadProjView);

                    foreach (var leadProjView in LeadProjViews)
                    {
                        leadProjView.IsChanged = false;
                        leadProjView.BackupClear();
                    }
                    IsLeadProjViewsChanged = false;
                }
            }
        }
        void CancelChanges()
        {
            if (LeadProjViews.Where(v => v.IsChanged == true).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = LeaderView.Surname + " " + LeaderView.Name + " " + LeaderView.MiddleName + ":Руководимые проекты",
                    Buttons = MessageBoxButton.YesNo
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                    foreach (var lPV in LeadProjViews)
                        if (lPV.IsChanged)
                            lPV.IsChecked = lPV.Backup.IsChecked;
            }
        }
        #region Supporting methods
        void OnLeadProjViewChanged(EmplProjView emplProjView, string propertyName)
        {
            if (propertyName == "IsChecked")
                if (emplProjView.IsChecked == emplProjView.Backup.IsChecked)
                {
                    emplProjView.IsChanged = false;
                    if (ViewModelsContainer.LeadProjsViewModel
                                           .LeadProjViews
                                           .FirstOrDefault(lPV => lPV.IsChanged) == null)
                        ViewModelsContainer.LeadProjsViewModel.IsLeadProjViewsChanged = false;
                    emplProjView.Backup = null;
                }
                else
                {
                    emplProjView.IsChanged = true;
                        if (ViewModelsContainer.LeadProjsViewModel
                                               .LeadProjViews
                                               .FirstOrDefault(lPV => lPV.IsChanged) != null)
                            ViewModelsContainer.LeadProjsViewModel.IsLeadProjViewsChanged = true;
                }
        }
        #endregion
        #endregion
    }
}