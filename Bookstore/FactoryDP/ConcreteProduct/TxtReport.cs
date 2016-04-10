using Bookstore.FactoryDP.AbstractProductInterface;
using Services.CustomExceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Services.CustomExceptions;
using Services.CustomException;
using XMLDatabase.Models;
using System.Text;
using System.Reflection;

namespace Bookstore.FactoryDP.ConcreteProduct
{
    public class TxtReport : IReport
    {
        private const string fileName = @"E:\University\AnIII\Sem II\PS\Lab\Assigment2\Bookstore\Bookstore\Report\TxtReport.txt";
        public void CreateReport(IBookService bookService)
        {
            IEnumerable<Book> books = bookService.GetAll().Where(b => b.Quantity <= 0);

            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            WriteFile(books, fileName);         
        }

        private void WriteFile(IEnumerable<Book> books, string fileName)
        {
            FileStream text = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(text);

            foreach (Book book in books)
            {
                string bookFormattedData = FromatBookData(book);
                writer.WriteLine(bookFormattedData);
            }
            writer.Close();
        }

        private string FromatBookData(Book book)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Type type = typeof(Book);
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                string name = propertyInfo.Name;
                object value = propertyInfo.GetValue(book, null);
                stringBuilder.AppendLine(name + " = " + value.ToString());
            }
            stringBuilder.Append(Environment.NewLine);

            return stringBuilder.ToString();
        }
    }
}