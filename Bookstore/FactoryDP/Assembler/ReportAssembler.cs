using Bookstore.FactoryDP.AbstractFactoryInterface;
using Bookstore.FactoryDP.AbstractProductInterface;
using Bookstore.FactoryDP.ConcreteFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookstore.FactoryDP.Assembler
{
    public class ReportAssembler
    {
        IReportFactory reportFactory;

        public ReportAssembler(string reportTypeName)
        {
            if (reportTypeName.Equals("txt"))
            {
                this.reportFactory = new TxtReportFactory();
            }
            else
            if (reportTypeName.Equals("xml"))
            {
                this.reportFactory = new XmlReportFactory();
            }
            else
            {
                this.reportFactory = new DefaultReportFactory();
            }
        }

        public IReport AssembleReport()
        {
            return reportFactory.GetReport();
        }
    }
}