using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Commons.Exceptions;
using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Result;
using HBSIS.GE.FileImporter.Services.Commons.Base.Message;
using HBSIS.GE.FileImporter.Services.Commons.Cache;
using HBSIS.GE.FileImporter.Services.Commons.Config;
using HBSIS.GE.FileImporter.Services.Commons.Helpers;
using HBSIS.GE.FileImporter.Services.Commons.Logging.Message;
using System;

namespace HBSIS.GE.FileImporter.Services.Commons.Base.Service
{
    public abstract class BusinessService<TMessage, TDto> : BaseService<TMessage, TDto>
       where TMessage : BaseMessage<TMessage>
       where TDto : class, IDto
    {
        public override void StoreMessage(TMessage message)
        {
            var result = ProcessMessage(message);
        }

        protected abstract Result Process(TMessage message);

        public virtual Result<TDto> ProcessMessage(TMessage message)
        {
            if (message == null) return ResultBuilder<TDto>.Warning("Mensagem incorreta.");

            MessageLogger.Received(message);

            SaveContext(message);
            PreProcess(message);

            try
            {
                TDto dto = null;

                var result = ValidateMessage(message);

                var isSuccess = result.IsSuccess();

                if (isSuccess)
                {
                    result = Process(message);
                    isSuccess = result.IsSuccess();
                }

                return ResultBuilder<TDto>.Return(result, dto);
            }
            catch (HBFlowException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageLogger.Error(message);
                throw;
            }
        }
        //}

        protected virtual void PreProcess(TMessage message)
        {
            ClearCache();
            ClearMessages();
        }

        protected virtual void SaveContext(TMessage message)
        {
            if (message == null) return;

            GlobalSettings.CurrentUserName = message.UserName;
        }

        protected virtual Result ValidateMessage(TMessage message)
        {
            return ResultBuilder.Success();
        }

        protected virtual TDto ProcessDto(CacheCollection cache)
        {
            return cache.GetFirstOf<TDto>();
        }
    }
}