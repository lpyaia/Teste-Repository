using HBSIS.Framework.Commons;
using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Services.Commons.Integration.Config;
using HBSIS.MercadoLes.Services.Commons.Integration.Log;
using System;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    public abstract class IntegrationSender<TRequest, TResponse, TLog> : IIntegrationSender<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
        where TLog : class, ILogIntegrationSender, new()

    {
        public IntegrationSender(IIntegrationConfig config)
        {
            Attempts = 3;
            TakePendents = 1000;

            if (config == null)
                throw new HBIntegrationException("Config not defined.");

            Config = config;
        }

        protected TLog CurrentLog { get; private set; }

        protected IIntegrationConfig Config { get; }

        public int Attempts { get; set; }

        public int TakePendents { get; set; }

        protected TLog CreateLog(TRequest request)
        {
            try
            {
                return CreateLogInternal(request);
            }
            catch (Exception ex)
            {
                throw new HBIntegrationException("Could not create Log.", ex);
            }
        }

        protected abstract TLog CreateLogInternal(TRequest request);

        public void SendSync(TRequest request)
        {
            if (request == null)
                throw new HBIntegrationException("Request not defined.");

            if (CurrentLog == null)
                CurrentLog = CreateLog(request);

            if (!Config.Enabled) return;

            try
            {
                CurrentLog.SetSending();

                var response = default(TResponse);

                var isValid = SendInternal(request, out response);

                if (isValid)
                    CurrentLog.SetSuccess(response);
            }
            catch (Exception ex)
            {
                CurrentLog.SetError(ex.Message);
                LoggerHelper.Error(ex);
            }
            finally
            {
                CurrentLog.Retries++;
                CurrentLog.Save();
            }
        }

        protected abstract bool SendInternal(TRequest request, out TResponse response);

        public void SendAsync(TRequest request)
        {
            if (request == null)
                throw new HBIntegrationException("Request not defined.");

            CurrentLog = CreateLog(request);
        }

        public void Resend(Guid idRequest)
        {
            CurrentLog = LogIntegrationBuilder.Get<TLog>(idRequest);

            if (CurrentLog != null)
            {
                var model = CurrentLog.GetRequest<TRequest>();
                SendSync(model);
            }
        }

        public void ResendAll()
        {
            if (!Config.Enabled) return;

            var pendents = GetPendents();

            foreach (var log in pendents)
            {
                CurrentLog = LogIntegrationBuilder.Get<TLog>(log.Id);
                var model = CurrentLog.GetRequest<TRequest>();

                SendSync(model);
            }
        }

        protected virtual List<TLog> GetPendents()
        {
            return LogIntegrationBuilder.ListPendents<TLog>(Attempts, TakePendents);
        }
    }
}