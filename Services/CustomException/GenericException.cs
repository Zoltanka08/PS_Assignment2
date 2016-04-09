using System;
using Services.AbstractResponseFactory;
using Services.ConcreteResponseFactory;
using Services.Interfaces;

namespace Services.CustomExceptions
{
    public class GenericException : System.Exception, IException
    {
        public virtual string ErrorCode { get; set; }
        public virtual string ErrorDescription { get; set; }

        public ICustomResponse GetResponse()
        {
            ResponseFactory factory = new GenericResponseFactory();
            ResponseAssembler assembler = new ResponseAssembler();
            return assembler.AssembleResponse(factory);
        }
    }
}