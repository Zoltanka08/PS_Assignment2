
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using System.Web.Routing;
using Services.Services;
using Services.Interfaces;
using System.Web;
using XMLDatabase.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using Bookstore.CustomPrincipalNamespace;
using XMLDatabase.Interfaces;
using XMLDatabase.DataAccessors;

namespace ElectroShopMobile.CustomAttributes
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private IEmployeeService userService;

        public UserAuthorizeAttribute()
        {
            InitializeService(userService);
        }

        public void InitializeService(IEmployeeService userService)
        {
            IUserDataAccessor userDataAccessor = new UserDataAccessor();
            this.userService = new EmployeeService(userDataAccessor);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            User user = userService.GetByUsername(HttpContext.Current.User.Identity.Name);
            if (httpContext.User.IsInRole(base.Roles))
                return true;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "MyAccount",
                        action = "Login"
                    })
                );
        }
    }
}