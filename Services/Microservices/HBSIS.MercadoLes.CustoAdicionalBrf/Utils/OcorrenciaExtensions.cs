using HBSIS.MercadoLes.CustoAdicionalBrf.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Utils
{
    public static class OcorrenciaExtensions
    {
        public static void AdicionarOcorrencia(this List<Ocorrencia> ocorrencias, Ocorrencia ocorrencia)
        {
            if (ocorrencia != null && ocorrencia.ExibirOcorrenciaNoXml())
                ocorrencias.Add(ocorrencia);
        }
    }
}
