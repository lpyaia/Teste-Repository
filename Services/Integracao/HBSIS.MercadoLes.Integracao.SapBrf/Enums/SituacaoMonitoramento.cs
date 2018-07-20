using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Enums
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
