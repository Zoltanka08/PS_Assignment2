using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XMLDatabase.Models;

namespace Services.Interfaces
{
    public interface IBookService
    {
        Book GetById(int id);
        IEnumerable<Book> GetAll();
        bool Insert(Book book);
        bool Update(Book book);
        bool Delete(int id);
    }
}
