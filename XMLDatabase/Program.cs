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
            //IUserDataAccessor userDataAccessor = new UserDataAccessor();
            //List<User> users = userDataAccessor.GetAll().ToList();
            //User userToUpdate = users.First(u => u.Id == 2);
            //userToUpdate.Lastname = "SamdJD";
            //userDataAccessor.Update(userToUpdate);
            //users = userDataAccessor.GetAll().ToList();
            //userDataAccessor.Delete(2);
            //users = userDataAccessor.GetAll().ToList();
            //User user = userDataAccessor.GetUserByUsername("Zoli12");

            //IBookDataAccessor bookDataAccessor = new BookDataAccessor();
            //List<Book> books = bookDataAccessor.GetAll().ToList();
            //Book bookToUpdate = books.First(b => b.Id == 1);
            //bookToUpdate.Genre = "New";
            //bookDataAccessor.Update(bookToUpdate);
            //bookToUpdate = bookDataAccessor.GetById(1);
            //bookDataAccessor.Delete(1);

        }
    }
}
