﻿using HBSIS.Framework.Bus.Bus;
using HBSIS.Framework.Commons.Exceptions;
using HBSIS.Framework.Commons.Helper;
using HBSIS.Framework.Commons.Result;
using HBSIS.MercadoLes.Commons.Base.Message;
using HBSIS.MercadoLes.Commons.Cache;
using HBSIS.MercadoLes.Commons.Config;
using HBSIS.MercadoLes.Commons.Helpers;
using HBSIS.MercadoLes.Commons.Logging.Message;
using System;

namespace HBSIS.MercadoLes.Commons.Base.Service
{
    public abstract class BusinessService<TMessage, TDto> : BaseService<TMessage, TDto>
       where TMessage : BaseMessage<TMessage>
       where TDto : class, IDto
    {
        public override void StoreMessage(TMessage message)
        {
            var result = ProcessMessage(message);

            //LoggerHelper.Log(result);
        }

        protected abstract Result Process(TMessage message);

        public virtual Result<TDto> ProcessMessage(TMessage message)
        {
            if (message == null) return ResultBuilder<TDto>.Warning("Mensagem incorreta.");

            MessageLogger.Received(message);

            SaveContext(message);
            PreProcess(message);

            //// Criar e instanciar factory do Dapper para chama-lo no Program
            //using (FactoryProvider.CurrentFactory.GetDataContext())
            //{
            try
            {
                TDto dto = null;

                var result = ValidateMessage(message);

                var isSuccess = result.IsSuccess();

                if (isSuccess)
                {
                    //// Abrir Transação com o Dapper
                    //using (var tx = TransactionFactory.GetTransaction())
                    //{
                    result = Process(message);

                    // if (result.IsSuccess())
                    //   tx.Commit();
                    //}

                    isSuccess = result.IsSuccess();
                }

                if (isSuccess)
                {
                    if (Caches.Count > 0)
                    {
                        dto = ProcessDto(Caches);
                        dto.Send(message);
                    }

                    SendMessages(message);
                }

                MessageLogger.Consumed(message, isSuccess, result.MessageToString());

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