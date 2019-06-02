using ClientEntities;
using ILogic;
using InfBaseModelClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    /// <summary>
    /// Класс реализации бизнесс-логики сотрудников
    /// </summary>
    [Export(typeof(IEmployeesLogic))]
    [Export(typeof(IProjEmplsLogic))]
    [Export(typeof(IProjLeadLogic))]
    public class EmployeesLogic : IEmployeesLogic, IProjEmplsLogic, IProjLeadLogic
    {
        #region Fields
        Uri uri = new Uri("http://localhost:25707/WcfDataServ.svc");
        #endregion


        #region Methods
        #region Get/Set Data Methods
        public List<Employee> GetEmployees(bool expand = true)
        {
            return TryGetEmployeesByPredicate(e => true, expand);
        }
        public List<Employee> GetEmplIDs()
        {
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			QueryOperationResponse<Employee> qOR;
            qOR = (infBaseModel.Employees.Select(e => new Employee() { Id = e.Id }) as DataServiceQuery<Employee>)
                             .Execute() as QueryOperationResponse<Employee>;

            DataServiceQueryContinuation<Employee> token = null;
            List<Employee> employees = new List<Employee>();
            do
            {
                if (token != null)
                    qOR = infBaseModel.Execute(token);
                employees.AddRange(qOR);
            } while ((token = qOR.GetContinuation()) != null);

            return employees;
        }
        public bool SetEmployees(List<Employee> emplToAdd, List<Employee> emplToUpdate, List<Employee> emplToDelete)
        {
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			if (emplToAdd != null)
                foreach (var e in emplToAdd) infBaseModel.AddToEmployees(e);
            if (emplToUpdate != null)
                foreach (var e in emplToUpdate)
                {
                    infBaseModel.AttachTo("Employees", e);
                    infBaseModel.UpdateObject(e);
                }
            if (emplToDelete != null)
                foreach (var e in emplToDelete)
                {
                    List<Project> leadProjects = infBaseModel.Projects.Where(p => p.LeaderId == e.Id).ToList();
                    foreach (var p in leadProjects)
                        infBaseModel.SetLink(p, "Leader", null);
                    infBaseModel.AttachTo("Employees", e);
                    infBaseModel.DeleteObject(e);
                }
            if (emplToAdd != null && emplToAdd.Count > 0 ||
                emplToUpdate != null && emplToUpdate.Count > 0 ||
                emplToDelete != null && emplToDelete.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;

        }
        public bool SetEmployeesToProject(Project project, List<Employee> emplsToAdd, List<Employee> emplsToDelele)
        {
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			infBaseModel.AttachTo("Projects", project);
            if (emplsToDelele != null)
                foreach (var e in emplsToDelele)
                {
                    infBaseModel.AttachTo("Employees", e);
                    infBaseModel.DeleteLink(project, "Employees", e);
                }
            if (emplsToAdd != null)
                foreach (var e in emplsToAdd)
                {
                    infBaseModel.AttachTo("Employees", e);
                    infBaseModel.AddLink(project, "Employees", e);
                }
            if (emplsToAdd != null && emplsToAdd.Count > 0 ||
                emplsToDelele != null && emplsToDelele.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;
        }
        public bool SetLeaderToProject(Project project, Employee leader)
        {
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			infBaseModel.AttachTo("Projects", project);
            if (leader != null)
                infBaseModel.AttachTo("Employees", leader);
            infBaseModel.SetLink(project, "Leader", leader);
            return infBaseModel.SaveChanges().All(oR => oR.Error == null);
        }
        #endregion


        #region Filters
        public List<Employee> GetEmployeesByNameStartWith(string name, bool expand = true)
        {
            if (name != null)
                return TryGetEmployeesByPredicate(e => e.Name.StartsWith(name), expand);
            else
                return TryGetEmployeesByPredicate(e => true, expand);
        }
        public List<Employee> GetEmployeesBySurnameStartWith(string surname, bool expand = true)
        {
            if (surname != null)
                return TryGetEmployeesByPredicate(e => e.Surname.StartsWith(surname), expand);
            else
                return TryGetEmployeesByPredicate(e => true, expand);
        }
        public List<Employee> GetEmployeesByMiddleNameStartWith(string middleName, bool expand = true)
        {
            if (middleName != null)
                return TryGetEmployeesByPredicate(p => p.MiddleName.StartsWith(middleName), expand);
            else
                return TryGetEmployeesByPredicate(e => true, expand);
        }
        #endregion


        #region Supporting Methods
        List<Employee> TryGetEmployeesByPredicate(Expression<Func<Employee, bool>> predicate, bool expand = true)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;

			List<Employee> employees = new List<Employee>();
			QueryOperationResponse<Employee> qOR;
			try
			{
				if (expand == true)
				{
					qOR = (infBaseModel.Employees.Expand(e => e.Projects)
												 .Expand(e => e.LeadProjects)
												 .Where(predicate) as DataServiceQuery<Employee>)
												 .Execute() as QueryOperationResponse<Employee>;
				}
				else
				{
					qOR = (infBaseModel.Employees.Where(predicate) as DataServiceQuery<Employee>)
												 .Execute() as QueryOperationResponse<Employee>;
				}
				DataServiceQueryContinuation<Employee> token = null;
				do
				{
					if (token != null)
						qOR = infBaseModel.Execute(token);
					employees.AddRange(qOR);
				} while ((token = qOR.GetContinuation()) != null);
			}
			catch (DataServiceQueryException ex)
			{
				throw new Exception("Вы не прошли аутентификацию! Необходимо ввести ваш логин и пароль.", ex);
			}
            return employees;
        }
		void OnSendingRequest2(object sender, SendingRequest2EventArgs e)
		{
			string creds = Login + ":" + Password;
			byte[] bCreds = Encoding.ASCII.GetBytes(creds);
			string base64Creds = Convert.ToBase64String(bCreds);
			e.RequestMessage.SetHeader("Authorization", "Basic " + base64Creds);
		}
		#endregion
		#endregion


		#region Properties
		public string Login { get; set; }
		public string Password { get; set; }
		#endregion
	}
}
