using HBSIS.GE.FileImporter.Services.Commons.Integration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HBSIS.GE.FileImporter.Services.Commons.Helpers
{
    /// <summary>
    /// Classe utilizada para calcular a distância entre duas coordenadas.
    /// </summary>
    public class GoogleMapsDistance
    {
        private const string URI_GOOGLE_DISTANCE_MATRIX = "http://maps.googleapis.com/maps/api/distancematrix/json";

        private readonly IWebRequester webRequester;
        private readonly IGoogleMapsRequestSigner requestSigner;

        public GoogleMapsDistance(IWebRequester webRequester, IGoogleMapsRequestSigner requestSigner)
        {
            this.webRequester = webRequester;
            this.requestSigner = requestSigner;
        }

        /// <summary>
        ///     Buscar informações de Distância e Tempo previstos no Google Maps
        /// </summary>
        /// <param name="lat1">Latitude de Origem</param>
        /// <param name="lon1">Longitude de Origem</param>
        /// <param name="lat2">Latitude Destino</param>
        /// <param name="lon2">Longitude Destino</param>
        /// <returns></returns>
        public (double distancia, int tempo) DistanciaGoogleMaps(double lat1, double lon1, double lat2, double lon2)
        {
            var enUS = new CultureInfo("en-us");
            var origem = string.Format("{0},{1}", lat1.ToString("##0.000000", enUS), lon1.ToString("##0.000000", enUS));
            var destino = string.Format("{0},{1}", lat2.ToString("##0.000000", enUS), lon2.ToString("##0.000000", enUS));
            
            return DistanciaGoogleMaps(origem, destino);
        }

        /// <summary>
        ///     Buscar informações de Distância e Tempo previstos no Google Maps
        /// </summary>
        /// <param name="origem">Coordenadas de origem, formato: (##0.000000,##0.000000)</param>
        /// <param name="destino">Coordenadas de destino, formato: (##0.000000,##0.000000)</param>
        /// <returns></returns>
        private (double distancia, int tempo) DistanciaGoogleMaps(string origem, string destino)
        {
            var uri =
                string.Format("{0}?origins={1}&destinations={2}&mode=driving&language=pt-BR&sensor=false&units=metric"
                    , URI_GOOGLE_DISTANCE_MATRIX, origem, destino);

            uri = requestSigner.SignUrl(uri);

            try
            {
                var response = webRequester.GetFrom(uri);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                double distancia = result.rows[0].elements[0].distance.value;
                int tempo = Convert.ToInt32(result.rows[0].elements[0].duration.value) / 60;

                if (result.status == "OK")
                {
                    return (distancia, tempo);
                }
            }
            catch (Exception ex)
            {
                //Logger.Error("Erro buscando informações de distância e tempo no GMaps", ex);
            }

            return (-1, -1);
        }
    }
}
