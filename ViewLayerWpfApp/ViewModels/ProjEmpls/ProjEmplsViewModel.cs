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
using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.ProjEmpls
{
    public partial class ProjEmplsViewModel : INotifyPropertyChanged
    {
        #region Fields
        string title = "";
        ICommand dataLoadingCommand;
        ICommand saveChangesCommand;
        ICommand cancelChangesCommand;

        MessageBoxModel messageBoxVM;
        bool isProjEmplViewsChanged;
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
        public ObservableCollection<ProjEmplView> ProjEmplViews { get; set; } = new ObservableCollection<ProjEmplView>();
        public bool IsProjEmplViewsChanged
        {
            get { return isProjEmplViewsChanged; }
            set
            {
                isProjEmplViewsChanged = value;
                OnPropertyChanged("IsProjEmplViewsChanged");
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

        #region Supporting Properties
        [Import]
        IProjEmplsLogic ProjEmplsLogic { get; set; }
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
            ProjEmplsLogic.Login = Credentials.Login;
            ProjEmplsLogic.Password = Credentials.Password;

            Title = ProjectView.ProjName + ": Сотрудники";
            errCaption = "Сотрудники проекта: " + ProjectView.ProjName + " - Ошибка";
            List<Employee> employees = new List<Employee>();
            IEnumerable<ProjEmplView> projEmplViews;
            try
            {
				employees = ProjEmplsLogic.GetEmployees(); 
            }
            catch (Exception ex)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                    Caption = errCaption
                };
                Log.WriteLogAsync(ex);
                projEmplViews = new List<ProjEmplView>();
            }

            projEmplViews = Mapper.Map<IEnumerable<ProjEmplView>>(employees);

            foreach (var projEmplView in projEmplViews)
                if (ProjectView.ProjEmplViews.FirstOrDefault(pEV => pEV.Id == projEmplView.Id) == null)
                    projEmplView.IsChecked = false;

            foreach (var projEmplView in projEmplViews)
            {
                projEmplView.PropertyChanged += (s, e) => 
                    OnProjEmplViewChanged(projEmplView, e.PropertyName);
                ProjEmplViews.Add(projEmplView);
            }
        }
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = ProjectView.ProjName + ": Сотрудники",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                Project project = Mapper.Map<ProjectView, Project>(ProjectView);
                IEnumerable<ProjEmplView> projEmplViewsToAdd = ProjEmplViews.Where(pEV => pEV.IsChanged &&
                                                                                          pEV.IsChecked);
                IEnumerable<ProjEmplView> projEmplViewsToDelete = ProjEmplViews.Where(pEV => pEV.IsChanged &&
                                                                                             pEV.IsChecked == false);
                List<Employee> projsToAdd = Mapper.Map<IEnumerable<ProjEmplView>, List<Employee>>(projEmplViewsToAdd);
                List<Employee> projsToDelete = Mapper.Map<IEnumerable<ProjEmplView>, List<Employee>>(projEmplViewsToDelete);


                bool IsChangesSaved = false;
                try
                {
                    IsChangesSaved = ProjEmplsLogic.SetEmployeesToProject(project, projsToAdd, projsToDelete);
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
                    foreach (var projEmplView in projEmplViewsToAdd)
                        ProjectsViewModel.CurrentProjView.ProjEmplViews.Add(projEmplView);
                    foreach (var projEmplView in projEmplViewsToDelete)
                        ProjectsViewModel.CurrentProjView.ProjEmplViews.Remove(projEmplView);

                    foreach (var projEmplView in ProjEmplViews)
                    {
                        projEmplView.IsChanged = false;
                        projEmplView.BackupClear();
                    }
                    IsProjEmplViewsChanged = false;
                }
            }
        }
        void CancelChanges()
        {
            if (ProjEmplViews.Where(v => v.IsChanged == true).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = ProjectView.ProjName + ": Сотрудники",
                    Buttons = MessageBoxButton.YesNo
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                    foreach (var pEV in ProjEmplViews)
                        if (pEV.IsChanged)
                            pEV.IsChecked = pEV.Backup.IsChecked;
            }
        }
        #region Editing ProjEmplView
        void OnProjEmplViewChanged(ProjEmplView projEmplView, string propertyName)
        {
            if (propertyName == "IsChecked")
                if (projEmplView.IsChecked == projEmplView.Backup.IsChecked)
                {
                    projEmplView.IsChanged = false;
                    if (ViewModelsContainer.ProjEmplsViewModel.ProjEmplViews.FirstOrDefault(pEV => pEV.IsChanged) == null)
                        ViewModelsContainer.ProjEmplsViewModel.IsProjEmplViewsChanged = false;
                    projEmplView.Backup = null;
                }
                else
                {
                    projEmplView.IsChanged = true;
                    if (ViewModelsContainer.ProjEmplsViewModel.ProjEmplViews.FirstOrDefault(pEV => pEV.IsChanged) != null)
                        ViewModelsContainer.ProjEmplsViewModel.IsProjEmplViewsChanged = true;
                }
        }
        #endregion
        #endregion
    }
}

