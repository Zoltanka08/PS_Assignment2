using System;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Microsoft.Practices.Unity;

namespace ElectroShopMobile.CustomAttributes
{
    public class UserAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private bool hasRole = true;

        public string Role;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
        }
    }
}