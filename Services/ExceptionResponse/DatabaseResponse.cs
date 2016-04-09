using System.Net;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class DatabaseResponse : ICustomResponse
    {
        private string message = "Database error!";
        private HttpStatusCode status = HttpStatusCode.InternalServerError;

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