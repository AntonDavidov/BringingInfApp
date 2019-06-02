using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels.EmplProjs;

namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для EmplProjsWindow.xaml
    /// </summary>
    public partial class EmplProjsWindow : Window
    {
        #region Constructor
        public EmplProjsWindow()
        {
            InitializeComponent();

            ((EmplProjsViewModel)this.DataContext).PropertyChanged += (s, eArgs) =>
            {
				if (eArgs.PropertyName == "MessageBoxVM")
                    switch (MessageBox.Show(((EmplProjsViewModel)this.DataContext).MessageBoxVM.Message,
                                    ((EmplProjsViewModel)this.DataContext).MessageBoxVM.Caption,
                                    ((EmplProjsViewModel)this.DataContext).MessageBoxVM.Buttons))
                    {
                        case MessageBoxResult.No:
                            {
                                ((EmplProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.No;
                                break;
                            }
                        case MessageBoxResult.None:
                            {
                                ((EmplProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.None;
                                break;
                            }
                        case MessageBoxResult.Yes:
                            {
                                ((EmplProjsViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.Yes;
                                break;
                            }
                    }
            };
        }
        #endregion
    }
}
