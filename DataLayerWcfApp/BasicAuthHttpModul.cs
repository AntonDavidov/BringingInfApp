using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Text;
using System.Data.Entity;
using DataLayerWcfApp.DataModel;

namespace DataLayerWcfApp
{
	public class BasicAuthHttpModul: IHttpModule
	{
		#region Methods
		public void Init(HttpApplication httpApp)
		{
			httpApp.AuthenticateRequest += HttpApp_AuthenticateRequest;
		}
		public void Dispose()
		{
		}

		private void HttpApp_AuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication httpApp = (HttpApplication)sender;
			if (!UserAuthenticationProvider.Authenticate(httpApp.Context))
			{
				httpApp.Context.Response.Headers.Add("WWW-Authenticate", "Basic");
				httpApp.Context.Response.StatusCode = 401;
				httpApp.Context.Response.Status = "401 Unauthorized";
				httpApp.Context.Response.End();
			}
		}
		#endregion
	}

	class UserAuthenticationProvider
	{
		#region Methods
		public static bool Authenticate(HttpContext context)
		{
			if (!context.Request.Headers.AllKeys.Contains("Authorization"))
				return false;
			//if (!context.Request.IsSecureConnection)
			//	return false;

			string authHeader = context.Request.Headers["Authorization"];
			IPrincipal principal = null;
			if (TryGetPrincipal(authHeader, out principal))
			{
				context.User = principal;
				return true;
			}
			return false;
		}

		static bool TryGetPrincipal(string authHeader, out IPrincipal principal)
		{
			string[] creds = ParseAuthHeader(authHeader);

			principal = null;
			if (creds != null)
				if (creds.Length != 2)
				{
					principal = null;
					return false;
				}
				else
				{
					IdentityDbContext<IdentityUser> identityDbContext = new InfBaseModel() as IdentityDbContext<IdentityUser>;
					string userName = creds[0];
					List<IdentityUser> users = identityDbContext.Users.Include(u => u.Roles).Where(u => u.UserName == userName).ToList();
					foreach (var user in users)
					{
						if (creds[1] == user.PasswordHash)
						{
                            List<string> roleNames = new List<string>();
                            foreach (var role in user.Roles)
                            	roleNames.Add(identityDbContext.Roles.First(r => r.Id == role.RoleId).Name);
                            principal = new GenericPrincipal(new GenericIdentity(creds[0]), new string[]{ "Admin", "User" });//roleNames.ToArray());
							return true;
						}
					}
					principal = null;
					return false;
				}
			else
			{
				principal = null;
				return false;
			}
		}
		static string[] ParseAuthHeader(string authHeader)
		{
			if (authHeader == null ||
				authHeader.Length == 0 ||
				!authHeader.StartsWith("Basic"))
				return null;

			string base64Credentials = authHeader.Substring(6);
			string[] credentials = Encoding.ASCII
										   .GetString(Convert.FromBase64String(base64Credentials))
										   .Split(':');
			if (credentials.Length != 2 ||
				string.IsNullOrEmpty(credentials[0]) ||
				string.IsNullOrEmpty(credentials[1]))
				return null;

			return credentials;
		}
		#endregion
	}
}