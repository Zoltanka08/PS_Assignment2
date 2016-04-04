using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.DataAccessors;
using XMLDatabase.Interfaces;
using XMLDatabase.Models;

namespace XMLDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserDataAccessor userDataAccessor = new UserDataAccessor();
            List<User> users = userDataAccessor.GetAll().ToList();
        }
    }
}
