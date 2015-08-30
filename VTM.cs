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
            var url = string.Format("https://{0}:{1}", host, port);

            // Create HTTP Client for all future requests to API
            this.client = new HttpClient(handler);
            this.client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public VirtualServerStatistics fetchVirtualServerStats(string href)
        {
            HttpResponseMessage response = client.GetAsync(href).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return (response.Content.ReadAsAsync<VirtualServerStatObject>().Result).statistics;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        public Children fetchVirtualServers()
        {
            // List data response.
            HttpResponseMessage response = client.GetAsync("/api/tm/3.3/status/local_tm/statistics/virtual_servers").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsAsync<Children>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        public NodeStatistics fetchNodeStats(string href)
        {
            HttpResponseMessage response = client.GetAsync(href).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return (response.Content.ReadAsAsync<NodeStatObject>().Result).statistics;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        public Children fetchNodes()
        {
            // List data response.
            HttpResponseMessage response = client.GetAsync("/api/tm/3.3/status/local_tm/statistics/nodes/node").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsAsync<Children>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        public PoolStatistics fetchPoolStats(string href)
        {
            HttpResponseMessage response = client.GetAsync(href).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return (response.Content.ReadAsAsync<PoolStatObject>().Result).statistics;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }

        public Children fetchPools()
        {
            // List data response.
            HttpResponseMessage response = client.GetAsync("/api/tm/3.3/status/local_tm/statistics/pools").Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                return response.Content.ReadAsAsync<Children>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }
        }
    }
}
