using Services.CustomExceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CustomException
{
    public class ReportException : Exception
    {
        public ReportException()
        {
        }

        public ReportException(string message)
            : base(message)
        {
        }

        public ReportException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
