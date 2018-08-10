using HBSIS.Framework.Bus;
using HBSIS.Framework.Bus.Cache;
using HBSIS.Framework.Bus.EasyNetQRabbit;
using HBSIS.Framework.Bus.Message;
using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Result;
using HBSIS.GE.FileImporter.Services.Commons.Logging;
using HBSIS.GE.FileImporter.Services.Commons.Logging.Message;
using System;

namespace HBSIS.GE.FileImporter.Services.Commons.Cache
{
    public class CacheConsumer : BaseConsumer<CacheMessage>
    {
        public CacheConsumer(string contextName)
            : base(contextName)
        {
        }

        public override void Consume(CacheMessage message)
        {
            var result = ProcessCache(message);

            LoggerHelper.Log(result);
        }

        private Result ProcessCache(CacheMessage message)
        {
            try
            {
                if (message == null) return ResultBuilder.Warning("Mensagem inválida.");

                MessageLogger.Received(message);

                //var cacher = Cachers.Actual[message.ContentType];

                //if (cacher == null) return ResultBuilder.Warning($"{message.ContentType}: Cacher não encontrado.");

                //cacher.StoreCache(message);

                ResultBuilder.Warning($"{message.ContentType}: Cacher não encontrado.");

                MessageLogger.Consumed(message);

                return ResultBuilder.Success();
            }
            catch (Exception)
            {
                MessageLogger.Error(message);
                throw;
            }
        }
    }
}