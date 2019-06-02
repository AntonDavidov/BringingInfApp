using System;
using System.Collections.Generic;
using System.Linq;
using ViewLayerWpfApp.ViewModels.Account;
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
using ViewLayerWpfApp.Views;

namespace ViewLayerWpfApp.Views.Account
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
		#region Contructor
		public LoginWindow()
        {
            InitializeComponent();
			Left = SystemParameters.VirtualScreenWidth / 2 - Width / 2;
			Top = SystemParameters.VirtualScreenHeight / 2 - Height / 2;
			((LoginViewModel)DataContext).PropertyChanged += (s, e) =>
			  {
				  if(e.PropertyName=="CredentialsEntered")
				  {
                      //DialogResult = true;
                      Close();
				  }
			  };
		}
		#endregion


		#region Properties
		public bool NeedAuthenticationMessage { get; set; }
		#endregion

		#region Methods
		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginViewModel)DataContext).Password = passwordBox.Password;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (NeedAuthenticationMessage)
				MessageBox.Show("Вы не прошли аутентификацию! Необходимо ввести ваш логин и пароль.");
		}		
		#endregion
	}
}
