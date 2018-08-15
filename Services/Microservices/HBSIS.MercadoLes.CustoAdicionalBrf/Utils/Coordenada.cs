using HBSIS.MercadoLes.Commons.Integration;
using HBSIS.MercadoLes.Commons.Helpers;
using System;
using System.Linq;
using HBSIS.MercadoLes.Persistence;
using HBSIS.MercadoLes.Infra;

namespace HBSIS.MercadoLes.CustoAdicionalBrf.Utils
{
    public static class Coordenada
    {
        /// <summary>
        ///     Calcular a distância em linha reta entre duas coordenadas em graus decimais (DD), em metros
        /// </summary>
        /// <param name="lat1">Latitude 1</param>
        /// <param name="lon1">Longitude 1</param>
        /// <param name="lat2">Latitude 2</param>
        /// <param name="lon2">Longitude 2</param>
        /// <returns>Distância em metros</returns>
        public static (double distancia, int tempo) DistanciaLinhaReta(double lat1, double lon1, double lat2, double lon2)
        {
            const int R = 6378140; // Raio da terra
            const double radius = Math.PI / 180;
            var dLat = radius * (lat2 - lat1); // Converte para radianos
            var dLon = radius * (lon2 - lon1); // Converte para radianos
            var nlat1 = radius * lat1;
            var nlat2 = radius * lat2;

            var a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(nlat1) * Math.Cos(nlat2) * Math.Pow(Math.Sin(dLon / 2), 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            int d = (int)(R * c);

            return (d, 0);
        }

        /// <summary>
        ///     Calcular a distância em linha reta entre duas coordenadas em graus decimais (DD), em metros
        /// </summary>
        /// <param name="lat1">Latitude 1</param>
        /// <param name="lon1">Longitude 1</param>
        /// <param name="lat2">Latitude 2</param>
        /// <param name="lon2">Longitude 2</param>
        /// <returns>Distância em metros</returns>
        public static (double distancia, int tempo) DistanciaLinhaReta(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            double doubleLat1 = Convert.ToDouble(lat1);
            double doubleLon1 = Convert.ToDouble(lon1);
            double doubleLat2 =  Convert.ToDouble(lat2);
            double doubleLon2 = Convert.ToDouble(lon2);

            return DistanciaLinhaReta(doubleLat1, doubleLon1, doubleLat2, doubleLon2);
        }

        public static (double distancia, int tempo) Distancia(double lat1, double lon1, double lat2, double lon2)
        {
            PersistenceDataContext persistenceDataContext = new PersistenceDataContext();
            Configuracao config = new Configuracao();

            config = persistenceDataContext.ConfiguracaoRepository.GetAll().FirstOrDefault();
            var googleDistance = new GoogleMapsDistance(new DefaultWebRequester(), new GoogleMapsRequestSigner(config.DsClientIdApiMapa, config.DsChavePrivadaApiMapa));

            if (config != null && !string.IsNullOrEmpty(config.DsClientIdApiMapa) && !string.IsNullOrEmpty(config.DsChavePrivadaApiMapa))
            {
                return googleDistance.DistanciaGoogleMaps(lat1, lon1, lat2, lon2);
            }

            return DistanciaLinhaReta(lat1, lon1, lat2, lon2);
        }
    }
}


