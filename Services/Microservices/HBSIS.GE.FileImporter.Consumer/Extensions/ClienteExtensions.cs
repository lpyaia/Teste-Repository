using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.GE.FileImporter.Consumer.Extensions
{
    public static class ClienteExtensions
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
