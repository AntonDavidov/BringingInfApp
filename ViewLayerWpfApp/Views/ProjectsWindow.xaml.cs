using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ViewLayerWpfApp.ViewModels;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels.Employees;
using ViewLayerWpfApp.ViewModels.Projects;
using ViewLayerWpfApp.ViewModels.ProjEmpls;
using ViewLayerWpfApp.ViewModels.ProjLead;

namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика работы окна ProjectsWindow.xaml
    /// </summary>
    public partial class ProjectsWindow : Window
    {
		#region Fields
		ProjectsViewModel pVM;
		bool employeesWindowOpen = false;
		bool tryingAuth = false;
        #endregion


        #region Constructor
        public ProjectsWindow()
        {
            InitializeComponent();
			Left = SystemParameters.VirtualScreenWidth / 2 - Width / 2;
			Top = SystemParameters.VirtualScreenHeight / 2 - Height / 2;
			pVM = (ProjectsViewModel)this.DataContext;
			pVM.PropertyChanged += (s, eArgs) =>
			{
				if (eArgs.PropertyName == "NeedAuthentication")
				{
					if (pVM.NeedAuthentication)
					{
						LoginWindow loginWindow = new LoginWindow();
						if (tryingAuth)
							loginWindow.NeedAuthenticationMessage = true;
						loginWindow.ShowDialog();
						if (loginWindow.DialogResult == true)
							tryingAuth = true;
					}
				}
				if (eArgs.PropertyName == "MessageBoxVM")
				{
					pVM.MessageBoxVM.Result = MessageBox.Show(pVM.MessageBoxVM.Message,
															  pVM.MessageBoxVM.Caption,
															  pVM.MessageBoxVM.Buttons);
				}
				if (eArgs.PropertyName == "NeedApplicationShutdown")
						App.Current.Shutdown();
				ViewModelsContainer.ProjectsViewModel = (ProjectsViewModel)this.DataContext;
			};
        }
        #endregion


        #region Methods

        void menuItemEmployees_Click(object sender, RoutedEventArgs e)
        {
            if (!employeesWindowOpen)
            {
                EmployeesWindow employeesWindow = new EmployeesWindow();
                ViewModelsContainer.EmployeesViewModel = (EmployeesViewModel)employeesWindow.DataContext;
                employeesWindow.Show();
                employeesWindowOpen = true;
                employeesWindow.Closed += (s, eArgs) => 
                {
                    employeesWindowOpen = false;
                    menuItemEmployees.IsEnabled = true;
                    ViewModelsContainer.EmployeesViewModel = null;
                };
                menuItemEmployees.IsEnabled = false;
            }
        }
        void menuItemHelp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("В ПО реализована возможность добавления/редактирования/удаления" +
                " данных прямо в таблицах путём использования кнопок [Добавить] и [Удалить] под таблицей" +
                " и использования контекстного меню таблиц с пунктами [Добавить] и [Удалить]. После" +
                " добавления и редактирования данных строки подсвечиваются серым цветом. Для сохранения" +
                " всех внесённых изменений необходимо нажать кнопку [Сохранить изменения], для" +
                " отмены - [Отменить изменения].\n\n Фильтр данных работает как в режиме подгрузки данных из базы" +
                " данных, так и с данными, уже загруженными в клиентское приложение. Изменение режима происходит" +
                " при помощи установки или отмены галочки [Подгружать данные из базы данных]. В режиме подгрузки" +
                " данных фильтрация работет только по нажатию кнопки [Отобразить].", "Помощь");
        }

        void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.CurrentCell.Column.Header.ToString() == "Кол-во сотрудников")
            {
                if (((ProjectView)dataGrid.CurrentItem).IsAdded != true)
                {
                    ProjEmplsWindow projEmplBindingsWindow;
                    projEmplBindingsWindow = new ProjEmplsWindow();
                    ((ProjEmplsViewModel)projEmplBindingsWindow.DataContext).ProjectView = (ProjectView)dataGrid.CurrentCell.Item;
                    ((ProjEmplsViewModel)projEmplBindingsWindow.DataContext).ProjectsViewModel = pVM;
                    ViewModelsContainer.ProjEmplsViewModel = ((ProjEmplsViewModel)projEmplBindingsWindow.DataContext);
                    projEmplBindingsWindow.ShowDialog();
                    ViewModelsContainer.ProjEmplsViewModel = null;
                }
                else
                    MessageBox.Show("Нельзя редактировать колличество сотрудников на проекте " +
                                    "до тех пор, пока информация о проекте не добавлена в базу данных.",
                                    "Проекты");
            }

            if (dataGrid.CurrentCell.Column.Header.ToString() == "Наличие руководителя")
            {
                if (((ProjectView)dataGrid.CurrentItem).IsAdded != true)
                {
                    ProjLeadWindow projLeadBindingsWindow;
                    projLeadBindingsWindow = new ProjLeadWindow();
                    ((ProjLeadViewModel)projLeadBindingsWindow.DataContext).ProjectView = (ProjectView)dataGrid.CurrentCell.Item;
                    ((ProjLeadViewModel)projLeadBindingsWindow.DataContext).ProjectsViewModel = pVM;
                    ViewModelsContainer.ProjLeadViewModel = ((ProjLeadViewModel)projLeadBindingsWindow.DataContext);
                    projLeadBindingsWindow.ShowDialog();
                    ViewModelsContainer.ProjLeadViewModel = null;
                }
                else
                    MessageBox.Show("Нельзя выбирать руководителя на проекте " +
                                    "до тех пор, пока информация о проекте не добавлена в базу данных.",
                                    "Проекты");
            } 
        }
        void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
		#endregion

		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			LoginWindow loginWindow = new LoginWindow();
			loginWindow.ShowDialog();
		}
	}
}