using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Services.Messages.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HBSIS.GE.Microservices.FileImporter.Producer.FileProcessStrategies
{
    public class ClienteFileProcess : FileProcessStrategy
    {
        public override void Process(DataSet excelSpreadsheet)
        {
            foreach (DataRow rowColumn in excelSpreadsheet.Tables[0].Rows)
            {
                ClienteSpreadsheetLine clienteSpreadsheetLine = new ClienteSpreadsheetLine();

                if (rowColumn.ItemArray.Count() == 30)
                {
                    clienteSpreadsheetLine.Codigo = rowColumn[0].ToString();
                    clienteSpreadsheetLine.Cliente = rowColumn[1].ToString();
                    clienteSpreadsheetLine.Rua = rowColumn[2].ToString();
                    clienteSpreadsheetLine.Bairro = rowColumn[3].ToString();
                    clienteSpreadsheetLine.Cidade = rowColumn[4].ToString();
                    clienteSpreadsheetLine.Estado = rowColumn[5].ToString();

                    clienteSpreadsheetLine.Tipo = rowColumn[6].ToString();
                    clienteSpreadsheetLine.PotencialCVA = rowColumn[7].ToString();
                    clienteSpreadsheetLine.TempoAtendimento = rowColumn[8].ToString();
                    clienteSpreadsheetLine.TempoTratativa = rowColumn[9].ToString();
                    clienteSpreadsheetLine.RestricaoDias = rowColumn[10].ToString();
                    clienteSpreadsheetLine.PrimeiraAbertura = rowColumn[11].ToString();
                    clienteSpreadsheetLine.PrimeiroFechamento = rowColumn[12].ToString();
                    clienteSpreadsheetLine.SegundaAbertura = rowColumn[13].ToString(); ;
                    clienteSpreadsheetLine.SegundoFechamento = rowColumn[14].ToString();

                    clienteSpreadsheetLine.Contato1 = rowColumn[15].ToString();
                    clienteSpreadsheetLine.TelefoneContato1 = rowColumn[16].ToString();
                    clienteSpreadsheetLine.EnviarSmsContato1 = rowColumn[17].ToString();

                    clienteSpreadsheetLine.Contato2 = rowColumn[18].ToString();
                    clienteSpreadsheetLine.TelefoneContato2 = rowColumn[19].ToString();
                    clienteSpreadsheetLine.EnviarSmsContato2 = rowColumn[20].ToString();

                    clienteSpreadsheetLine.Contato3 = rowColumn[21].ToString();
                    clienteSpreadsheetLine.TelefoneContato3 = rowColumn[22].ToString();
                    clienteSpreadsheetLine.EnviarSmsContato3 = rowColumn[23].ToString();

                    clienteSpreadsheetLine.Contato4 = rowColumn[24].ToString();
                    clienteSpreadsheetLine.TelefoneContato4 = rowColumn[25].ToString();
                    clienteSpreadsheetLine.EnviarSmsContato4 = rowColumn[26].ToString();

                    clienteSpreadsheetLine.Contato5 = rowColumn[27].ToString();
                    clienteSpreadsheetLine.TelefoneContato5 = rowColumn[28].ToString();
                    clienteSpreadsheetLine.EnviarSmsContato5 = rowColumn[29].ToString();

                    FileImporterMessage message = new FileImporterMessage("GE-Clientes-01-", clienteSpreadsheetLine);

                    SendMessage(message);
                }
            }
        }
    }
}
