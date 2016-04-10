
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace ElectroShopMobile.CustomAttributes
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private bool hasRole = true;
        public string Role;

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }
}