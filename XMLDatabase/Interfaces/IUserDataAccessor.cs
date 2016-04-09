using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace XMLDatabase.Interfaces
{
    public interface IUserDataAccessor
    {
        bool Insert(User user);
        bool Update(User user);
        bool Delete(int id);
        IEnumerable<User> GetAll();
        User GetUserByUsername(string username);
    }
}
