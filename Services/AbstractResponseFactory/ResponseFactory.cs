using Services.Interfaces;

namespace Services.AbstractResponseFactory
{
    public abstract class ResponseFactory
    {
        public abstract ICustomResponse GetResponse();
    }
}