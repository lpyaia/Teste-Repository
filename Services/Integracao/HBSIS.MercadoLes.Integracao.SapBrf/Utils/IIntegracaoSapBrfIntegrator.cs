using HBSIS.Framework.Commons;
using HBSIS.MercadoLes.Infra.Entities;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;

namespace HBSIS.MercadoLes.Integracao.SapBrf
{
    public interface IIntegracaoSapBrfIntegrator
    {
        void Enviar(IntegracaoSapBrfModel model);

        void Enviar(EnvioXml envio);

        void ReenviarTodos();

        void EnviarTest(SI_CUSTO_ADICIONAL_FRETE_OUTRequest envioXml);
    }
}