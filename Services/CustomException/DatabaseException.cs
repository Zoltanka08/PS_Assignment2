using Services.AbstractResponseFactory;
using Services.ConcreteResponseFactory;
using Services.Interfaces;

namespace Services.CustomExceptions
{
    public class DatabaseException : GenericException, IException
    {
        public DatabaseException()
        {
            base.ErrorCode = "500";
            base.ErrorDescription = "Database error!";
        }
        public DatabaseException(string errorCode, string errorDescription) : base()
        {
            base.ErrorCode = errorCode;
            base.ErrorDescription = errorDescription;
        }

        public ICustomResponse GetResponse()
        {
            ResponseFactory factory = new DatabaseResponseFactory();
            ResponseAssembler assembler = new ResponseAssembler();
            return assembler.AssembleResponse(factory);
        }
    }
}