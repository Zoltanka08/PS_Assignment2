using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
    }
}