using ClientEntities;
using ILogic;
using InfBaseModelClient;
using System.Data.Services;
using System.Data.Services.Client;
using System;
using System.Text;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.ServiceModel.Web;
using System.Threading.Tasks;


namespace Logic
{
    /// <summary>
    /// Класс реализации бизнесс-логики проектов
    /// </summary>
    [Export(typeof(IProjectsLogic))]
    [Export(typeof(IEmplProjsLogic))]
    [Export(typeof(ILeadProjsLogic))]
    public class ProjectsLogic : IProjectsLogic, IEmplProjsLogic, ILeadProjsLogic
    {
        #region Fields
        Uri uri = new Uri("http://localhost:25707/WcfDataServ.svc");
        #endregion


        #region Methods
        #region Get/Set Data Methods
        public List<Project> GetProjects(bool expand = true)
        {
            return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjIDs()
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
            QueryOperationResponse<Project> qOR = (infBaseModel.Projects.Select(p => new Project() { Id = p.Id }) 
                                                                         as DataServiceQuery<Project>)
                                                                        .Execute() as QueryOperationResponse<Project>;
            DataServiceQueryContinuation<Project> token = null;
            List<Project> projects = new List<Project>();

            do
            {
                if (token != null)
                    qOR = infBaseModel.Execute(token);

                projects.AddRange(qOR);

            }
            while ((token = qOR.GetContinuation()) != null);

            return projects;
        }
        public bool SetProjects(List<Project> projToAdd, List<Project> projToUpdate, List<Project> projToDelete)
        {
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			if (projToAdd != null)
                foreach (var p in projToAdd)
                    infBaseModel.AddToProjects(p);
            if (projToUpdate != null)
                foreach (var p in projToUpdate)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.UpdateObject(p);
                }
            if (projToDelete != null)
                foreach (var p in projToDelete)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.DeleteObject(p);
                }
            if (projToAdd != null && projToAdd.Count > 0 ||
                projToUpdate != null && projToUpdate.Count > 0 ||
                projToDelete != null && projToDelete.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;
        }
        public bool SetProjectsToEmployee(Employee employee, List<Project> projsToAdd, List<Project> projsToDelele)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			infBaseModel.AttachTo("Employees", employee);
            if (projsToDelele != null)
                foreach (var p in projsToDelele)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.DeleteLink(employee, "Projects", p);
                }
            if (projsToAdd != null)
                foreach (var p in projsToAdd)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.AddLink(employee, "Projects", p);
                }
            if (projsToAdd != null && projsToAdd.Count > 0 ||
                projsToDelele != null && projsToDelele.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;
        }
        public bool SetLeadProjectsToEmployee(Employee employee, List<Project> leadProjsToAdd, List<Project> leadProjsToDelele)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;
			infBaseModel.AttachTo("Employees", employee);
            if (leadProjsToAdd != null)
                foreach (var p in leadProjsToAdd)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.AddLink(employee, "LeadProjects", p);
                }
            if (leadProjsToDelele != null)
                foreach (var p in leadProjsToDelele)
                {
                    infBaseModel.AttachTo("Projects", p);
                    infBaseModel.DeleteLink(employee, "LeadProjects", p);
                }
            if (leadProjsToAdd != null && leadProjsToAdd.Count > 0 ||
                leadProjsToDelele != null && leadProjsToDelele.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;
        }
        public List<Project> OtherLeadOnProjExistance(List<Project> projects)
        {
            List<Project> projsWithLeader;
            if (projects != null)
            {
				InfBaseModel infBaseModel = new InfBaseModel(uri);
				infBaseModel.SendingRequest2 += OnSendingRequest2;
				projsWithLeader = TryGetProjectsByPredicate(p => p.LeaderId != null);
                List<Project> resultProjects = new List<Project>();
                foreach (var project in projects)
                    if (projsWithLeader.Exists(p => p.Id == project.Id))
                        resultProjects.Add(project);

                return resultProjects;
            }
            else
                return new List<Project>();
        }
        #endregion


        #region Filters
        public List<Project> GetProjectsByNameStartWith(string projName, bool expand = true)
        {
            if (projName != null)
                return TryGetProjectsByPredicate(p => p.ProjName.StartsWith(projName), expand);
            else
                return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjectsByOrgOrderNameStartWith(string orgOrderName, bool expand = true)
        {
            if (orgOrderName != null)
                return TryGetProjectsByPredicate(p => p.OrgOrderName.StartsWith(orgOrderName), expand);
            else
                return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjectsByOrgExecuteNameStartWith(string orgExecuteName, bool expand = true)
        {
            if (orgExecuteName != null)
                return TryGetProjectsByPredicate(p => p.OrgExecuteName.StartsWith(orgExecuteName), expand);
            else
                return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjectsByDateProjExecuteBeginRange(DateTime? lowDate, DateTime? upperDate, bool expand = true)
        {
            if (lowDate != null && upperDate != null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteBegin >= lowDate &&
                                                   p.DateProjExecuteBegin <= upperDate, expand);
            else
            if (lowDate != null && upperDate == null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteBegin >= lowDate, expand);
            else
            if (lowDate == null && upperDate != null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteBegin <= upperDate, expand);
            else
                return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjectsByDateProjExecuteEndRange(DateTime? lowDate, DateTime? upperDate, bool expand = true)
        {
            if (lowDate != null && upperDate != null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteEnd >= lowDate &&
                                                   p.DateProjExecuteEnd <= upperDate, expand);
            else
            if (lowDate != null && upperDate == null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteEnd >= lowDate, expand);
            else
            if (lowDate == null && upperDate != null)
                return TryGetProjectsByPredicate(p => p.DateProjExecuteEnd <= upperDate, expand);
            else
                return TryGetProjectsByPredicate(p => true, expand);
        }
        public List<Project> GetProjectsByPriorityEqualsOrMoreThen(int priority, bool expand = true)
        {
            return TryGetProjectsByPredicate(p => p.Priority >= priority, expand);
        }
        public List<Project> GetProjectsByPriorityLessThen(int priority, bool expand = true)
        {
            return TryGetProjectsByPredicate(p => p.Priority < priority, expand);
        }
		#endregion


		#region Supporting Method
		List<Project> TryGetProjectsByPredicate(Expression<Func<Project, bool>> predicate, bool expand = true)
		{
			InfBaseModel infBaseModel = new InfBaseModel(uri);
			infBaseModel.SendingRequest2 += OnSendingRequest2;

			List<Project> projects = new List<Project>();
			QueryOperationResponse<Project> qORP;
			try
			{
				if (expand == true)
					qORP = (infBaseModel.Projects.Expand(p => p.Employees)
												.Expand(p => p.Leader)
												.Where(predicate) as DataServiceQuery<Project>)
												.Execute() as QueryOperationResponse<Project>;
				else
					qORP = (infBaseModel.Projects.Where(predicate) as DataServiceQuery<Project>)
												.Execute() as QueryOperationResponse<Project>;

				DataServiceQueryContinuation<Project> token = null;
				do
				{
					if (token != null)
						qORP = infBaseModel.Execute(token);
					projects.AddRange(qORP);
				}
				while ((token = qORP.GetContinuation()) != null);
			}
			catch(DataServiceQueryException ex)
			{
				throw new LogicDataQueryException(ex, ex.Response.StatusCode);
			}
			return projects;
		}
		void OnSendingRequest2(object sender, SendingRequest2EventArgs e)
		{
			//MD5CryptoServiceProvider mD5Crypto = new MD5CryptoServiceProvider();

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
