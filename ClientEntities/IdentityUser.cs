using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientEntities
{


    /// <summary>
    /// В схеме отсутствуют комментарии для DataLayerWcfApp.DataModel.IdentityUser.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Users")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class IdentityUser : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Создайте новый объект IdentityUser.
        /// </summary>
        /// <param name="ID">Начальное значение Id.</param>
        /// <param name="emailConfirmed">Начальное значение EmailConfirmed.</param>
        /// <param name="phoneNumberConfirmed">Начальное значение PhoneNumberConfirmed.</param>
        /// <param name="twoFactorEnabled">Начальное значение TwoFactorEnabled.</param>
        /// <param name="lockoutEnabled">Начальное значение LockoutEnabled.</param>
        /// <param name="accessFailedCount">Начальное значение AccessFailedCount.</param>
        /// <param name="userName">Начальное значение UserName.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static IdentityUser CreateIdentityUser(string ID, bool emailConfirmed, bool phoneNumberConfirmed, bool twoFactorEnabled, bool lockoutEnabled, int accessFailedCount, string userName)
        {
            IdentityUser identityUser = new IdentityUser();
            identityUser.Id = ID;
            identityUser.EmailConfirmed = emailConfirmed;
            identityUser.PhoneNumberConfirmed = phoneNumberConfirmed;
            identityUser.TwoFactorEnabled = twoFactorEnabled;
            identityUser.LockoutEnabled = lockoutEnabled;
            identityUser.AccessFailedCount = accessFailedCount;
            identityUser.UserName = userName;
            return identityUser;
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Id.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Id;
        partial void OnIdChanging(string value);
        partial void OnIdChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Email.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this.OnEmailChanging(value);
                this._Email = value;
                this.OnEmailChanged();
                this.OnPropertyChanged("Email");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Email;
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства EmailConfirmed.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool EmailConfirmed
        {
            get
            {
                return this._EmailConfirmed;
            }
            set
            {
                this.OnEmailConfirmedChanging(value);
                this._EmailConfirmed = value;
                this.OnEmailConfirmedChanged();
                this.OnPropertyChanged("EmailConfirmed");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _EmailConfirmed;
        partial void OnEmailConfirmedChanging(bool value);
        partial void OnEmailConfirmedChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства PasswordHash.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string PasswordHash
        {
            get
            {
                return this._PasswordHash;
            }
            set
            {
                this.OnPasswordHashChanging(value);
                this._PasswordHash = value;
                this.OnPasswordHashChanged();
                this.OnPropertyChanged("PasswordHash");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _PasswordHash;
        partial void OnPasswordHashChanging(string value);
        partial void OnPasswordHashChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства SecurityStamp.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string SecurityStamp
        {
            get
            {
                return this._SecurityStamp;
            }
            set
            {
                this.OnSecurityStampChanging(value);
                this._SecurityStamp = value;
                this.OnSecurityStampChanged();
                this.OnPropertyChanged("SecurityStamp");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _SecurityStamp;
        partial void OnSecurityStampChanging(string value);
        partial void OnSecurityStampChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства PhoneNumber.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string PhoneNumber
        {
            get
            {
                return this._PhoneNumber;
            }
            set
            {
                this.OnPhoneNumberChanging(value);
                this._PhoneNumber = value;
                this.OnPhoneNumberChanged();
                this.OnPropertyChanged("PhoneNumber");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _PhoneNumber;
        partial void OnPhoneNumberChanging(string value);
        partial void OnPhoneNumberChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства PhoneNumberConfirmed.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool PhoneNumberConfirmed
        {
            get
            {
                return this._PhoneNumberConfirmed;
            }
            set
            {
                this.OnPhoneNumberConfirmedChanging(value);
                this._PhoneNumberConfirmed = value;
                this.OnPhoneNumberConfirmedChanged();
                this.OnPropertyChanged("PhoneNumberConfirmed");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _PhoneNumberConfirmed;
        partial void OnPhoneNumberConfirmedChanging(bool value);
        partial void OnPhoneNumberConfirmedChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства TwoFactorEnabled.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool TwoFactorEnabled
        {
            get
            {
                return this._TwoFactorEnabled;
            }
            set
            {
                this.OnTwoFactorEnabledChanging(value);
                this._TwoFactorEnabled = value;
                this.OnTwoFactorEnabledChanged();
                this.OnPropertyChanged("TwoFactorEnabled");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _TwoFactorEnabled;
        partial void OnTwoFactorEnabledChanging(bool value);
        partial void OnTwoFactorEnabledChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства LockoutEndDateUtc.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> LockoutEndDateUtc
        {
            get
            {
                return this._LockoutEndDateUtc;
            }
            set
            {
                this.OnLockoutEndDateUtcChanging(value);
                this._LockoutEndDateUtc = value;
                this.OnLockoutEndDateUtcChanged();
                this.OnPropertyChanged("LockoutEndDateUtc");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _LockoutEndDateUtc;
        partial void OnLockoutEndDateUtcChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnLockoutEndDateUtcChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства LockoutEnabled.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool LockoutEnabled
        {
            get
            {
                return this._LockoutEnabled;
            }
            set
            {
                this.OnLockoutEnabledChanging(value);
                this._LockoutEnabled = value;
                this.OnLockoutEnabledChanged();
                this.OnPropertyChanged("LockoutEnabled");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _LockoutEnabled;
        partial void OnLockoutEnabledChanging(bool value);
        partial void OnLockoutEnabledChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства AccessFailedCount.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int AccessFailedCount
        {
            get
            {
                return this._AccessFailedCount;
            }
            set
            {
                this.OnAccessFailedCountChanging(value);
                this._AccessFailedCount = value;
                this.OnAccessFailedCountChanged();
                this.OnPropertyChanged("AccessFailedCount");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _AccessFailedCount;
        partial void OnAccessFailedCountChanging(int value);
        partial void OnAccessFailedCountChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства UserName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this.OnUserNameChanging(value);
                this._UserName = value;
                this.OnUserNameChanged();
                this.OnPropertyChanged("UserName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _UserName;
        partial void OnUserNameChanging(string value);
        partial void OnUserNameChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
