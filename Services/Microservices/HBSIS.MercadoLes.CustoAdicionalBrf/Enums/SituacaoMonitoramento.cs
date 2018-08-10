using System.ComponentModel;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Enums
{
    public enum SituacaoMonitoramento
    {
        [Description("Não iniciada")]
        NaoIniciada = 1,

        [Description("Em andamento")]
        EmAndamento = 2,

        [Description("Finalizada")]
        Finalizada = 3,

        [Description("Todas")]
        Todas
    }
}
