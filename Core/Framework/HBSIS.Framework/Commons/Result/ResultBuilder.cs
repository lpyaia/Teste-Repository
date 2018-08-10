using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Commons.Result
{
    public static class ResultBuilder
    {
        public static Result Success(string message = null) => new Result(ResultStatus.Success, message);

        public static Result Warning(string message = null) => new Result(ResultStatus.Warning, message);

        public static Result Warning(IEnumerable<string> messages) => new Result(ResultStatus.Warning, messages.ToArray());

        public static Result Error(Exception ex) => new Result(ex);

        public static Result Error(string message = null) => new Result(ResultStatus.Error, message);
    }
}