using ClientEntities;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с руководителем проекта
    /// </summary>
    public interface IProjLeadLogic
    {
        List<Employee> GetEmployees(bool expand = false);
        List<Employee> GetEmplIDs();
        bool SetLeaderToProject(Project project, Employee leader);

		string Login { get; set; }
		string Password { get; set; }
	}
}
