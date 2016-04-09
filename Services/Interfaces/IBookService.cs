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
        void Insert(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
