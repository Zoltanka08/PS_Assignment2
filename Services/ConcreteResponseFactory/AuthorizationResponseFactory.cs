using Services.AbstractResponseFactory;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class AuthorizationResponseFactory : ResponseFactory
    {
        public override ICustomResponse GetResponse()
        {
            return new AuthorizationResponse();
        }
    }
}