﻿using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Commons.Enums;
using HBSIS.MercadoLes.Commons.Integration.Log;

namespace HBSIS.MercadoLes.Commons.Integration
{
    public static class IntegrationHelper
    {
        public static bool IsSuccess(this IIntegrationStatus value)
        {
            return value?.Status == StatusLog.Success;
        }

        public static T GetRequest<T>(this ILogIntegrationSender value)
           where T : class
        {
            if (value == null || value.Request == null) return null;

            var ret = value.Request as T;

            if (ret == null)
            {
                ret = JsonHelper.Deserialize<T>(value.Request.ToString());
            }

            return ret;
        }
    }
}