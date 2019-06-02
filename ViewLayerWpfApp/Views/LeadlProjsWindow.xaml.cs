using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels.LeadProjs;

namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LeadProjsWindow.xaml
    /// </summary>
    public partial class LeadProjsWindow : Window
    {
        #region Constructor
        public LeadProjsWindow()
        {
            InitializeComponent();

            ((LeadProjsViewModel)this.DataContext).PropertyChanged += (s, eArgs) =>
            {
				if (eArgs.PropertyName == "MessageBoxVM")
                    switch (MessageBox.Show(((LeadProjsViewModel)this.DataContext).MessageBoxVM.Message,
                                    ((LeadProjsViewModel)this.DataContext).MessageBoxVM.Caption,
                                    ((LeadProjsViewModel)this.DataContext).MessageBoxVM.Buttons))
                    {
                        case MessageBoxResult.No:
                            {
                                ((LeadProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.No;
                                break;
                            }
                        case MessageBoxResult.None:
                            {
                                ((LeadProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.None;
                                break;
                            }
                        case MessageBoxResult.Yes:
                            {
                                ((LeadProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.Yes;
                                break;
                            }
                    }
            };
        }
        #endregion
    }
}
