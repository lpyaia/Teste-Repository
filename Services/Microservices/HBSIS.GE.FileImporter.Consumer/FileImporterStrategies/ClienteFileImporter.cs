using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Infra;
using HBSIS.GE.FileImporter.Services.Persistence;
using HBSIS.GE.FileImporter.Consumer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Dapper;

namespace HBSIS.GE.FileImporter.Consumer.FileImporterStrategies
{
    public class ClienteFileImporter : FileImporterStrategy<ClienteSpreadsheetLine>, IFileImporterStrategy
    {
        private PersistenceDataContext _persistenceDataContext;
        private List<Cliente> _lstCliente;
        private List<ClienteCelular> _lstClienteCelular;
        private List<CommandDefinition> _lstCommands;

        public ClienteFileImporter()
        {
            _persistenceDataContext = new PersistenceDataContext();
            _lstCliente = new List<Cliente>();
            _lstClienteCelular = new List<ClienteCelular>();
            _lstCommands = new List<CommandDefinition>();
        }

        public override void ImportData(ClienteSpreadsheetLine data)
        {
            try
            {
                Cliente cliente = _persistenceDataContext.ClienteRepository.GetByCodigoClienteNegocio(data.Codigo);

                if (cliente == null) return;

                _lstCommands.Add(UpdateCliente(cliente, data));

                _lstCommands.Add(UpdateClienteCelular(cliente.CdCliente, data.Contato1, data.TelefoneContato1, data.EnviarSmsContato1));
                _lstCommands.Add(UpdateClienteCelular(cliente.CdCliente, data.Contato2, data.TelefoneContato2, data.EnviarSmsContato2));
                _lstCommands.Add(UpdateClienteCelular(cliente.CdCliente, data.Contato3, data.TelefoneContato3, data.EnviarSmsContato3));
                _lstCommands.Add(UpdateClienteCelular(cliente.CdCliente, data.Contato4, data.TelefoneContato4, data.EnviarSmsContato4));
                _lstCommands.Add(UpdateClienteCelular(cliente.CdCliente, data.Contato5, data.TelefoneContato5, data.EnviarSmsContato5));

                _lstCommands = _lstCommands.Where(command => !string.IsNullOrEmpty(command.CommandText)).ToList();

                _persistenceDataContext.ClienteRepository.ExecuteCommandDefinition(_lstCommands);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CommandDefinition UpdateClienteCelular(long cdCliente, string nome, string telefone, string enviarSMS)
        {
            try
            {
                if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(telefone))
                {
                    bool enviaSMS = GetBooleanFromString(enviarSMS);
                    telefone = telefone.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");

                    ClienteCelular clienteCelular =
                        _persistenceDataContext.ClienteCelularRepository.GetByNumeroCelularAndCdCliente(cdCliente, telefone);

                    if (clienteCelular == null)
                    {
                        clienteCelular = new ClienteCelular();
                        clienteCelular.CdCliente = cdCliente;
                        clienteCelular.NrCelular = telefone;
                        clienteCelular.DtCriacao = DateTime.Now;
                        clienteCelular.IdExcluido = false;
                        clienteCelular.NmContato = nome;
                        clienteCelular.IdEnviarSMS = enviaSMS;

                        return _persistenceDataContext.ClienteCelularRepository.GetInsertImportacaoCommand(clienteCelular);
                    }

                    else
                    {
                        return _persistenceDataContext.ClienteCelularRepository.GetAtualizarNomeContadoCommand(clienteCelular.CdCelular, nome);
                    }
                }

                return new CommandDefinition();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private CommandDefinition UpdateCliente(Cliente cliente, ClienteSpreadsheetLine clienteExcel)
        {
            try
            {
                #region Validações e conversões
                cliente.DsObservacao = clienteExcel.Tipo.HasValue() ?
                    clienteExcel.Tipo :
                    cliente.DsObservacao;

                cliente.IdPotencialCliente = clienteExcel.PotencialCVA.HasValue() ?
                    GetNumberFromString(clienteExcel.PotencialCVA) :
                    cliente.IdPotencialCliente;

                cliente.TempoAtendimentoEntrega = clienteExcel.TempoAtendimento.HasValue() ?
                    GetHoursWithMinutesAndSecondsFromString(clienteExcel.TempoAtendimento) :
                    cliente.TempoAtendimentoEntrega;

                cliente.TempoTratativaDevEntrega = clienteExcel.TempoTratativa.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.TempoTratativa) :
                    new DateTime(1900, 01, 01, 00, 00, 00);

                cliente.IdDiasRestricao = clienteExcel.RestricaoDias.HasValue() ?
                    ConvertDiasRestricoes(clienteExcel.RestricaoDias) :
                    0;

                cliente.DtInicioExpediente = clienteExcel.PrimeiraAbertura.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.PrimeiraAbertura) :
                    new DateTime(1900, 01, 01, 00, 00, 00);

                cliente.DtFimExpediente = clienteExcel.PrimeiroFechamento.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.PrimeiroFechamento) :
                    new DateTime(1900, 01, 01, 00, 00, 00);

                cliente.DtInicioExpedienteAlternativo = clienteExcel.SegundaAbertura.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.SegundaAbertura) :
                    new DateTime(1900, 01, 01, 00, 00, 00);

                cliente.DtFimExpedienteAlternativo = clienteExcel.SegundoFechamento.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.SegundoFechamento) :
                    new DateTime(1900, 01, 01, 00, 00, 00);
                #endregion

                return _persistenceDataContext.ClienteRepository.GetUpdateImportacaoCommand(cliente);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool GetBooleanFromString(string textToFind)
        {
            if (textToFind.ToLower().Contains("sim") || textToFind == "1")
                return true;

            return false;
        }

        private int GetNumberFromString(string textToFind)
        {
            int number = 0;
            var resultString = Regex.Match(textToFind, @"\d+").Value;

            int.TryParse(resultString, out number);

            return number;
        }

        private int ConvertDiasRestricoes(string txtDiasRestricoes)
        {
            int[] diasSemana = new int[7];
            int somaBinaria = 0;

            foreach (char txtDia in txtDiasRestricoes)
            {
                int dia = GetNumberFromString(txtDia.ToString());

                if (dia == 0) continue;

                // Atribui ao vetor para evitar que seja somado o mesmo dia mais de uma vez em caso de erro de digitação.
                // Ex: 221 -> Resultado = 21. O dia 2 (segunda-feira) não pode ser somado mais de uma vez.
                diasSemana[dia - 1] = 1;
            }

            for (int i = 0; i < 7; i++)
            {
                if (diasSemana[i] == 1)
                {
                    somaBinaria += (int)Math.Pow(2, i);
                }
            }

            return somaBinaria;
        }

        private DateTime GetMinutesWithSecondsFromString(string textToFind)
        {
            textToFind = Regex.Match(textToFind, @"[0-9]{1,2}:[0-9]{1,2}").Value;

            string[] timePlaces = textToFind.Split(":");
            int minutes = 0;
            int seconds = 0;

            if (timePlaces.Count() >= 2)
            {
                minutes = GetNumberFromString(timePlaces[0]);
                seconds = GetNumberFromString(timePlaces[1]);

                minutes = minutes > 59 ? 0 : minutes;
                seconds = seconds > 59 ? 0 : seconds;
            }

            return new DateTime(1900, 01, 01, 00, minutes, seconds);
        }

        private DateTime GetHoursWithMinutesFromString(string textToFind)
        {
            textToFind = Regex.Match(textToFind, @"[0-9]{1,2}:[0-9]{1,2}").Value;

            string[] timePlaces = textToFind.Split(":");
            int hours = 0;
            int minutes = 0;

            if (timePlaces.Count() >= 2)
            {
                hours = GetNumberFromString(timePlaces[0]);
                minutes = GetNumberFromString(timePlaces[1]);

                hours = hours > 23 ? 0 : hours;
                minutes = minutes > 59 ? 0 : minutes;
            }

            return new DateTime(1900, 01, 01, hours, minutes, 0);
        }

        private DateTime GetHoursWithMinutesAndSecondsFromString(string textToFind)
        {
            textToFind = Regex.Match(textToFind, @"[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}").Value;

            string[] timePlaces = textToFind.Split(":");
            int hours = 0;
            int minutes = 0;
            int seconds = 0;

            if (timePlaces.Count() >= 2)
            {
                hours = GetNumberFromString(timePlaces[0]);
                minutes = GetNumberFromString(timePlaces[1]);
                seconds = GetNumberFromString(timePlaces[2]);

                hours = hours > 23 ? 0 : hours;
                minutes = minutes > 59 ? 0 : minutes;
                seconds = seconds > 59 ? 0 : seconds;
            }

            return new DateTime(1900, 01, 01, hours, minutes, seconds);
        }
    }
}
