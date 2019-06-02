using ClientEntities;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с проектами сотруника
    /// </summary>
    public interface IEmplProjsLogic
    {
        List<Project> GetProjects(bool expand = false);
        List<Project> GetProjIDs();
        bool SetProjectsToEmployee(Employee employee, List<Project> projsToAdd, List<Project> projsToDelele);

		string Login { get; set; }
		string Password { get; set; }
	}
}
