using Bookstore.FactoryDP.AbstractProductInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.FactoryDP.AbstractFactoryInterface
{
    public interface IReportFactory
    {
        IReport GetReport();
    }
}