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
    public partial class ForgotPasswordViewModel: INotifyPropertyChanged
    {
        #region Fields
        string login;
        string registrationCode;
        string recoveryPassword;
        ICommand recoverPasswordCommand;
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
        public string RegistrationCode
        {
            get { return registrationCode; }
            set
            {
                registrationCode = value;
                OnPropertyChanged("RegistrationCode");
            }
        }
        public string RecoveryPassword
        {
            get { return recoveryPassword; }
            set
            {
                recoveryPassword = value;
                OnPropertyChanged("RecoveryPassword");
            }
        }
        public ICommand RecoverPasswordCommand
        {
            get
            {
                if(recoverPasswordCommand == null)
                   recoverPasswordCommand = new RelayCommand(()=> RecoverPassword());
                return recoverPasswordCommand;
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
        void RecoverPassword()
        {
        }
        #endregion
    }
}