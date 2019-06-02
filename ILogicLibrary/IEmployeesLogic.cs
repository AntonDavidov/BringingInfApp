using ClientEntities;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с сотрудниками
    /// </summary>
    public interface IEmployeesLogic
    {
        List<Employee> GetEmployees(bool expand = true);
        List<Employee> GetEmplIDs();
        bool SetEmployees(List<Employee> emplToAdd, List<Employee> emplToUpdate, List<Employee> emplToDelete);

        List<Employee> GetEmployeesByNameStartWith(string name, bool expand = true);
        List<Employee> GetEmployeesBySurnameStartWith(string surname, bool expand = true);
        List<Employee> GetEmployeesByMiddleNameStartWith(string middleName, bool expand = true);

		string Login { get; set; }
		string Password { get; set; }
	}
}
