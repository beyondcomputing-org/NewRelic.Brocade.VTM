using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Org.BeyondComputing.NewRelic.Brocade.VTM
{
    class VTM
    {
        private HttpClient client;

        public VTM(string host, int port, string username, string password)
        {
            // HTTP Client settings
            var credentials = new NetworkCredential(username, password);

            var handler = new HttpClientHandler { Credentials = credentials };
            #if DEBUG
            //Trust Self Signed Certs in local development environment
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            #endif

            var url = string.Format("https://{0}:{1}", host, port);

            // Create HTTP Client for all future requests to API
            this.client = new HttpClient(handler);
            this.client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public T fetchVTMObject<T>(string href)
        {
            HttpResponseMessage response = client.GetAsync(href).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return  response.Content.ReadAsAsync<T>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new ApplicationException("Error retrieving data from VTM");
            }
        }

    }
}
