using HBSIS.Framework.Commons;
using HBSIS.MercadoLes.Infra;
using SI_CUSTO_ADICIONAL_FRETE_OUTService;

namespace HBSIS.MercadoLes.CustoAdicionalBrf
{
    public interface IIntegracaoSapBrfIntegrator
    {
        void ReenviarTodos();

        void Enviar(SI_CUSTO_ADICIONAL_FRETE_OUTRequest envioXml);
    }
}