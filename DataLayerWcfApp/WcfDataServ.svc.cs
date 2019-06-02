//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using DataLayerWcfApp.DataModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Web;
using System.Security.Principal;
using System.Web;

namespace DataLayerWcfApp
{
    public class WcfDataServ : EntityFrameworkDataService<InfBaseModel>
    {
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Employees", EntitySetRights.All);
            config.SetEntitySetPageSize("Employees", 20);
            config.SetEntitySetAccessRule("Projects", EntitySetRights.All);
            config.SetEntitySetPageSize("Projects", 20);
            config.SetEntitySetAccessRule("Users", EntitySetRights.All);
            config.SetEntitySetAccessRule("Roles", EntitySetRights.All);

            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        [QueryInterceptor("Projects")]
        public Expression<Func<Project, bool>> FilterProjects()
        {
			bool result = HttpContext.Current.User.IsInRole("Admin");
			return p => result;
		}
		[QueryInterceptor("Employees")]
		public Expression<Func<Employee, bool>> FilterEmployees()
		{
			bool result = HttpContext.Current.User.IsInRole("Admin");
			return p => result;
		}
	}
}
