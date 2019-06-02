namespace ClientEntities
{
    /// <summary>
    /// В схеме отсутствуют комментарии для DataLayerWcfApp.DataModel.Project.
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Projects")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class Project : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Создайте новый объект Project.
        /// </summary>
        /// <param name="ID">Начальное значение Id.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static Project CreateProject(int ID)
        {
            Project project = new Project();
            project.Id = ID;
            return project;
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
        /// В схеме отсутствуют комментарии для свойства ProjName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ProjName
        {
            get
            {
                return this._ProjName;
            }
            set
            {
                this.OnProjNameChanging(value);
                this._ProjName = value;
                this.OnProjNameChanged();
                this.OnPropertyChanged("ProjName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _ProjName;
        partial void OnProjNameChanging(string value);
        partial void OnProjNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства OrgOrderName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string OrgOrderName
        {
            get
            {
                return this._OrgOrderName;
            }
            set
            {
                this.OnOrgOrderNameChanging(value);
                this._OrgOrderName = value;
                this.OnOrgOrderNameChanged();
                this.OnPropertyChanged("OrgOrderName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _OrgOrderName;
        partial void OnOrgOrderNameChanging(string value);
        partial void OnOrgOrderNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства OrgExecuteName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string OrgExecuteName
        {
            get
            {
                return this._OrgExecuteName;
            }
            set
            {
                this.OnOrgExecuteNameChanging(value);
                this._OrgExecuteName = value;
                this.OnOrgExecuteNameChanged();
                this.OnPropertyChanged("OrgExecuteName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _OrgExecuteName;
        partial void OnOrgExecuteNameChanging(string value);
        partial void OnOrgExecuteNameChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства DateProjExecuteBegin.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> DateProjExecuteBegin
        {
            get
            {
                return this._DateProjExecuteBegin;
            }
            set
            {
                this.OnDateProjExecuteBeginChanging(value);
                this._DateProjExecuteBegin = value;
                this.OnDateProjExecuteBeginChanged();
                this.OnPropertyChanged("DateProjExecuteBegin");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Nullable<global::System.DateTime> _DateProjExecuteBegin;
        partial void OnDateProjExecuteBeginChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnDateProjExecuteBeginChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства DateProjExecuteEnd.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> DateProjExecuteEnd
        {
            get
            {
                return this._DateProjExecuteEnd;
            }
            set
            {
                this.OnDateProjExecuteEndChanging(value);
                this._DateProjExecuteEnd = value;
                this.OnDateProjExecuteEndChanged();
                this.OnPropertyChanged("DateProjExecuteEnd");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Nullable<global::System.DateTime> _DateProjExecuteEnd;
        partial void OnDateProjExecuteEndChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnDateProjExecuteEndChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Priority.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> Priority
        {
            get
            {
                return this._Priority;
            }
            set
            {
                this.OnPriorityChanging(value);
                this._Priority = value;
                this.OnPriorityChanged();
                this.OnPropertyChanged("Priority");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Nullable<int> _Priority;
        partial void OnPriorityChanging(global::System.Nullable<int> value);
        partial void OnPriorityChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства Comment.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this.OnCommentChanging(value);
                this._Comment = value;
                this.OnCommentChanged();
                this.OnPropertyChanged("Comment");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        string _Comment;
        partial void OnCommentChanging(string value);
        partial void OnCommentChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для свойства LeaderId.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<int> LeaderId
        {
            get
            {
                return this._LeaderId;
            }
            set
            {
                this.OnLeaderIdChanging(value);
                this._LeaderId = value;
                this.OnLeaderIdChanged();
                this.OnPropertyChanged("LeaderId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Nullable<int> _LeaderId;
        partial void OnLeaderIdChanging(global::System.Nullable<int> value);
        partial void OnLeaderIdChanged();
        /// <summary>
        /// В схеме отсутствуют комментарии для Employees.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<Employee> Employees
        {
            get
            {
                return this._Employees as System.Data.Services.Client.DataServiceCollection<Employee>;
            }
            set
            {
                this._Employees = value;
                this.OnPropertyChanged("Employees");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        global::System.Data.Services.Client.DataServiceCollection<Employee> _Employees = new global::System.Data.Services.Client.DataServiceCollection<Employee>(null, global::System.Data.Services.Client.TrackingMode.None);
        /// <summary>
        /// В схеме отсутствуют комментарии для Leader.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public Employee Leader
        {
            get
            {
                return this._Leader;
            }
            set
            {
                this._Leader = value;
                this.OnPropertyChanged("Leader");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        Employee _Leader;
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
