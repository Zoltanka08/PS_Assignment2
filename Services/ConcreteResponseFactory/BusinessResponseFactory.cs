using Services.AbstractResponseFactory;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class BusinessResponseFactory : ResponseFactory
    {
        public override ICustomResponse GetResponse()
        {
            return new BusinessResponse();
        }
    }
}