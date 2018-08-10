using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels
{
    public class ClienteSpreadsheetLine : SpreadsheetLineBase
    {
        public string Codigo { get; set; }
        public string Cliente { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string PotencialCVA { get; set; }
        public string TempoAtendimento { get; set; }
        public string TempoTratativa { get; set; }
        public string RestricaoDias { get; set; }
        public string PrimeiraAbertura { get; set; }
        public string PrimeiroFechamento { get; set; }
        public string SegundaAbertura { get; set; }
        public string SegundoFechamento { get; set; }
        public string Contato1 { get; set; }
        public string TelefoneContato1 { get; set; }
        public string EnviarSmsContato1 { get; set; }
        public string Contato2 { get; set; }
        public string TelefoneContato2 { get; set; }
        public string EnviarSmsContato2 { get; set; }
        public string Contato3 { get; set; }
        public string TelefoneContato3 { get; set; }
        public string EnviarSmsContato3 { get; set; }
        public string Contato4 { get; set; }
        public string TelefoneContato4 { get; set; }
        public string EnviarSmsContato4 { get; set; }
        public string Contato5 { get; set; }
        public string TelefoneContato5 { get; set; }
        public string EnviarSmsContato5 { get; set; }
    }
}
