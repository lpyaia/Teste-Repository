using HBSIS.MercadoLes.Integracao.SapBrf.IntegracaoCutoffWebService;
using HBSIS.MercadoLes.Services.Commons.Integration;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;
using System;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Wrapper
{
    public interface IIntegracaoSapBrfWrapper : IIntegrationSender<SI_CUSTO_ADICIONAL_FRETE_OUTRequest, SI_CUSTO_ADICIONAL_FRETE_OUTResponse>
    {

    }
}