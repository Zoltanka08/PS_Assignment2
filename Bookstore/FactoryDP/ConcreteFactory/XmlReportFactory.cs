using Bookstore.FactoryDP.AbstractFactoryInterface;
using Bookstore.FactoryDP.AbstractProductInterface;
using Bookstore.FactoryDP.ConcreteProduct;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.FactoryDP.ConcreteFactory
{
    public class XmlReportFactory : IReportFactory
    {
        public AbstractProductInterface.IReport GetReport()
        {
            IReport xmlReport = new XmlReport();
            return xmlReport;
        }
    }
}