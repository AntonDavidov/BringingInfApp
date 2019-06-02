using ClientEntities;
using ILogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewLayerWpfApp.ViewModels.Account
{
    public partial class RegisterViewModel: INotifyPropertyChanged
    {
        #region Fields
        string login;
        ICommand registerCommand;
        #region Supporting fields
        [Import]
        IUsersLogic usersLogic { get; set; }
        #endregion
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
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public ICommand RegisterCommand
        {
            get
            {
                if(registerCommand == null)
                   registerCommand = new RelayCommand(()=> Register());
                return registerCommand;
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
        void Register()
        {
            usersLogic.AddUser(new IdentityUser() { UserName = Login, PasswordHash = Password });
        }
        #endregion
    }
}