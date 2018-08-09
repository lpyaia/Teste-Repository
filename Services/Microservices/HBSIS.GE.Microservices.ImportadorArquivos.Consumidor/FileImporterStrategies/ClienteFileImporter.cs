using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.GE.FileImporter.Services.Persistence;
using HBSIS.GE.Microservices.FileImporter.Consumer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HBSIS.GE.Microservices.FileImporter.Consumer.FileImporterStrategies
{
    public class ClienteFileImporter : FileImporterStrategy<ClienteSpreadsheetLine>, IFileImporterStrategy
    {
        private PersistenceDataContext _persistenceDataContext;

        public ClienteFileImporter()
        {
            _persistenceDataContext = new PersistenceDataContext();
        }

        public override void ImportData(ClienteSpreadsheetLine data)
        {
            try
            {
                Cliente cliente = _persistenceDataContext.ClienteRepository.GetByCodigoClienteNegocio(data.Codigo);

                if (cliente == null)
                {
                    // Funcionalidade desabilitada no momento.
                    // Aguardando requisitos.
                    //CreateCliente(data);
                    return;
                }

                else
                {
                    UpdateCliente(cliente, data);
                }

                UpdateClienteCelular(cliente.CdCliente, data.Contato1, data.TelefoneContato1, data.EnviarSmsContato1);
                UpdateClienteCelular(cliente.CdCliente, data.Contato2, data.TelefoneContato2, data.EnviarSmsContato2);
                UpdateClienteCelular(cliente.CdCliente, data.Contato3, data.TelefoneContato3, data.EnviarSmsContato3);
                UpdateClienteCelular(cliente.CdCliente, data.Contato4, data.TelefoneContato4, data.EnviarSmsContato4);
                UpdateClienteCelular(cliente.CdCliente, data.Contato5, data.TelefoneContato5, data.EnviarSmsContato5);
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateClienteCelular(long cdCliente, string nome, string telefone, string enviarSMS)
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

                        _persistenceDataContext.ClienteCelularRepository.InsertImportacao(clienteCelular);
                    }

                    else
                    {
                        _persistenceDataContext.ClienteCelularRepository.AtualizarNomeContato(clienteCelular.CdCelular, nome);
                    }
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void CreateCliente(ClienteSpreadsheetLine clienteExcel)
        {
            Cliente cliente = new Cliente();

            // Campos fixos
            cliente.NmCliente = clienteExcel.Cliente;
            cliente.DsEndereco = clienteExcel.Rua;
            cliente.NmBairro = clienteExcel.Bairro;
            cliente.NmCidade = clienteExcel.Cidade;
            cliente.NmEstado = clienteExcel.Estado;
        }
        
        private void UpdateCliente(Cliente cliente, ClienteSpreadsheetLine clienteExcel)
        {
            try
            {
                cliente.DsObservacao = clienteExcel.Tipo.HasValue() ?
                    clienteExcel.Tipo :
                    cliente.DsObservacao;

                cliente.IdPotencialCliente = clienteExcel.PotencialCVA.HasValue() ?
                    GetNumberFromString(clienteExcel.PotencialCVA) :
                    cliente.IdPotencialCliente;

                cliente.TempoAtendimentoEntrega = clienteExcel.TempoAtendimento.HasValue() ?
                    GetMinutesWithSecondsFromString(clienteExcel.TempoAtendimento) :
                    cliente.TempoAtendimentoEntrega;

                cliente.TempoTratativaDevEntrega = clienteExcel.TempoTratativa.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.TempoTratativa) :
                    cliente.TempoTratativaDevEntrega;

                cliente.IdDiasRestricao = clienteExcel.RestricaoDias.HasValue() ?
                    GetNumberFromString(clienteExcel.RestricaoDias) :
                    cliente.IdDiasRestricao;

                cliente.DtInicioExpediente = clienteExcel.PrimeiraAbertura.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.PrimeiraAbertura) :
                    cliente.DtInicioExpediente;

                cliente.DtFimExpediente = clienteExcel.PrimeiroFechamento.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.PrimeiroFechamento) :
                    cliente.DtFimExpediente;

                cliente.DtInicioExpedienteAlternativo = clienteExcel.SegundaAbertura.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.SegundaAbertura) :
                    cliente.DtInicioExpedienteAlternativo;

                cliente.DtFimExpedienteAlternativo = clienteExcel.SegundoFechamento.HasValue() ?
                    GetHoursWithMinutesFromString(clienteExcel.SegundoFechamento) :
                    cliente.DtFimExpedienteAlternativo;

                _persistenceDataContext.ClienteRepository.UpdateImportacao(cliente);
            }

            catch(Exception ex)
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

            if(timePlaces.Count() >= 2)
            {
                hours = GetNumberFromString(timePlaces[0]);
                minutes = GetNumberFromString(timePlaces[1]);

                hours = hours > 23 ? 0 : hours;
                minutes = minutes > 59 ? 0 : minutes;
            }

            return new DateTime(1900, 01, 01, hours, minutes, 0);
        }

        
    }
}
