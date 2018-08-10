using HBSIS.Framework.Data.Dapper;
using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Persistence
{
    public class PersistenceDataContext : DapperDataContext
    {
        private RotaRepository _rotaRepository;
        private EntregaRepository _entregaRepository;
        private ClienteRepository _clienteRepository;
        private MotivoDevolucaoRepository _motivoDevolucaoRepository;
        private SolicitacaoDescargaRepository _solicitacaoDescargaRepository;
        private TransportadoraRepository _transportadoraRepository;
        private OcorrenciaRepository _ocorrenciaRepository;
        private MetasPainelIndicadoresRepository _metasPainelIndicadoresRepository;
        private BaldeioEntregaRepository _baldeioEntregaRepository;
        private UnidadeNegocioRepository _unidadeNegocioRepository;
        private DepositoRepository _depositoRepository;
        private VeiculoRepository _veiculoRepository;
        private TipoVeiculoRepository _tipoVeiculoRepository;
        private ConfiguracaoRepository _configuracaoRepository;
        private DeslocamentoRotaRepository<DeslocamentoAlmoco> _deslocamentoAlmocoRotaRepository;
        private DeslocamentoRotaRepository<DeslocamentoAbastecimento> _deslocamentoAbastecimentoRotaRepository;
        private DeslocamentoRotaRepository<DeslocamentoPernoite> _deslocamentoPernoiteRotaRepository;
        private ParadasTratadasAnaliticoRepository _paradasTratadasAnaliticoRepository;

        public RotaRepository RotaRepository
        {
            get
            {
                if(_rotaRepository == null)
                    _rotaRepository = new RotaRepository(ConnectionString);

                return _rotaRepository;
            }
        }

        public EntregaRepository EntregaRepository
        {
            get
            {
                if (_entregaRepository == null)
                    _entregaRepository = new EntregaRepository(ConnectionString);

                return _entregaRepository;
            }
        }

        public ClienteRepository ClienteRepository
        {
            get
            {
                if (_clienteRepository == null)
                    _clienteRepository = new ClienteRepository(ConnectionString);

                return _clienteRepository;
            }
        }

        public MotivoDevolucaoRepository MotivoDevolucaoRepository
        {
            get
            {
                if (_motivoDevolucaoRepository == null)
                    _motivoDevolucaoRepository = new MotivoDevolucaoRepository(ConnectionString);

                return _motivoDevolucaoRepository;
            }
        }
      
        public VeiculoRepository VeiculoRepository
        {
            get
            {
                if (_veiculoRepository == null)
                    _veiculoRepository = new VeiculoRepository(ConnectionString);

                return _veiculoRepository;
            }
        }

        public TipoVeiculoRepository TipoVeiculoRepository
        {
            get
            {
                if (_tipoVeiculoRepository == null)
                    _tipoVeiculoRepository = new TipoVeiculoRepository(ConnectionString);

                return _tipoVeiculoRepository;
            }
        }

        public SolicitacaoDescargaRepository SolicitacaoDescargaRepository
        {
            get
            {
                if (_solicitacaoDescargaRepository == null)
                    _solicitacaoDescargaRepository = new SolicitacaoDescargaRepository(ConnectionString);

                return _solicitacaoDescargaRepository;
            }
        }

        public TransportadoraRepository TransportadoraRepository
        {
            get
            {
                if (_transportadoraRepository == null)
                    _transportadoraRepository = new TransportadoraRepository(ConnectionString);

                return _transportadoraRepository;
            }
        }

        public OcorrenciaRepository OcorrenciaRepository
        {
            get
            {
                if (_ocorrenciaRepository == null)
                    _ocorrenciaRepository = new OcorrenciaRepository(ConnectionString);

                return _ocorrenciaRepository;
            }
        }

        public MetasPainelIndicadoresRepository MetasPainelIndicadoresRepository
        {
            get
            {
                if (_metasPainelIndicadoresRepository == null)
                    _metasPainelIndicadoresRepository = new MetasPainelIndicadoresRepository(ConnectionString);

                return _metasPainelIndicadoresRepository;
            }
        }

        public BaldeioEntregaRepository BaldeioEntregaRepository
        {
            get
            {
                if (_baldeioEntregaRepository == null)
                    _baldeioEntregaRepository = new BaldeioEntregaRepository(ConnectionString);

                return _baldeioEntregaRepository;
            }
        }

        public UnidadeNegocioRepository UnidadeNegocioRepository
        {
            get
            {
                if (_unidadeNegocioRepository == null)
                    _unidadeNegocioRepository = new UnidadeNegocioRepository(ConnectionString);

                return _unidadeNegocioRepository;
            }
        }

        public DepositoRepository DepositoRepository
        {
            get
            {
                if (_depositoRepository == null)
                    _depositoRepository = new DepositoRepository(ConnectionString);

                return _depositoRepository;
            }
        }

        public ConfiguracaoRepository ConfiguracaoRepository
        {
            get
            {
                if (_configuracaoRepository == null)
                    _configuracaoRepository = new ConfiguracaoRepository(ConnectionString);

                return _configuracaoRepository;
            }
        }

        public DeslocamentoRotaRepository<DeslocamentoAlmoco> DeslocamentoAlmocoRotaRepository
        {
            get
            {
                if (_deslocamentoAlmocoRotaRepository == null)
                    _deslocamentoAlmocoRotaRepository = new DeslocamentoRotaRepository<DeslocamentoAlmoco>(ConnectionString);

                return _deslocamentoAlmocoRotaRepository;
            }
        }

        public DeslocamentoRotaRepository<DeslocamentoAbastecimento> DeslocamentoAbastecimentoRotaRepository
        {
            get
            {
                if (_deslocamentoAbastecimentoRotaRepository == null)
                    _deslocamentoAbastecimentoRotaRepository = new DeslocamentoRotaRepository<DeslocamentoAbastecimento>(ConnectionString);

                return _deslocamentoAbastecimentoRotaRepository;
            }
        }

        public DeslocamentoRotaRepository<DeslocamentoPernoite> DeslocamentoPernoiteRotaRepository
        {
            get
            {
                if (_deslocamentoPernoiteRotaRepository == null)
                    _deslocamentoPernoiteRotaRepository = new DeslocamentoRotaRepository<DeslocamentoPernoite>(ConnectionString);

                return _deslocamentoPernoiteRotaRepository;
            }
        }

        public ParadasTratadasAnaliticoRepository ParadasTratadasAnaliticoRepository
        {
            get
            {
                if (_paradasTratadasAnaliticoRepository == null)
                    _paradasTratadasAnaliticoRepository = new ParadasTratadasAnaliticoRepository(ConnectionString);

                return _paradasTratadasAnaliticoRepository;
            }
        }
    }
}
