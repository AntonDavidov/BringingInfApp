using ClientEntities;
using ILogic;
using InfBaseModelClient;
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Logic
{
    [Export(typeof(IUsersLogic))]
    public class UsersLogic : IUsersLogic
    {
        #region Fields
        Uri uri = new Uri("http://localhost:25707/WcfDataServ.svc");
        #endregion


        #region Methods
        public void AddUser(IdentityUser user)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
            string strUri = string.Format("http://localhost:25707/WcfDataServ.svc/AddUser?user='{0}'", user);
            
            Uri methodUri = new Uri(strUri);
            infBaseModel.Execute<string>(methodUri);
            int tyu = 0;
        }
        #endregion


        /*
        #region Fields
        Uri uri = new Uri("http://localhost:25707/WcfDataServ.svc");
        #endregion


        #region Methods
        #region Get/Set Data Methods
        public List<IdentityUser> GetUsers(bool expand = true)
        {
            return TryGetUsersByPredicate(e => true, expand);
        }

        public void AddUser(IdentityUser user)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
        }


        public List<IdentityUser> GetEmplIDs()
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
            QueryOperationResponse<IdentityUser> qOR;
            qOR = (infBaseModel.Users.Select(e => new { Id = e.Id }) as DataServiceQuery<IdentityUser>)
                             .Execute() as QueryOperationResponse<IdentityUser>;

            DataServiceQueryContinuation<IdentityUser> token = null;
            List<IdentityUser> Users = new List<IdentityUser>();
            do
            {
                if (token != null)
                    qOR = infBaseModel.Execute(token);
                Users.AddRange(qOR);
            } while ((token = qOR.GetContinuation()) != null);

            return Users;
        }
        public bool SetUsers(List<IdentityUser> UsersToAdd, List<IdentityUser> UsersToUpdate, List<IdentityUser> UsersToDelete)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
            if (UsersToAdd != null)
                foreach (var e in UsersToAdd) infBaseModel.AddToUsers(e);
            if (UsersToUpdate != null)
                foreach (var e in UsersToUpdate)
                {
                    infBaseModel.AttachTo("Users", e);
                    infBaseModel.UpdateObject(e);
                }
            if (UsersToDelete != null)
                foreach (var e in UsersToDelete)
                {
                    infBaseModel.AttachTo("Users", e);
                    infBaseModel.DeleteObject(e);
                }
            if (UsersToAdd != null && UsersToAdd.Count > 0 ||
                UsersToUpdate != null && UsersToUpdate.Count > 0 ||
                UsersToDelete != null && UsersToDelete.Count > 0)
                return infBaseModel.SaveChanges().All(oR => oR.Error == null);
            else
                return false;

        }
        #endregion


        #region Filters
        public List<IdentityUser> GetUsersByNameStartWith(string name, bool expand = true)
        {
            if (name != null)
                return TryGetUsersByPredicate(e => e.Name.StartsWith(name), expand);
            else
                return TryGetUsersByPredicate(e => true, expand);
        }
        public List<IdentityUser> GetUsersBySurnameStartWith(string surname, bool expand = true)
        {
            if (surname != null)
                return TryGetUsersByPredicate(e => e.Surname.StartsWith(surname), expand);
            else
                return TryGetUsersByPredicate(e => true, expand);
        }
        public List<IdentityUser> GetUsersByMiddleNameStartWith(string middleName, bool expand = true)
        {
            if (middleName != null)
                return TryGetUsersByPredicate(p => p.MiddleName.StartsWith(middleName), expand);
            else
                return TryGetUsersByPredicate(e => true, expand);
        }
        #endregion


        #region Supporting Methods
        List<IdentityUser> TryGetUsersByPredicate(Expression<Func<IdentityUser, bool>> predicate, bool expand = true)
        {
            InfBaseModel infBaseModel = new InfBaseModel(uri);
            QueryOperationResponse<IdentityUser> qOR;
            if (expand == true)
            {
                qOR = (infBaseModel.Users.Expand(e => e.Projects)
                                             .Expand(e => e.LeadProjects)
                                             .Where(predicate) as DataServiceQuery<IdentityUser>)
                                             .Execute() as QueryOperationResponse<IdentityUser>;
            }
            else
            {
                qOR = (infBaseModel.Users.Where(predicate) as DataServiceQuery<IdentityUser>)
                                             .Execute() as QueryOperationResponse<IdentityUser>;
            }
            DataServiceQueryContinuation<IdentityUser> token = null;
            List<IdentityUser> Users = new List<IdentityUser>();
            do
            {
                if (token != null)
                    qOR = infBaseModel.Execute(token);
                Users.AddRange(qOR);
            } while ((token = qOR.GetContinuation()) != null);

            return Users;
        }
        #endregion
        #endregion
        */
    }
}
