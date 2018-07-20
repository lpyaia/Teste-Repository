using HBSIS.Framework.Commons.Exceptions;
using System;
using System.Runtime.Serialization;

namespace HBSIS.Framework.Commons.Exceptions
{
    [Serializable]
    public class HBFlowException : HBException
    {
        public HBFlowException() : base()
        {
        }

        public HBFlowException(string message) : base(message)
        {
        }

        public HBFlowException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HBFlowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}