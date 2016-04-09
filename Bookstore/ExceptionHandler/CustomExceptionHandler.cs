using System;
using Services.Interfaces;
using Services.CustomExceptions;

namespace Bookstore.ExceptionHandler
{
    public class CustomExceptionHandler
    {
        private IException exception;

        public CustomExceptionHandler(IException exception)
        {
            this.exception = exception;
        }

        public ICustomResponse Handle()
        {
            return exception.GetResponse();
        }
    }
}