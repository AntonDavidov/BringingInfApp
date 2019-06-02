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
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.EmplProjs
{
    public partial class EmplProjsViewModel : INotifyPropertyChanged
    {
        #region Fields
        string title = "";
        ICommand dataLoadingCommand;
        ICommand saveChangesCommand;
        ICommand cancelChangesCommand;

        MessageBoxModel messageBoxVM;
        bool isEmplProjViewsChanged;
        #region Supporting Fields
        string errCaption;
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
        public ObservableCollection<EmplProjView> EmplProjViews { get; set; } = new ObservableCollection<EmplProjView>();
        public bool IsEmplProjViewsChanged
        {
            get { return isEmplProjViewsChanged; }
            set
            {
                isEmplProjViewsChanged = value;
                OnPropertyChanged("IsEmplProjViewsChanged");
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
        IEmplProjsLogic EmplProjsLogic { get; set; }
        public EmployeesViewModel EmployeesViewModel { get; set; }
        public EmployeeView EmployeeView { get; set; }

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
            EmplProjsLogic.Login = Credentials.Login;
            EmplProjsLogic.Password = Credentials.Password;

            Title = EmployeeView.Surname + " " +
                    EmployeeView.Name + " " +
                    EmployeeView.MiddleName + ": Проекты";
            errCaption = EmployeeView.Surname + " " +
                         EmployeeView.Name + " " +
                         EmployeeView.MiddleName + ": Проекты - Ошибка";
            List<Project> projects = new List<Project>();
            IEnumerable<EmplProjView> emplProjViews;
            try
            {
                projects = EmplProjsLogic.GetProjects();
            }
            catch (Exception ex)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = ErrorMessage.MakingMessageForMessageBox(ex),
                    Caption = errCaption
                };
                Log.WriteLogAsync(ex);
                emplProjViews = new List<EmplProjView>();
            }

            emplProjViews = Mapper.Map<IEnumerable<EmplProjView>>(projects);

            foreach (var emplProjView in emplProjViews)
                if (EmployeeView.EmplProjViews.FirstOrDefault(ePV => ePV.Id == emplProjView.Id) == null)
                    emplProjView.IsChecked = false;

            foreach (var emplProjView in emplProjViews)
            {
                emplProjView.PropertyChanged += (s, e) => OnEmplProjViewChanged(emplProjView, e.PropertyName);
                EmplProjViews.Add(emplProjView);
            }
        }
        void SaveChanges()
        {
            MessageBoxVM = new MessageBoxModel()
            {
                Message = "Выполнить сохранение всех внесённых изменений?",
                Caption = EmployeeView.Surname + " " + EmployeeView.Name + " " + EmployeeView.MiddleName + ": Проекты",
                Buttons = MessageBoxButton.YesNo
            };
            if (MessageBoxVM.Result == MessageBoxResult.Yes)
            {
                Employee employee = Mapper.Map<EmployeeView, Employee>(EmployeeView);
                IEnumerable<EmplProjView> emplProjViewsToAdd = EmplProjViews.Where(ePV => ePV.IsChanged &&
                                                                                          ePV.IsChecked);
                IEnumerable<EmplProjView> emplProjViewsToDelete = EmplProjViews.Where(ePV => ePV.IsChanged &&
                                                                                             ePV.IsChecked == false);
                List<Project> projsToAdd = Mapper.Map<IEnumerable<EmplProjView>, List<Project>>(emplProjViewsToAdd);
                List<Project> projsToDelete = Mapper.Map<IEnumerable<EmplProjView>, List<Project>>(emplProjViewsToDelete);

                bool IsChangesSaved = false;
                try
                {
                    IsChangesSaved = EmplProjsLogic.SetProjectsToEmployee(employee, projsToAdd, projsToDelete);
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
                    foreach (var emplProjView in emplProjViewsToAdd)
                        EmployeesViewModel.CurrentEmplView
                                          .EmplProjViews
                                          .Add(emplProjView);
                    foreach (var emplProjView in emplProjViewsToDelete)
                        EmployeesViewModel.CurrentEmplView
                                          .EmplProjViews
                                          .Remove(emplProjView);

                    foreach (var emplProjView in EmplProjViews)
                    {
                        emplProjView.IsChanged = false;
                        emplProjView.BackupClear();
                    }
                    IsEmplProjViewsChanged = false;
                }
            }
        }
        void CancelChanges()
        {
            if (EmplProjViews.Where(v => v.IsChanged == true).Count() > 0)
            {
                MessageBoxVM = new MessageBoxModel()
                {
                    Message = "Все внесённые изменения будут удалены. Продолжить?",
                    Caption = EmployeeView.Surname + " " + EmployeeView.Name + " " + EmployeeView.MiddleName + ": Проекты",
                    Buttons = MessageBoxButton.YesNo
                };
                if (MessageBoxVM.Result == MessageBoxResult.Yes)
                    foreach (var ePV in EmplProjViews)
                        if (ePV.IsChanged)
                            ePV.IsChecked = ePV.Backup.IsChecked;
            }
        }
        #region Supporting Methods
        void OnEmplProjViewChanged(EmplProjView emplProjView, string propertyName)
        {
            if (propertyName == "IsChecked")
                if (emplProjView.IsChecked == emplProjView.Backup.IsChecked)
                {
                    emplProjView.IsChanged = false;
                        if (ViewModelsContainer.EmplProjsViewModel
                                               .EmplProjViews
                                               .FirstOrDefault(ePV => ePV.IsChanged) == null)
                            ViewModelsContainer.EmplProjsViewModel.IsEmplProjViewsChanged = false;
                    emplProjView.Backup = null;
                }
                else
                {
                    emplProjView.IsChanged = true;
                        if (ViewModelsContainer.EmplProjsViewModel
                                               .EmplProjViews
                                               .FirstOrDefault(ePV => ePV.IsChanged) != null)
                            ViewModelsContainer.EmplProjsViewModel.IsEmplProjViewsChanged = true;
                }
        }
        #endregion
        #endregion
    }
}
