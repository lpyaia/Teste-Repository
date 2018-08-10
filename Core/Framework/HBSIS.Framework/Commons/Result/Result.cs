using System;
using System.Collections.Generic;
using System.Linq;

namespace HBSIS.Framework.Commons.Result
{
    [Serializable]
    public class Result
    {
        private List<string> _messages = new List<string>();

        public Result(ResultStatus status)
        {
            Status = status;
        }

        public Result(ResultStatus status, params string[] messages)
        {
            Status = status;

            if (messages != null && messages.Length > 0)
            {
                foreach (var message in messages)
                {
                    if (!string.IsNullOrWhiteSpace(message))
                        _messages.Add(message);
                }
            }
        }

        public Result(Exception exception)
            : this(ResultStatus.Error)
        {
            Exception = exception;

            if (exception == null) return;

            _messages.Add(exception.ToString());

            if (exception.InnerException != null)
            {
                _messages.Add(exception.InnerException.ToString());
            }
        }

        public Result(Exception exception, string customMessageError)
           : this(exception)
        {
            if (!string.IsNullOrWhiteSpace(customMessageError))
                _messages.Add(customMessageError);
        }

        public Result(Result result)
        {
            Status = result.Status;
            Exception = result.Exception;
            _messages = result.Messages.ToList();
        }

        public ResultStatus Status { get; protected set; }
        public Exception Exception { get; }

        public IEnumerable<string> Messages
        {
            get { return _messages ?? new List<string>(); }
        }
    }
}