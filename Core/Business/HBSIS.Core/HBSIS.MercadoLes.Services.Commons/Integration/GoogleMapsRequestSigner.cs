using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    public interface IGoogleMapsRequestSigner
    {
        string SignUrl(string url);
    }

    /// <summary>
    /// Classe utilizada para incluir a assinatura na url usada em requisições do Google Maps.
    /// As urls são assinadas apenas quando o ClientId e PrivateKey forem informados.
    /// </summary>
    public class GoogleMapsRequestSigner : IGoogleMapsRequestSigner
    {
        private readonly string clientId;
        private readonly string privateKey;

        public GoogleMapsRequestSigner(string clientId, string privateKey)
        {
            this.clientId = clientId;
            this.privateKey = privateKey;
        }

        /// <summary>
        /// Retorna a Url contendo o parâmetro de assinatura.
        /// </summary>
        /// <param name="url">
        /// Url a ser validada.
        /// A Url não deve ser modificada após ser assinada, pois, a assinatura é validada usando a própria Url.</param>
        /// <returns></returns>
        public string SignUrl(string url)
        {
            // Aplica a assinatura apenas se o ClientId e a PrivateKey foram informados.
            if (!String.IsNullOrEmpty(clientId) && !String.IsNullOrEmpty(privateKey))
            {
                url += "&client=" + clientId;
                url = IncludeSignature(url, privateKey);
            }

            return url;
        }

        // Esse método é igual ao exemplo fornecido pelo Google.
        // https://developers.google.com/maps/documentation/business/webservices/auth?hl=en-us
        private string IncludeSignature(string url, string keyString)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();

            // converting key to bytes will throw an exception, need to replace '-' and '_' characters first.
            string usablePrivateKey = keyString.Replace("-", "+").Replace("_", "/");
            byte[] privateKeyBytes = Convert.FromBase64String(usablePrivateKey);

            Uri uri = new Uri(url);
            byte[] encodedPathAndQueryBytes = encoding.GetBytes(uri.LocalPath + uri.Query);

            // compute the hash
            HMACSHA1 algorithm = new HMACSHA1(privateKeyBytes);
            byte[] hash = algorithm.ComputeHash(encodedPathAndQueryBytes);

            // convert the bytes to string and make url-safe by replacing '+' and '/' characters
            string signature = Convert.ToBase64String(hash).Replace("+", "-").Replace("/", "_");

            // Add the signature to the existing URI.
            return uri.Scheme + "://" + uri.Host + uri.LocalPath + uri.Query + "&signature=" + signature;
        }
    }

}
