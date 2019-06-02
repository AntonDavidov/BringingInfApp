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
using System.Windows.Input;
using ViewLayerWpfApp.ViewModels.SupportingClasses;

namespace ViewLayerWpfApp.ViewModels.Account
{
    public partial class LoginViewModel: INotifyPropertyChanged
    {
        #region Fields
        string login;
        bool remember;
		bool credentialsEntered = false;
        ICommand enterCommand;
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
        public bool Remember
        {
            get { return remember; }
            set
            {
                remember = value;
                OnPropertyChanged("Remember");
            }
        }
		public bool CredentialsEntered
		{
			get { return credentialsEntered; }
			set
			{
				credentialsEntered = value;
				OnPropertyChanged("CredentialsEntered");
			}
		}
		public ICommand EnterCommand
        {
            get
            {
                if(enterCommand == null)
                   enterCommand = new RelayCommand(()=> Enter());
                return enterCommand;
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
        void Enter()
        {
			Credentials.Login = Login;
			Credentials.Password = Password;
			CredentialsEntered = true;
        }
        #endregion
    }
}