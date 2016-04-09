using System.Net;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class BusinessResponse : ICustomResponse
    {
        private string message = "Business validation error!";
        private HttpStatusCode status = HttpStatusCode.NotAcceptable;

        public string Message
        {
            get { return this.message; }
        }

        public HttpStatusCode Status
        {
            get { return this.status; }
        }
    }
}