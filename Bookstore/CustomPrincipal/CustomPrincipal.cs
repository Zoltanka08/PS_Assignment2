using Bookstore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bookstore.CustomPrincipal
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public string Username { get; set; }

        public string Role { get; set; }

        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
               !string.IsNullOrWhiteSpace(role) && Role.Equals(role);
        }
    }
}