using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels;
using ViewLayerWpfApp.ViewModels.Employees;
using ViewLayerWpfApp.ViewModels.EmplProjs;
using ViewLayerWpfApp.ViewModels.LeadProjs;


namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
		#region Constructor
		public EmployeesWindow()
		{
			InitializeComponent();
			Left = SystemParameters.VirtualScreenWidth / 2 - Width / 2 + Width / 4;
			Top = SystemParameters.VirtualScreenHeight / 2 - Height / 2 + Width / 4;
			EmployeesViewModel eVM = (EmployeesViewModel)this.DataContext;

			eVM.PropertyChanged += (s, eArgs) =>
			{
				if (eVM.NeedAuthentication)
				{
					LoginWindow loginWindow = new LoginWindow();
					loginWindow.ShowDialog();
				}

				if (eArgs.PropertyName == "MessageBoxVM")
				{
					switch (MessageBox.Show(eVM.MessageBoxVM.Message,
											eVM.MessageBoxVM.Caption,
											eVM.MessageBoxVM.Buttons))
					{
						case MessageBoxResult.Cancel:
							{
								eVM.MessageBoxVM.Result = MessageBoxResult.Cancel;
								break;
							}
						case MessageBoxResult.No:
							{
								eVM.MessageBoxVM.Result = MessageBoxResult.No;
								break;
							}
						case MessageBoxResult.None:
							{
								eVM.MessageBoxVM.Result = MessageBoxResult.None;
								break;
							}
						case MessageBoxResult.Yes:
							{
								eVM.MessageBoxVM.Result = MessageBoxResult.Yes;
								break;
							}
						default: break;
					}
				}
			};
		}  
        #endregion

        #region Methods
        void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.CurrentCell.Column.Header.ToString() == "Кол-во проектов")
            {
                if (((EmployeeView)dataGrid.CurrentItem).IsAdded != true)
                {
                    EmplProjsWindow emplProjsWindow;
                    emplProjsWindow = new EmplProjsWindow();
                    ((EmplProjsViewModel)emplProjsWindow.DataContext).EmployeeView = (EmployeeView)dataGrid.CurrentCell.Item;
                    ((EmplProjsViewModel)emplProjsWindow.DataContext).EmployeesViewModel = (EmployeesViewModel)(this.DataContext);
                    ViewModelsContainer.EmplProjsViewModel = ((EmplProjsViewModel)emplProjsWindow.DataContext);
                    emplProjsWindow.ShowDialog();
                    ViewModelsContainer.EmplProjsViewModel = null;
                }
                else
                    MessageBox.Show("Нельзя редактировать колличество сотрудников на проекте " +
                                    "до тех пор, пока информация о проекте не добавлена в базу данных.",
                                    "Проекты");
            }

            if (dataGrid.CurrentCell.Column.Header.ToString() == "Кол-во руководимых проектов")
            {
                if (((EmployeeView)dataGrid.CurrentItem).IsAdded != true)
                {
                    LeadProjsWindow leadProjsWindow;
                    leadProjsWindow = new LeadProjsWindow();
                    ((LeadProjsViewModel)leadProjsWindow.DataContext).LeaderView = (EmployeeView)dataGrid.CurrentCell.Item;
                    ((LeadProjsViewModel)leadProjsWindow.DataContext).EmployeesViewModel = (EmployeesViewModel)(this.DataContext);
                    ViewModelsContainer.LeadProjsViewModel = ((LeadProjsViewModel)leadProjsWindow.DataContext);
                    leadProjsWindow.ShowDialog();
                    ViewModelsContainer.LeadProjsViewModel = null;
                }
                else
                    MessageBox.Show("Нельзя выбирать руководителя на проекте " +
                                    "до тех пор, пока информация о проекте не добавлена в базу данных.",
                                    "Проекты");
            }
        }
        #endregion
    }
}
