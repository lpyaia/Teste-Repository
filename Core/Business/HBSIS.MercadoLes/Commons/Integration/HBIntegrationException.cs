using HBSIS.Framework.Commons.Exceptions;
using System;
using System.Runtime.Serialization;

namespace HBSIS.MercadoLes.Commons.Integration
{
    public class HBIntegrationException : HBException
    {
        public HBIntegrationException()
        {
        }

        public HBIntegrationException(string message) : base(message)
        {
        }

        public HBIntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HBIntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}