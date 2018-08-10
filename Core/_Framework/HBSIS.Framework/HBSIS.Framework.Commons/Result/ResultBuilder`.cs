using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Commons.Result
{
    public static class ResultBuilder<T>
    {
        public static Result<T> Success(T value = default(T)) => new Result<T>(ResultStatus.Success, value);

        public static Result<T> Warning(string message = null) => new Result<T>(ResultStatus.Warning, message);

        public static Result<T> Warning(IEnumerable<string> messages) => new Result<T>(ResultStatus.Warning, messages.ToArray());

        public static Result<T> Error(Exception ex) => new Result<T>(ex);

        public static Result<T> Error(string message = null) => new Result<T>(ResultStatus.Error, message);

        public static Result<T> Return(Result result, T value = default(T)) => new Result<T>(result, value);
    }
}