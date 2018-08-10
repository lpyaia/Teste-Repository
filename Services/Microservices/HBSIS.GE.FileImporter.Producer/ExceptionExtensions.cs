using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Producer
{
    public static class ExceptionExtensions
    {
        public static string GetInnerExceptionMessage(this Exception exception)
        {
            string innerMessage = string.Empty;

            if (exception.InnerException != null)
                innerMessage = exception.InnerException.Message;

            return innerMessage;
        }
    }
}
