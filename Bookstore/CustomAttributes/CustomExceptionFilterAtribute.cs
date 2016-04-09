using System;
using System.Net.Http;
using System.Web.Http.Filters;
using Services.AbstractResponseFactory;
using Bookstore.ExceptionHandler;
using Services.Interfaces;

namespace Bookstore.CustomAttributes
{
    public class CustomExceptionFilter : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            var exceptionType = actionExecutedContext.Exception.GetType();
            IException exception = (IException)Activator.CreateInstance(exceptionType);
            CustomExceptionHandler handler = new CustomExceptionHandler(exception);
            var response = handler.Handle();
        
            actionExecutedContext.Response = new HttpResponseMessage()
            {
                Content = new StringContent(response.Message, System.Text.Encoding.UTF8, "text/plain"),
                StatusCode = response.Status
            };

            base.OnException(actionExecutedContext);
        }
    }
}