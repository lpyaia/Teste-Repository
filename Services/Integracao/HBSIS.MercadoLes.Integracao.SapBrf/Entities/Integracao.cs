using System;
using System.Collections.Generic;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Entities
{
    public class Integracao
    {
        public long NumeroRota { get; set; }
        public MultiTransporte MultiTransporte { get; set; }
        public string Data { get; set; }
        public string Placa { get; set; }
        public string CpfMotorista { get; set; }
        public string CnpjTransportador { get; set; }
        public string UnidadeNegocio { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }

        // Propriedade privada para não ser exibida no arquivo xml
        private DateTime _dtData;

        public Integracao()
        {
            Ocorrencias = new List<Ocorrencia>();
            _dtData = new DateTime();
        }

        public void AdicionarOcorrencia(Ocorrencia ocorrenciaXml)
        {
            Ocorrencias.Add(ocorrenciaXml);
        }

        public void SetDtData(DateTime data)
        {
            _dtData = data;
        }

        public DateTime GetDtData()
        {
            return _dtData;
        }

    }
}
