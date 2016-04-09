using Services.Interfaces;

namespace Services.AbstractResponseFactory
{
    public class ResponseAssembler
    {
        public ICustomResponse AssembleResponse(ResponseFactory factory)
        {
            return factory.GetResponse();
        }
    }
}