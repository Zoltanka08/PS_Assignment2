using Services.AbstractResponseFactory;
using Services.ConcreteResponseFactory;
using Services.Interfaces;

namespace Services.CustomExceptions
{
    public class AuthorizationException : GenericException, IException
    {
        public AuthorizationException()
        {
            base.ErrorCode = "401";
            base.ErrorDescription = "Unauthorized";
        }
        public AuthorizationException(string errorCode, string errorDescription) : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
        }

        public ICustomResponse GetResponse()
        {
            ResponseFactory factory = new AuthorizationResponseFactory();
            ResponseAssembler assembler = new ResponseAssembler();
            return assembler.AssembleResponse(factory);
        }
    }
}