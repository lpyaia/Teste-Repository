using HBSIS.Core.HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.Core.HBSIS.GE.FileImporter.Infra.ExcelModels;
using HBSIS.GE.FileImporter.Infra.Entities;
using HBSIS.GE.FileImporter.Services.Persistence;
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

        private void UpdateClienteCelular(long cdCliente, string nome, string telefone, string enviarSMS)
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

        private void CreateCliente(ClienteSpreadsheetLine clienteExcel)
        {
            Cliente cliente = new Cliente();

            // Campos fixos
            cliente.NmCliente = clienteExcel.Cliente;
            cliente.DsEndereco = clienteExcel.Rua;
            cliente.NmBairro = clienteExcel.Bairro;
            cliente.NmCidade = clienteExcel.Cidade;
            cliente.NmEstado = clienteExcel.Estado;

            // Campos que podem ser alterados
            cliente.DsObservacao = clienteExcel.Tipo;
            cliente.IdPotencialCliente = GetNumberFromString(clienteExcel.PotencialCVA);
            cliente.TempoAtendimentoEntrega = GetHoursWithMinutesFromString(clienteExcel.TempoAtendimento);
            cliente.TempoTratativaDevEntrega = GetHoursWithMinutesFromString(clienteExcel.TempoTratativa);
            cliente.IdDiasRestricao = GetNumberFromString(clienteExcel.RestricaoDias);
            cliente.DtInicioExpediente = GetHoursWithMinutesFromString(clienteExcel.PrimeiraAbertura);
            cliente.DtFimExpediente = GetHoursWithMinutesFromString(clienteExcel.PrimeiroFechamento);
            cliente.DtInicioExpedienteAlternativo = GetHoursWithMinutesFromString(clienteExcel.SegundaAbertura);
            cliente.DtFimExpedienteAlternativo = GetHoursWithMinutesFromString(clienteExcel.SegundoFechamento);
        }
        
        private void UpdateCliente(Cliente cliente, ClienteSpreadsheetLine clienteExcel)
        {
            cliente.DsObservacao = clienteExcel.Tipo;
            cliente.IdPotencialCliente = GetNumberFromString(clienteExcel.PotencialCVA);
            cliente.TempoAtendimentoEntrega = GetHoursWithMinutesFromString(clienteExcel.TempoAtendimento);
            cliente.TempoTratativaDevEntrega = GetHoursWithMinutesFromString(clienteExcel.TempoTratativa);
            cliente.IdDiasRestricao = GetNumberFromString(clienteExcel.RestricaoDias);
            cliente.DtInicioExpediente = GetHoursWithMinutesFromString(clienteExcel.PrimeiraAbertura);
            cliente.DtFimExpediente = GetHoursWithMinutesFromString(clienteExcel.PrimeiroFechamento);
            cliente.DtInicioExpedienteAlternativo = GetHoursWithMinutesFromString(clienteExcel.SegundaAbertura);
            cliente.DtFimExpedienteAlternativo = GetHoursWithMinutesFromString(clienteExcel.SegundoFechamento);

            _persistenceDataContext.ClienteRepository.UpdateImportacao(cliente);
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

        private DateTime GetHoursWithMinutesFromString(string textToFind)
        {
            textToFind = Regex.Match(textToFind, @"[0-9]{1,2}:[0-9]{1,2}").Value;

            string[] timePlaces = textToFind.Split(":");
            int hour = 0;
            int minute = 0;

            if(timePlaces.Count() >= 2)
            {
                hour = GetNumberFromString(timePlaces[0]);
                minute = GetNumberFromString(timePlaces[1]);
            }

            return new DateTime(1900, 01, 01, hour, minute, 0);
        }

        
    }
}
