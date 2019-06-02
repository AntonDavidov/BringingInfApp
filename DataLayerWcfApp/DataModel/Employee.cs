namespace DataLayerWcfApp.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Класс сущности "Сотрудник"
    /// </summary>
    public partial class Employee
    {
        #region Constructor
        public Employee()
        {
            Projects = new HashSet<Project>();
            LeadProjects = new HashSet<Project>();
        }
        #endregion


        #region Properties
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        public string EMail { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Project> LeadProjects { get; set; }
        #endregion
    }
}
