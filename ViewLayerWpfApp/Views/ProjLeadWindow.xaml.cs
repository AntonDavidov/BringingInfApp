using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ViewLayerWpfApp.Views.Account;
using ViewLayerWpfApp.ViewModels.ProjLead;

namespace ViewLayerWpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjLeadWindow.xaml
    /// </summary>
    public partial class ProjLeadWindow : Window
    {

        #region Conctrustor
        public ProjLeadWindow()
        {
            InitializeComponent();

            ((ProjLeadViewModel)this.DataContext).PropertyChanged += (s, eArgs) =>
            {
				if (eArgs.PropertyName == "MessageBoxVM")
                    switch (MessageBox.Show(((ProjLeadViewModel)this.DataContext).MessageBoxVM.Message,
                                    ((ProjLeadViewModel)this.DataContext).MessageBoxVM.Caption,
                                    ((ProjLeadViewModel)this.DataContext).MessageBoxVM.Buttons))
                    {
                        case MessageBoxResult.No:
                            {
                                ((ProjLeadViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.No;
                                break;
                            }
                        case MessageBoxResult.None:
                            {
                                ((ProjLeadViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.None;
                                break;
                            }
                        case MessageBoxResult.Yes:
                            {
                                ((ProjLeadViewModel)this.DataContext).MessageBoxVM.Result = MessageBoxResult.Yes;
                                break;
                            }
                    }
            };
        }
        #endregion
    }
}
