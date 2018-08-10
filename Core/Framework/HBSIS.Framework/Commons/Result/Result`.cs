using System;

namespace HBSIS.Framework.Commons.Result
{
    [Serializable]
    public class Result<T> : Result
    {
        public Result(ResultStatus status)
            : base(status)
        {
        }

        public Result(T value)
           : this(ResultStatus.Success, value)
        {
        }

        public Result(ResultStatus status, T value)
            : base(status)
        {
            Value = value;
        }

        public Result(ResultStatus status, params string[] messages)
            : base(status, messages)
        {
        }

        public Result(Exception exception)
            : base(exception)
        {
        }

        public Result(Result result, T value = default(T))
            : base(result)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}