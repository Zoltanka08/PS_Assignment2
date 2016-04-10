using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.CustomPrincipalNamespace
{
    public class CustomPrincipalSerializeModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}