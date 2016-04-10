using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Interfaces
{
    public interface ICustomPrincipal: IPrincipal
    {
        string Username { get; set; }
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Role { get; set; }
    }
}
