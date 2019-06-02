using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewLayerWpfApp.ViewModels.Account
{
    public partial class ResetPasswordViewModel: INotifyPropertyChanged
    {
        #region Fields
        string login;
        string registrationCode;
        ICommand resetPasswordCommand;
        #endregion


        #region Properties
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string RegistrationCode
        {
            get { return registrationCode; }
            set
            {
                registrationCode = value;
                OnPropertyChanged("RegistrationCode");
            }
        }
        public ICommand ResetPasswordCommand
        {
            get
            {
                if(resetPasswordCommand == null)
                   resetPasswordCommand = new RelayCommand(()=> ResetPassword());
                return resetPasswordCommand;
            }
        }
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
        void ResetPassword()
        {
        }
        #endregion
    }
}