using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Enums
{
    public enum TipoSituacaoEntrega : short
    {
        NaoApontado = 1,
        
        NaoEntregue = 2,
        
        Devolvido = 3,
        
        Entregue = 4,
        
        Revertido = 5,

        DevolvidaParcialmente = 6,
        
        RevertidaParcialmente = 7
    }
}
