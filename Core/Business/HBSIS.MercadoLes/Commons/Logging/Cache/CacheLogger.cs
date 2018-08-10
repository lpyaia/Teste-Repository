using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Data.Mongo;
using HBSIS.MercadoLes.Commons.Helpers;
using MongoDB.Driver;
using System;

namespace HBSIS.MercadoLes.Commons.Logging.Cache
{
    public static class CacheLogger
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
                        var connStringName = HBSIS.Framework.Commons.Config.Configuration.Actual.GetLogConnectionStringName();
                        _session = MongoFactory.CreateSession(connStringName);
                    }

                    return _session;
                }
            }
        }

        public static void Update(string key)
        {
            SaveInternal(key);
        }

        public static void Delete(string key)
        {
            DeleteInternal(key);
        }

        private static void SaveInternal(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            try
            {
                if (Session == null) return;

                var filter = Builders<LogCache>.Filter.Eq(x => x.Id, key);
                var input = Session.GetCollection<LogCache>().GetOrDefault(filter);

                if (input == null)
                {
                    input = new LogCache();
                    input.Id = key;
                    input.Created = DateHelper.Now;

                    Session.GetCollection<LogCache>().Insert(input);
                }

                input.LastUpdate = DateHelper.Now;

                var update = Builders<LogCache>.Update.Set(x => x.LastUpdate, input.LastUpdate);

                Session.GetCollection<LogCache>().Update(filter, update);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Ocorreu um erro ao salvar o LogCache.", ex);
            }
        }

        private static void DeleteInternal(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            try
            {
                if (Session == null) return;

                var filter = Builders<LogCache>.Filter.Eq(x => x.Id, key);
                Session.GetCollection<LogCache>().Delete(filter);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"Ocorreu um erro ao excluir o LogCache.", ex);
            }
        }
    }
}