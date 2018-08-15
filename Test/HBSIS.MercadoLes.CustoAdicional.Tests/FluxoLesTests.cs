using HBSIS.MercadoLes.Infra;
using HBSIS.MercadoLes.CustoAdicionalBrf.XmlBuilders;
using HBSIS.MercadoLes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HBSIS.MercadoLes.Integracao.SapBrf.Tests
{
    public class FluxoLesTests
    {
        public FluxoLesTests()
        {
            Startup.Configure();
        }
        
        [Fact]
        public void DivergenciaPernoiteSemMarcacaoFimRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 200,
                        NrLatitude = -14.000m,
                        NrLongitude = -20.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 4, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },
            };

            Assert.Equal("<1;3>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteSemMarcacaoInicioRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 200,
                        NrLatitude = -14.000m,
                        NrLongitude = -20.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 2, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 2, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 22, 0, 0),
                    NrLatitude = -14.000m,
                    NrLongitude = -20.000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },
            };

            Assert.Equal("<0;2>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteNaoDormiuNoDeposito()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 200,
                        NrLatitude = -14.000m,
                        NrLongitude = -20.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -45.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -45.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };
            
            Assert.Equal("<1;3>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteDormiuNoDeposito()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;2>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteDormiuDoisDiasNoDeposito()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;1>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteDormiuProximoDeDoisDepositos()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 1000,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;1>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteMarcacaoDepositoInicioRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.0000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;2>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteMarcacaoDepositoFimRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.0000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 21, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 3, 07, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 4, 07, 0, 0),
                    NrLatitude = -25.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;2>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteDormiuNoDepositoMasNaoApontouMarcacaoFimRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;3>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteDormiuNoDepositoMasNaoApontouMarcacaoInicioRepouso()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 6, 0, 0),
                    NrLatitude = 0,
                    NrLongitude = 0
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 10, 0, 0),
                    NrLatitude = -45.1000m,
                    NrLongitude = -35.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 2, 23, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 13, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 30,
                    DtInclusao = new DateTime(2018, 2, 3, 10, 0, 0),
                    NrLatitude = -35.1000m,
                    NrLongitude = -55.1000m
                },

            };

            Assert.Equal("<1;3>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        [Fact]
        public void DivergenciaPernoiteMarcacaoInicioFimRepousoNoDepositoMasForaDeOrdem()
        {
            List<Deposito> depositos = new List<Deposito>()
            {
                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 700,
                        NrLatitude = -19.000m,
                        NrLongitude = -29.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 500,
                        NrLatitude = -39.000m,
                        NrLongitude = -69.000m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 800,
                        NrLatitude = -23.532637m,
                        NrLongitude = -47.469106m
                    },
                },

                new Deposito()
                {
                    PontoInteresse = new PontoInteresse()
                    {
                        QtMetrosRaio = 300,
                        NrLatitude = -45.000m,
                        NrLongitude = -23.000m
                    },
                }
            };

            Rota rota1 = new Rota()
            {
                DtPartidaRealizada = new DateTime(2018, 2, 1, 6, 41, 0),
                DtChegadaRealizada = new DateTime(2018, 2, 4, 11, 41, 0),
                DtPartidaPrevista = new DateTime(2018, 2, 1, 0, 0, 0),
                DtChegadaPrevista = new DateTime(2018, 2, 2, 11, 0, 0)
            };

            List<Ocorrencia> ocorrencias = new List<Ocorrencia>()
            {
                new Ocorrencia()
                {
                    IdOcorrencia = 11,
                    DtInclusao = new DateTime(2018, 2, 1, 22, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                },

                new Ocorrencia()
                {
                    IdOcorrencia = 10,
                    DtInclusao = new DateTime(2018, 2, 2, 7, 0, 0),
                    NrLatitude = -23.538681m,
                    NrLongitude = -47.466290m
                }
            };

            Assert.Equal("<1;3>", DivergenciaPernoiteOcorrencia.Processar(rota1, ocorrencias, depositos).ToString());
        }

        //[Fact]
        //public void DivergenciaDiaria()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(235291);
        //    Assert.Equal(1, DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada);

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312627);
        //    Assert.Equal(3, DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada);

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(761935);
        //    Assert.Equal(1, DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada);

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(314427);
        //    Assert.Equal(7, DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada);

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312649);
        //    Assert.Equal(1, DivergenciaDiariaOcorrencia.Processar(rota).QuantidadeDiariaRealizada);
        //}

        //[Fact]
        //public void CustoDescargaTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(235291);
        //    Assert.Equal("<47110;38,00;98,00><390819;28,00;53,00>", CustoDescargaOcorrencia.Processar(rota)?.ToString());

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312501);
        //    Assert.Null(CustoDescargaOcorrencia.Processar(rota)?.ToString());

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312596);
        //    Assert.Equal("<970042;48,00;78,00>", CustoDescargaOcorrencia.Processar(rota)?.ToString());
        //}

        //[Fact]
        //public void DevolucaoTransportadorTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312604);
        //    Assert.Equal("<8;<7168074;04T>;<579900;04T>;<700389;04T>;<704325;04T>;<718091;04T>;<859717;04T>;<897415;04T>;<1046068;04T>>", DevolucaoTransportadorOcorrencia.Processar(rota)?.ToString());

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(235291);
        //    Assert.Null(DevolucaoTransportadorOcorrencia.Processar(rota)?.ToString());

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(761974);
        //    Assert.Equal("<1;<7404833;L03.1>>", DevolucaoTransportadorOcorrencia.Processar(rota)?.ToString());
        //}

        //[Fact]
        //public void ReentregaTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(761974);
        //    Assert.Null(ReentregaOcorrencia.Processar(rota)?.ToString());

        //    rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(235291);
        //    Assert.Equal("<2;<47110;9999999RN>;<212825;9999998RN>>", ReentregaOcorrencia.Processar(rota)?.ToString());
        //}

        //[Fact]
        //public void DiariaClienteTest()
        //{
        //    List<Rota> rotas = new List<Rota>
        //   {
        //       new Rota
        //        {
        //        CdRota = 312604,
        //        NmRota = null,
        //        DtRota = new DateTime(2016, 07, 28, 0, 0, 0),
        //        CdPlacaVeiculo = "NPH2212",
        //        CdRotaNegocio = 110738868
        //        },
        //       new Rota
        //       {
        //        CdRota = 235291,
        //        NmRota = null,
        //        DtRota = new DateTime(2018,06,01,0,0,0),
        //        CdPlacaVeiculo = "HEX5386",
        //        CdRotaNegocio = 110560678
        //       }
        //};

        //    List<UnidadeNegocio> unidadesNegocio = new List<UnidadeNegocio>
        //   {
        //    new UnidadeNegocio
        //    {
        //        CdUnidadeNegocio = "CD_CBA",
        //        QtHoraMaxPermanenciaCarreta = 4

        //    },
        //    new UnidadeNegocio
        //    {
        //        CdUnidadeNegocio = "CTA12",
        //        QtHoraMaxPermanenciaCarreta = 5

        //    }
        //};

        //    List<Entrega> entregas = new List<Entrega>
        //    {
        //        new Entrega
        //        {
        //            CdEntrega = 3992799,
        //            CdRota = 312604,
        //            CdCliente = 493842,
        //            DtPartidaRealizada = new DateTime(2018,05,23,12,0,0),
        //            DtChegadaRealizada = new DateTime(2018,05,23,10,0,0),
        //            CdUnidadeNegocio = "CD_CBA",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(0),
        //            Cliente = new Cliente
        //            {
        //             CdCliente = 493842,
        //             CdClienteNegocio = "579900"
        //            }
        //        },
        //        new Entrega
        //        {
        //            CdEntrega = 3992803,
        //            CdRota = 312604,
        //            CdCliente = 493843,
        //            DtPartidaRealizada = new DateTime(2018,05,23,10,10,0),
        //            DtChegadaRealizada = new DateTime(2018,05,23,6,0,0),
        //            CdUnidadeNegocio = "CD_CBA",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(0),
        //            Cliente = new Cliente
        //            {
        //             CdCliente = 493843,
        //             CdClienteNegocio = "700389"
        //            }
        //        },
        //        new Entrega
        //        {
        //            CdEntrega = 3992805,
        //            CdRota = 312604,
        //            CdCliente = 493844,
        //            DtPartidaRealizada = new DateTime(2018,05,3,12,0,0),
        //            DtChegadaRealizada = new DateTime(2018,05,1,12,0,0),
        //            CdUnidadeNegocio = "CD_CBA",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(0),
        //            Cliente = new Cliente
        //            {
        //             CdCliente = 493844,
        //             CdClienteNegocio = "704325"
        //            }
        //        },
        //        new Entrega
        //        {
        //            CdEntrega = 3992807,
        //            CdRota = 312604,
        //            CdCliente = 493847,
        //            DtPartidaRealizada = new DateTime(2016,07,28,9,16,12),
        //            DtChegadaRealizada = new DateTime(2016,7,28,9,6,11),
        //            CdUnidadeNegocio = "CD_CBA",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(0),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 493847,
        //             CdClienteNegocio = "718091"
        //            }
        //        },
        //        new Entrega
        //        {
        //            CdEntrega = 3992812,
        //            CdRota = 312604,
        //            CdCliente = 493850,
        //            DtPartidaRealizada = new DateTime(2016,7,28,9,32,12),
        //            DtChegadaRealizada = new DateTime(2016,7,28,9,19,12),
        //            CdUnidadeNegocio = "CD_CBA",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(0),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 493850,
        //             CdClienteNegocio = "859717"
        //            }
        //        },
        //         new Entrega
        //        {
        //            CdEntrega = 2950088,
        //            CdRota = 235291,
        //            CdCliente = 230861,
        //            DtPartidaRealizada = new DateTime(2018,5,28,15,00,0),
        //            DtChegadaRealizada = new DateTime(2018,5,23,12,0,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 230861,
        //             CdClienteNegocio = "47110"
        //            }
        //        },
        //          new Entrega
        //        {
        //            CdEntrega = 2950089,
        //            CdRota = 235291,
        //            CdCliente = 230869,
        //            DtPartidaRealizada = new DateTime(2018,5,24,1,25,0),
        //            DtChegadaRealizada = new DateTime(2018,5,24,1,15,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 230869,
        //             CdClienteNegocio = "185709"
        //            }
        //        },
        //             new Entrega
        //        {
        //            CdEntrega = 2950090,
        //            CdRota = 235291,
        //            CdCliente = 230871,
        //            DtPartidaRealizada = new DateTime(2018,6,1,22,0,0),
        //            DtChegadaRealizada = new DateTime(2018,5,1,18,0,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 230871,
        //             CdClienteNegocio = "201192"
        //            }
        //        },
        //         new Entrega
        //        {
        //            CdEntrega = 2950091,
        //            CdRota = 235291,
        //            CdCliente = 470899,
        //            DtPartidaRealizada = new DateTime(2018,5,28,14,0,0),
        //            DtChegadaRealizada = new DateTime(2018,5,12,12,0,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 470899,
        //             CdClienteNegocio = "212825"
        //            }
        //        },
        //               new Entrega
        //        {
        //            CdEntrega = 2950092,
        //            CdRota = 235291,
        //            CdCliente = 470665,
        //            DtPartidaRealizada = new DateTime(2018,5,13,4,59,0),
        //            DtChegadaRealizada = new DateTime(2018,5,13,01,0,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 470665,
        //             CdClienteNegocio = "319742"
        //            }
        //        },
        //              new Entrega
        //        {
        //            CdEntrega = 2950093,
        //            CdRota = 235291,
        //            CdCliente = 236264,
        //            DtPartidaRealizada = new DateTime(2018,5,31,22,0,0),
        //            DtChegadaRealizada = new DateTime(2018,5,31,18,0,0),
        //            CdUnidadeNegocio = "CTA12",
        //            UnidadeNegocio = unidadesNegocio.ElementAt(1),
        //             Cliente = new Cliente
        //            {
        //             CdCliente = 236264,
        //             CdClienteNegocio = "390819"
        //            }
        //        },

        //    };

        //    List<Veiculo> veiculos = new List<Veiculo>
        //   {
        //       new Veiculo
        //    {
        //        CdPlacaVeiculo = "NPH2212",
        //        CdUnidadeNegocio = "CD_CBA",
        //        CdTipoVeiculo = 4
        //    },
        //         new Veiculo
        //    {
        //        CdPlacaVeiculo = "HEX5386",
        //        CdUnidadeNegocio = "CLIEN1626",
        //        CdTipoVeiculo = 1
        //    }
        //};

        //    List<TipoVeiculo> tiposVeiculoRota = new List<TipoVeiculo>
        //    {
        //        new TipoVeiculo
        //    {
        //        CdTipoVeiculo = 4,
        //        DsTipo = "CTA"
        //    },
        //             new TipoVeiculo
        //    {
        //        CdTipoVeiculo = 1,
        //        DsTipo = "CTA"
        //    }
        //};
        //    rotas.ElementAt(0).Entregas = entregas.Where(o => o.CdRota == 312604).ToList();
        //    rotas.ElementAt(1).Entregas = entregas.Where(o => o.CdRota == 235291).ToList();

        //    Assert.Equal("<<700389,1>;<704325,3>>", DiariaClienteOcorrencia.Processar(rotas.ElementAt(0), tiposVeiculoRota.ElementAt(0))?.ToString());
        //    Assert.Equal("<<47110,6>;<201192,32>;<212825,17>>", DiariaClienteOcorrencia.Processar(rotas.ElementAt(1), tiposVeiculoRota.ElementAt(1))?.ToString());

        //}

        //[Fact]
        //public void AdicionalBalsaTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var ocorrencias = persistenceDataContext.OcorrenciaRepository.GetOcorrenciasCompletas(761979);
        //    Assert.Null(AdicionalBalsaOcorrencia.Processar(ocorrencias)?.ToString());

        //    ocorrencias = persistenceDataContext.OcorrenciaRepository.GetOcorrenciasCompletas(761974);
        //    Assert.Equal("<2;<Ponto Interesse Balsa>;<Teste Porto de Santos>>", AdicionalBalsaOcorrencia.Processar(ocorrencias)?.ToString());
        //}

        //[Fact]
        //public void DivergenciaKmTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var rota = persistenceDataContext.RotaRepository.GetRotaIndicadoresFluxoLES(312617);
        //    var ocorrencias = persistenceDataContext.OcorrenciaRepository.GetOcorrenciasCompletas(312617);
        //    var deslocamentosAlmoco = persistenceDataContext.DeslocamentoAlmocoRotaRepository.GetDeslocamentosPIM(312617);
        //    var deslocamentosAbastecimento = persistenceDataContext.DeslocamentoAbastecimentoRotaRepository.GetDeslocamentosPIM(312617);
        //    var deslocamentosPernoite = persistenceDataContext.DeslocamentoPernoiteRotaRepository.GetDeslocamentosPIM(312617);

        //    Assert.Equal("<0>", DivergenciaKmOcorrencia.Processar(rota, ocorrencias, deslocamentosAlmoco, deslocamentosAbastecimento, deslocamentosPernoite, null, 80.00m)?.ToString());
        //    Assert.Equal("<1>", DivergenciaKmOcorrencia.Processar(rota, ocorrencias, deslocamentosAlmoco, deslocamentosAbastecimento, deslocamentosPernoite, null, 25.00m)?.ToString());
        //}

        //[Fact]
        //public void AderenciaRaioKPITest()
        //{

        //}

        //[Fact]
        //public void InicioOuFimNoRaioTest()
        //{

        //}

        //[Fact]
        //public void MotoristaOuSistemaFinalizouRotaTest()
        //{
        //    Rota rota = new Rota();

        //    #region Casos de Testes
        //    rota.NmUsuarioFechamento = "PEV0120";
        //    Assert.True(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "OYW4619";
        //    Assert.True(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "SISTEMA";
        //    Assert.True(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "SISTEMA - 72HRS";
        //    Assert.True(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "528356";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "admin";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "HSU37020";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "AHSU3702";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "0HSU3702";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "HSU3702 AAA0000";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "HSU3702AAA0000";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "desconhecido";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "OYW461A";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "1YW4610";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "O2W4616";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "OYW46BA";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "AAAZZZZ";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));

        //    rota.NmUsuarioFechamento = "0009999";
        //    Assert.False(DivergenciaKmOcorrencia.MotoristaOuSistemaFinalizouRota(rota));
        //    #endregion
        //}

        //[Fact]
        //public void MultiTransporteTest()
        //{
        //    PersistenceDataContext persistenceDataContext = new PersistenceDataContext();

        //    var baldeiosEntregaRota = persistenceDataContext.BaldeioEntregaRepository.GetBaldeiosMultiTransporteByRotaDestino(761976);
        //    Assert.Equal("<<112658148>>", MultiTransporteNode.Processar(baldeiosEntregaRota)?.ToString());

        //    baldeiosEntregaRota = persistenceDataContext.BaldeioEntregaRepository.GetBaldeiosMultiTransporteByRotaDestino(235291);
        //    Assert.Null(MultiTransporteNode.Processar(baldeiosEntregaRota)?.ToString());

        //    baldeiosEntregaRota = persistenceDataContext.BaldeioEntregaRepository.GetBaldeiosMultiTransporteByRotaDestino(312501);
        //    Assert.Equal("<<110737992>;<110737993>>", MultiTransporteNode.Processar(baldeiosEntregaRota)?.ToString());
        //}

        //[Fact]
        //public void AdicionalMeiaPernoiteTest()
        //{
        //    Rota rota = new Rota()
        //    {
        //        DtPartidaRealizada = new DateTime(2018, 01, 10, 8, 00, 00),
        //        DtChegadaRealizada = new DateTime(2018, 01, 15, 12, 00, 00),
        //        UnidadeNegocio = new UnidadeNegocio()
        //        {
        //            // 12:00
        //            DtHoraLimRestanteRetorno = new DateTime(01, 01, 01, 12, 00, 00),

        //            // 2.5 KM
        //            QtQuilometroRestante = 2.5m
        //        }
        //    };
        //    List<Infra.Ocorrencia> ocorrencias = new List<Infra.Ocorrencia>()
        //    {
        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 00, 00),
        //            NrLatitude = -23.532341m,
        //            NrLongitude = -47.465444m,
        //            // Saída Revenda
        //            IdOcorrencia = 1
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 11, 11, 00, 00),
        //            NrLatitude = -23.497803m,
        //            NrLongitude = -47.468922m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 12, 12, 00, 00),
        //            NrLatitude = -23.498786m,
        //            NrLongitude = -47.468782m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 13, 10, 00, 00),
        //            NrLatitude = -23.500931m,
        //            NrLongitude = -23.500931m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 14, 10, 00, 00),
        //            NrLatitude = -23.503563m,
        //            NrLongitude = -47.466899m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 15, 05, 00, 00),
        //            NrLatitude = -23.532341m,
        //            NrLongitude = -47.465444m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 15, 11, 55, 00),
        //            NrLatitude = -23.505000m,
        //            NrLongitude = -47.463337m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 15, 11, 57, 00),
        //            NrLatitude = -23.505196m,
        //            NrLongitude = -47.463228m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 15, 12, 10, 00),
        //            NrLatitude = -23.528422m,
        //            NrLongitude = -47.465458m,
        //            // Posição
        //            IdOcorrencia = 30
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 15, 11, 59, 00),
        //            NrLatitude = -23.532341m,
        //            NrLongitude = -47.465444m,
        //            // Chegada Revenda
        //            IdOcorrencia = 7
        //        },
        //    };
        //    List<Infra.Deposito> depositos = new List<Infra.Deposito>()
        //    {
        //        new Deposito()
        //        {
        //            NmDeposito = "Deposito Ed. Av. Paulista",
        //            IdAtivo = true,
        //            PontoInteresse = new PontoInteresse()
        //            {
        //                NmPonto = "CD Ed. Av. Paulista",
        //                NrLatitude = -23.532341m,
        //                NrLongitude = -47.465444m
        //            }
        //        }
        //    };

        //    // A última ocorrência antes do horário é longe do Depósito
        //    Assert.Equal("<1>", AdicionalMeiaPernoiteOcorrencia.Processar(ocorrencias, depositos, rota)?.ToString());

        //    depositos = new List<Infra.Deposito>()
        //    {
        //        new Deposito()
        //        {
        //            NmDeposito = "Depósito Ed. Av. Paulista",
        //            IdAtivo = true,
        //            PontoInteresse = new PontoInteresse()
        //            {
        //                NmPonto = "CD Ed. Av. Paulista",
        //                NrLatitude = -23.532341m,
        //                NrLongitude = -47.465444m
        //            }
        //        },

        //        new Deposito()
        //        {
        //            NmDeposito = "Depósito 2",
        //            IdAtivo = true,
        //            PontoInteresse = new PontoInteresse()
        //            {
        //                NmPonto = "CD Depósito 2",
        //                NrLatitude = -23.505000m,
        //                NrLongitude = -47.463337m,
        //            }
        //        },
        //    };

        //    // A última ocorrência antes do horário é muito próxima a um dos Depósitos
        //    Assert.Equal("<0>", AdicionalMeiaPernoiteOcorrencia.Processar(ocorrencias, depositos, rota)?.ToString());

        //    rota = new Rota()
        //    {
        //        DtPartidaRealizada = new DateTime(2018, 01, 10, 7, 00, 00),
        //        DtChegadaRealizada = new DateTime(2018, 01, 10, 8, 30, 00),
        //        UnidadeNegocio = new UnidadeNegocio()
        //        {
        //            // 12:00
        //            DtHoraLimRestanteRetorno = new DateTime(01, 01, 01, 12, 00, 00),

        //            // 2.5 KM
        //            QtQuilometroRestante = 2.5m
        //        }
        //    };
        //    depositos = new List<Infra.Deposito>()
        //    {
        //        new Deposito()
        //        {
        //            NmDeposito = "Deposito Ed. Av. Paulista",
        //            IdAtivo = true,
        //            PontoInteresse = new PontoInteresse()
        //            {
        //                NmPonto = "CD Ed. Av. Paulista",
        //                NrLatitude = -23.532341m,
        //                NrLongitude = -47.465444m
        //            }
        //        }
        //    };

        //    // Não atende a condição da Quantidade de Diárias 
        //    Assert.Equal("<0>", AdicionalMeiaPernoiteOcorrencia.Processar(ocorrencias, depositos, rota)?.ToString());
        //}

        //[Fact]
        //public void DeslocamentoRepositoryTest()
        //{
        //    var rota = new Rota()
        //    {
        //        UnidadeNegocio = new UnidadeNegocio()
        //        {
        //            VlMaxDistanciaKmDesvioPIM = 1.5,
        //            VlMaxPorcentagemDesvioPIM = 25
        //        }
        //    };

        //    List<Deslocamento> deslocamentosAlmoco = new List<Deslocamento>()
        //    {
        //        new DeslocamentoAlmoco()
        //        {
        //            CdOcorrenciaInicioPIM = 1,
        //            CdOcorrenciaFimPIM = 5,
        //            LocalizacaoEntregaAntesPIM = (0,0),
        //            LocalizacaoEntregaDepoisPIM = (0,0),
        //            ValorOdometroEntregaAntesPIM = 0,
        //            ValorOdometroInicioPIM = 2000,
        //            ValorOdometroFimPIM = 2000,
        //            ValorOdometroEntregaDepoisPIM = 3000
        //        }
        //    };

        //    //var deslocamentosAlmoco = persistenceDataContext.DeslocamentoAlmocoRotaRepository.GetDeslocamentosPIM(312519);
        //    //var deslocamentosAbastecimento = persistenceDataContext.DeslocamentoAbastecimentoRotaRepository.GetDeslocamentosPIM(312519);
        //    //var deslocamentosPernoite = persistenceDataContext.DeslocamentoPernoiteRotaRepository.GetDeslocamentosPIM(312519);

        //    //DivergenciaKmOcorrencia.DeslocamentosAlmocoPernoiteAbastecimento(rota, deslocamentosAlmoco, deslocamentosAbastecimento, deslocamentosPernoite);

        //    Assert.True(true);
        //}

        //[Fact]
        //public void AvaliarSombraOuDesligamentoCelular()
        //{
        //    List<Infra.Ocorrencia> ocorrenciasSombraGPS = new List<Infra.Ocorrencia>()
        //    {
        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 10, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 20, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 25, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 35, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 36, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 37, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 45, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 9, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 9, 29, 00),
        //        },
        //    };
        //    DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasSombraGPS.ToList());
        //    Assert.True(DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasSombraGPS.ToList()));

        //    List<Infra.Ocorrencia> ocorrenciasCelularDesligado = new List<Infra.Ocorrencia>()
        //    {
        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 10, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 20, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 25, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 35, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 36, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 37, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 45, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 9, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 9, 31, 00),
        //        },
        //    };
        //    DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasCelularDesligado.ToList());
        //    Assert.False(DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasCelularDesligado.ToList()));

        //    List<Infra.Ocorrencia> ocorrenciasParadasPermitidas = new List<Infra.Ocorrencia>()
        //    {
        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 10, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 20, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 25, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 35, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 36, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 11, 37, 00),
        //            // Repouso
        //            Parada = new Parada()
        //            {
        //                IdTipoParada = 7
        //            }
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 11, 45, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 13, 00, 00),
        //            // Abastecimento
        //            Parada = new Parada()
        //            {
        //                IdTipoParada = 8
        //            }
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 13, 29, 00),
        //        },
        //    };
        //    DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasParadasPermitidas.ToList());
        //    Assert.True(DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasParadasPermitidas.ToList()));

        //    List<Infra.Ocorrencia> ocorrenciasParadasNaoPermitidas = new List<Infra.Ocorrencia>()
        //    {
        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 00, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 10, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 20, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 25, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 35, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 8, 36, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 11, 37, 00),
        //            // Repouso
        //            Parada = new Parada()
        //            {
        //                IdTipoParada = 7
        //            }
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 11, 45, 00),
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 13, 00, 00),
        //            // PNP
        //            Parada = new Parada()
        //            {
        //                IdTipoParada = 4
        //            }
        //        },

        //        new Infra.Ocorrencia()
        //        {
        //            DtInclusao = new DateTime(2018, 01, 10, 13, 29, 00),
        //        },
        //    };
        //    DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasParadasNaoPermitidas.ToList());
        //    Assert.False(DivergenciaKmOcorrencia.AvaliarSombraCelularOuCelularDesligado(ocorrenciasParadasNaoPermitidas.ToList()));
        //}

        //[Fact]
        //public void ParadasTratadasAnalitico()
        //{
        //    PersistenceDataContext persistenceData = new PersistenceDataContext();

        //    var paradas = persistenceData.ParadasTratadasAnaliticoRepository.Get(1195240);
        //    DivergenciaKmOcorrencia.AvaliarPNPRota(paradas);

        //    Assert.True(true);
        //}
    }
}
