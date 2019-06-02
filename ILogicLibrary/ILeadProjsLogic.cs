using ClientEntities;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с проектами руководителя
    /// </summary>
    public interface ILeadProjsLogic
    {
        List<Project> GetProjects(bool expand = false);
        List<Project> GetProjIDs();
        bool SetLeadProjectsToEmployee(Employee employee, List<Project> leadProjsToAdd, List<Project> leadProjsToDelele);
        List<Project> OtherLeadOnProjExistance(List<Project> projects);

		string Login { get; set; }
		string Password { get; set; }
	}
}
