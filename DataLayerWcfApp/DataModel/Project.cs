namespace DataLayerWcfApp.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Класс сущности "Проект"
    /// </summary>
    public partial class Project
    {
        #region Constructor
        public Project()
        {
            Employees = new HashSet<Employee>();
        }
        #endregion


        #region Properties
        public int Id { get; set; }
        [StringLength(50)]
        public string ProjName { get; set; }
        [StringLength(50)]
        public string OrgOrderName { get; set; }
        [StringLength(50)]
        public string OrgExecuteName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateProjExecuteBegin { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateProjExecuteEnd { get; set; }
        [Range(0, int.MaxValue)]
        public int? Priority { get; set; }
        [StringLength(100)]
        public string Comment { get; set; }


        public int? LeaderId { get; set; }        
        public virtual Employee Leader { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        #endregion
    }
}
