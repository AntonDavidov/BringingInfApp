using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels.ProjEmpls;

namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjEmplsWindow.xaml
    /// </summary>
    public partial class ProjEmplsWindow : Window
    {
        #region Conctrustor
        public ProjEmplsWindow()
        {
            InitializeComponent();

            ((ProjEmplsViewModel)this.DataContext).PropertyChanged += (s, eArgs) =>
            {
				if (eArgs.PropertyName == "MessageBoxVM")
                    switch (MessageBox.Show(((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Message,
                                    ((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Caption,
                                    ((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Buttons))
                    {
                        case MessageBoxResult.No:
                            {
                                ((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.No;
                                break;
                            }
                        case MessageBoxResult.None:
                            {
                                ((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.None;
                                break;
                            }
                        case MessageBoxResult.Yes:
                            {
                                ((ProjEmplsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.Yes;
                                break;
                            }
                    }
            };
        }
        #endregion
    }
}
