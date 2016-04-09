using Services.AbstractResponseFactory;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class DatabaseResponseFactory : ResponseFactory
    {
        public override ICustomResponse GetResponse()
        {
            return new DatabaseResponse();
        }
    }
}