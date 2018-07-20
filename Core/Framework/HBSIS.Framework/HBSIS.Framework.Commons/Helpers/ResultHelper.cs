using HBSIS.Framework.Commons.Result;
using System;
using System.Linq;

namespace HBSIS.Framework.Commons.Helper
{
    public static class ResultHelper
    {
        public static bool IsSuccess(this Result.Result value)
        {
            return value?.Status == ResultStatus.Success;
        }

        public static string MessageToString(this Result.Result result)
        {
            var messages = result.Messages.ToList();

            if (messages.Count == 0) return string.Empty;

            var ret = string.Empty;

            if (messages.Count > 0)
            {
                var stopIndex = messages.Count - 1;

                for (int i = 0; i < messages.Count; i++)
                {
                    ret += messages[0];

                    if (i != stopIndex)
                        ret += Environment.NewLine;
                }
            }

            return ret;
        }
    }
}