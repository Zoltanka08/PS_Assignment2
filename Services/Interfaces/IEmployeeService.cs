using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        User GetByUsername(string username);
        IEnumerable<User> GetAll();
        void Insert(User employee);
        void Update(User employee);
        void Delete(int id);
        User GetById(int id);
    }
}
