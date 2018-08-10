using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons.Config;
using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Data.Mongo;
using HBSIS.MercadoLes.Commons.Enums;
using HBSIS.MercadoLes.Commons.Helpers;
using MongoDB.Driver;
using System;
using System.Linq;

namespace HBSIS.MercadoLes.Commons.Logging.Message
{
    public static class MessageLogger
    {
        private static readonly object _lock = new object();
        private static IMongoDatabase _session = null;

        public static IMongoDatabase Session
        {
            get
            {
                lock (_lock)
                {
                    if (_session == null)
                    {
                        var connStringName = Configuration.Actual.GetLogConnectionStringName();
                        _session = MongoFactory.CreateSession(connStringName);
                    }

                    return _session;
                }
            }
        }

        public static LogMessage Get(string token)
        {
            return GetInternal(token);
        }

        public static void Sended<T>(T message)
           where T : class, ICallbackMessage
        {
            SaveInternal(message, StatusMessage.Sended);
        }

        public static void Received<T>(T message)
          where T : class, ICallbackMessage
        {
            SaveInternal(message, StatusMessage.Received);
        }

        public static void Consumed<T>(T message, bool isSuccess = true, string error = null)
             where T : class, ICallbackMessage
        {
            if (isSuccess)
            {
                SaveInternal(message, StatusMessage.Consumed);
            }
            else
            {
                SaveInternal(message, StatusMessage.Warning, error);
            }
        }

        public static void Error<T>(T message, string error = null)
             where T : class, ICallbackMessage
        {
            SaveInternal(message, StatusMessage.Error, error);
        }

        private static void SaveInternal<T>(T message, StatusMessage status, string error = null)
             where T : class, ICallbackMessage
        {
            if (message == null || string.IsNullOrEmpty(message.Token)) return;

            try
            {
                if (Session == null) return;

                var filter = Builders<LogMessage>.Filter.Eq(x => x.Id, message.Token);
                var input = Session.GetCollection<LogMessage>().GetOrDefault(filter);

                if (input == null && status == StatusMessage.Sended)
                {
                    input = new LogMessage();
                    input.Id = message.Token;
                    input.Date = DateHelper.Now;
                    input.Name = message.RequestName;

                    Session.GetCollection<LogMessage>().InsertOneAsync(input);
                }

                if (input != null)
                {
                    var detail = input.Messages.FirstOrDefault(x => x.RequestId == message.RequestId);

                    if (detail == null)
                    {
                        detail = new LogMessage.MessageDetail();
                        detail.RequestId = message.RequestId;
                        detail.Name = message.RequestName;
                        detail.Message = JsonHelper.Serialize(message);

                        input.Messages.Add(detail);
                    }

                    if (status == StatusMessage.Sended)
                        detail.Sender = Configuration.Actual.GetAppName();

                    if (status == StatusMessage.Received)
                        detail.Consumer = Configuration.Actual.GetAppName();

                    detail.Date = DateHelper.Now;
                    detail.Status = status;
                    detail.Error = error;

                    var index = input.Messages.IndexOf(detail);
                    var update = Builders<LogMessage>.Update.Set(x => x.Messages[index], input.Messages[index]);

                    Session.GetCollection<LogMessage>().Update(filter, update);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Ocorreu um erro ao salvar o LogMessage.", ex);
            }
        }

        private static LogMessage GetInternal(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;

            try
            {
                if (Session == null) return null;

                var filter = Builders<LogMessage>.Filter.Eq(x => x.Id, token);
                return Session.GetCollection<LogMessage>().GetOrDefault(filter);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Ocorreu um erro ao obter LogMessage.", ex);
                return null;
            }
        }
    }
}