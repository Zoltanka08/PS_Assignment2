using Bookstore.FactoryDP.AbstractFactoryInterface;
using Bookstore.FactoryDP.ConcreteProduct;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.FactoryDP.ConcreteFactory
{
    public class DefaultReportFactory: IReportFactory 
    {
        public AbstractProductInterface.IReport GetReport()
        {
            return new TxtReport();
        }
    }
}