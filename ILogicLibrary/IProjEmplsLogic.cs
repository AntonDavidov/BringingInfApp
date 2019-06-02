using ClientEntities;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с сотрудниками проекта
    /// </summary>
    public interface IProjEmplsLogic
    {
        List<Employee> GetEmployees(bool expand = false);
        List<Employee> GetEmplIDs();
        bool SetEmployeesToProject(Project project, List<Employee> emplsToAdd, List<Employee> emplsToDelele);

		string Login { get; set; }
		string Password { get; set; }
	}
}
