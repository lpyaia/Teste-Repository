using System;
using System.Collections.Generic;
using System.Text;

namespace HBSIS.MercadoLes.Infra
{
    public class Pernoite
    {
        private Ocorrencia _ocorrenciaInicioRepouso;
        private Ocorrencia _ocorrenciaFimRepouso;

        public Pernoite(Ocorrencia ocorrenciaInicioRepouso, Ocorrencia ocorrenciaFimRepouso)
        {
            _ocorrenciaInicioRepouso = ocorrenciaInicioRepouso;
            _ocorrenciaFimRepouso = ocorrenciaFimRepouso;
        }

        public (decimal lat, decimal lon) GetCoordenadasInicioRepouso()
        {
            return (_ocorrenciaInicioRepouso.NrLatitude, _ocorrenciaInicioRepouso.NrLongitude);
        }

        public (decimal lat, decimal lon) GetCoordenadasFimRepouso()
        {
            return (_ocorrenciaFimRepouso.NrLatitude, _ocorrenciaFimRepouso.NrLongitude);
        }
    }
}
