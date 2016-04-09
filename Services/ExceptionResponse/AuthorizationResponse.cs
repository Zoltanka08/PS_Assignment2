using System.Net;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class AuthorizationResponse : ICustomResponse
    {
        private string message = "Authentification error!";
        private HttpStatusCode status = HttpStatusCode.Unauthorized;

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