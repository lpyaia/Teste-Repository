using HBSIS.MercadoLes.Integracao.SapBrf.IntegracaoCutoffWebService;
using HBSIS.MercadoLes.Services.Commons;
using HBSIS.MercadoLes.Services.Commons.Helpers;
using HBSIS.MercadoLes.Services.Commons.Integration;
using HBSIS.MercadoLes.Services.Commons.Integration.Config;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Wrapper
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
            response = Channel.SI_CUSTO_ADICIONAL_FRETE_OUTAsync(request).GetAwaiter().GetResult();

            bool isValid = !string.IsNullOrEmpty(response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Status);

            if (!isValid)
            {
                CurrentLog.SetError(response.MT_CUSTO_ADICIONAL_FRETE_HBSIS_Response.Status, response);
            }

            return isValid;
        }
    }
}