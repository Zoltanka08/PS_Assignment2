using Bookstore.FactoryDP.AbstractFactoryInterface;
using Bookstore.FactoryDP.AbstractProductInterface;
using Bookstore.FactoryDP.ConcreteProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Interfaces;

namespace Bookstore.FactoryDP.ConcreteFactory
{
    public class TxtReportFactory : IReportFactory 
    {
        public AbstractProductInterface.IReport GetReport()
        {
            IReport txtReport = new TxtReport();
            return txtReport;
        }
    }
}