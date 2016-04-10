using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.FactoryDP.AbstractProductInterface
{
    public interface IReport
    {
        void CreateReport(IBookService bookService);
    }
}