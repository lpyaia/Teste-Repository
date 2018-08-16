using HBSIS.Framework.Commons.Helper;
using HBSIS.MercadoLes.Commons.Helpers;
using HBSIS.MercadoLes.Commons.Integration;
using HBSIS.MercadoLes.Commons.Integration.Config;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Wrapper
{
    public class IntegracaoSapBrfWrapper :
        IntegrationSender<SI_CUSTO_ADICIONAL_FRETE_OUTRequest, SI_CUSTO_ADICIONAL_FRETE_OUTResponse, LogIntegracaoSapBrf>, IIntegracaoSapBrfWrapper
    {
        private SI_CUSTO_ADICIONAL_FRETE_OUTChannel _channel;

        public IntegracaoSapBrfWrapper(IIntegrationConfig config) : base(config)
        {
        }

        protected SI_CUSTO_ADICIONAL_FRETE_OUTChannel Channel
        {
            get
            {
                if (_channel == null)
                {
                    _channel = ChannelFactoryHelper
                        .CreateChannel<SI_CUSTO_ADICIONAL_FRETE_OUTChannel>(Config.Url, Config.UserName, Config.Password);
                }

                return _channel;
            }

            set { _channel = value; }
        }

        protected override LogIntegracaoSapBrf CreateLogInternal(SI_CUSTO_ADICIONAL_FRETE_OUTRequest request)
        {
            var log = LogIntegracaoSapBrf.Create(Config.Name, "CustoAdicionalBrfWs", System.Net.WebUtility.HtmlDecode(Config.Url), Config.UserName, Config.Password, request);

            log.NumeroRotaNegocio = request.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request.Integracao.NumeroRota;

            log.Save();

            return log;
        }

        protected override bool SendInternal(SI_CUSTO_ADICIONAL_FRETE_OUTRequest request, out SI_CUSTO_ADICIONAL_FRETE_OUTResponse response)
        {
            response = Channel.SI_CUSTO_ADICIONAL_FRETE_OUT(request);

            LoggerHelper.Info($"Rota = {request.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Request.Integracao.NumeroRota} - " +
                $"Status = {response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Status} - " +
                $"Mensagem = {response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Mensagem}");

            bool isValid = !string.IsNullOrEmpty(response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Status);

            if (!isValid)
            {
                CurrentLog.SetError(response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Status, response);
            }

            return isValid;
        }
    }
}