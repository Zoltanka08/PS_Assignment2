
using Services.AbstractResponseFactory;
using Services.ConcreteResponseFactory;
using Services.Interfaces;

namespace Services.CustomExceptions
{
    public class BusinessException : GenericException, IException
    {
        public BusinessException()
        {
            base.ErrorCode = "500";
            base.ErrorDescription = "Business logic error!";
        }
        public BusinessException(string errorCode, string errorDescription) : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
        }
        public ICustomResponse GetResponse()
        {
            ResponseFactory factory = new BusinessResponseFactory();
            ResponseAssembler assembler = new ResponseAssembler();
            return assembler.AssembleResponse(factory);
        }
    }
}