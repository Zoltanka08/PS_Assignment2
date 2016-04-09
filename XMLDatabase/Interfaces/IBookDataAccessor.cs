using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace XMLDatabase.Interfaces
{
    public interface IBookDataAccessor
    {
        bool Insert(Book book);
        bool Update(Book book);
        bool Delete(int id);
        IEnumerable<Book> GetAll();
        Book GetById(int id);
    }
}
