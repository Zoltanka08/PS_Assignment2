using Services.AbstractResponseFactory;
using Services.Interfaces;

namespace Services.ConcreteResponseFactory
{
    public class GenericResponseFactory : ResponseFactory
    {
        public override ICustomResponse GetResponse()
        {
            return new GenericResponse();
        }
    }
}