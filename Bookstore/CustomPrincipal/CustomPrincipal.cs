using Bookstore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Bookstore.CustomPrincipalNamespace
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public IIdentity Identity { get; private set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
               !string.IsNullOrWhiteSpace(role) && Role.Equals(role);
        }

        IIdentity IPrincipal.Identity
        {
            get { return this.Identity; }
        }
        public string Firstname { get; set; }

        public string Lastname { get; set; }
    }
}