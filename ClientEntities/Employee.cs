namespace ClientEntities
{
    /// <summary>
    /// В схеме отсутствуют комментарии для DataLayerWcfApp.DataModel.Employee.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Employees")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class Employee : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Создайте новый объект Employee.
        /// </summary>
        /// <param name="ID">Начальное значение Id.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Employee CreateEmployee(int ID)
        {
            Employee employee = new Employee();
            employee.Id = ID;
            return employee;
        }
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Id.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int Id
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
        int _Id;
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Name.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Surname.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Surname
        {
            get
            {
                return this._Surname;
            }
            set
            {
                this.OnSurnameChanging(value);
                this._Surname = value;
                this.OnSurnameChanged();
                this.OnPropertyChanged("Surname");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _Surname;
        partial void OnSurnameChanging(string value);
        partial void OnSurnameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства MiddleName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string MiddleName
        {
            get
            {
                return this._MiddleName;
            }
            set
            {
                this.OnMiddleNameChanging(value);
                this._MiddleName = value;
                this.OnMiddleNameChanged();
                this.OnPropertyChanged("MiddleName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _MiddleName;
        partial void OnMiddleNameChanging(string value);
        partial void OnMiddleNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства EMail.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string EMail
        {
            get
            {
                return this._EMail;
            }
            set
            {
                this.OnEMailChanging(value);
                this._EMail = value;
                this.OnEMailChanged();
                this.OnPropertyChanged("EMail");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _EMail;
        partial void OnEMailChanging(string value);
        partial void OnEMailChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для LeadProjects.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Project> LeadProjects
        {
            get
            {
                return this._LeadProjects;
            }
            set
            {
                this._LeadProjects = value;
                this.OnPropertyChanged("LeadProjects");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Data.Services.Client.DataServiceCollection<Project> _LeadProjects = new global::System.Data.Services.Client.DataServiceCollection<Project>(null, global::System.Data.Services.Client.TrackingMode.None);
        /// <summary>
        /// В схеме отсутствуют комментарии для Projects.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Project> Projects
        {
            get
            {
                return this._Projects;
            }
            set
            {
                this._Projects = value;
                this.OnPropertyChanged("Projects");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Data.Services.Client.DataServiceCollection<Project> _Projects = new global::System.Data.Services.Client.DataServiceCollection<Project>(null, global::System.Data.Services.Client.TrackingMode.None);
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
