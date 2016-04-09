using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Interfaces
{
    public interface ICustomPrincipal
    {
        string Username { get; set; }

        string Role { get; set; }
    }
}
