using ViewLayerWpfApp.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ViewLayerWpfApp.Views.Account
{
    /// <summary>
    /// Логика взаимодействия для ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        #region Constructor
        public ResetPasswordWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region Methods
        private void oldPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((ResetPasswordViewModel)DataContext).OldPassword = oldPasswordBox.Password;
        }
        private void newPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((ResetPasswordViewModel)DataContext).NewPassword = newPasswordBox.Password;
        }
        private void confirmNewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((ResetPasswordViewModel)DataContext).ConfirmNewPassword = confirmNewPasswordBox.Password;
        }
        #endregion
    }
}
