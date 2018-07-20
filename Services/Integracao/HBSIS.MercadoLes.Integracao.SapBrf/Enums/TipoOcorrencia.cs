using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Enums
{
    public enum TipoOcorrencia
    {
        SaidaRevenda = 1,

        EntregaRealizada = 2,
        
        Devolucao = 3,
        
        ReversaoDevolucao = 4,
        
        InicioAlmoco = 5,
        
        FimAlmoco = 6,
        
        ChegadaRevenda = 7,
        
        InicioEspera = 8,
        
        FimEspera = 9,
        
        InicioRepouso = 10,
        
        FimRepouso = 11,
        
        InicioDescanso = 12,
        
        FimDescanso = 13,
        
        Recebimento = 14,
        
        InicioAbastecimento = 17,
        
        FimAbastecimento = 18,
        
        EvidenciaPDV = 20,
        
        Posicao = 30,
        
        PosicaoNova = 31,
        
        Mensagem = 40,
        
        ColetaRealizada = 60,
        
        ColetaCancelada = 61
    }

}
