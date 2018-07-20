using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HBSIS.MercadoLes.Services.Commons.Integration
{
    /// <summary>
    /// Interface utilizada para abstrair a requisição web.
    /// </summary>
    public interface IWebRequester
    {
        /// <summary>
        /// Retorna uma string com os dados obtidos na url informada.
        /// </summary>
        /// <param name="url">Url a ser requisitada</param>
        /// <returns>Dados obtidos</returns>
        string GetFrom(string url);
    }

    /// <summary>
    /// Implementação padrão da interface IWebRequester.
    /// </summary>
    public class DefaultWebRequester : IWebRequester
    {
        public string GetFrom(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            if (request == null)
            {
                return null;
            }

            request.Proxy = WebRequest.GetSystemWebProxy();
            if (request.Proxy != null)
            {
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
