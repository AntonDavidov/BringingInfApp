using ClientEntities;
using System;
using System.Collections.Generic;

namespace ILogic
{
    /// <summary>
    /// Интерфейс бизнес-логики работы с проектами
    /// </summary>
    public interface IProjectsLogic
    {
        List<Project> GetProjects(bool expand = true);
        List<Project> GetProjIDs();
        bool SetProjects(List<Project> projToAdd, List<Project> projToUpdate, List<Project> projToDelete);

        List<Project> GetProjectsByNameStartWith(string projName, bool expand = true);
        List<Project> GetProjectsByOrgOrderNameStartWith(string orgOrderName, bool expand = true);
        List<Project> GetProjectsByOrgExecuteNameStartWith(string orgExecuteName, bool expand = true);
        List<Project> GetProjectsByDateProjExecuteBeginRange(DateTime? lowDate, DateTime? upperDate, bool expand = true);
        List<Project> GetProjectsByDateProjExecuteEndRange(DateTime? lowDate, DateTime? upperDate, bool expand = true);
        List<Project> GetProjectsByPriorityEqualsOrMoreThen(int priority, bool expand = true);
        List<Project> GetProjectsByPriorityLessThen(int priority, bool expand = true);

		string Login { get; set; }
		string Password { get; set; }
    }
}
