using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewRelic.Platform.Sdk;

namespace Org.BeyondComputing.NewRelic.Brocade.VTM
{
    class VTMAgentFactory : AgentFactory
    {
        public override Agent CreateAgentWithConfiguration(IDictionary<string, object> properties)
        {
            // Set the default version of the API to use if not found in the config file
            const decimal _APIVersion = 6.0m;

            string name = (string)properties["name"];
            string host = (string)properties["host"];
            int port = int.Parse((string)properties["port"]);
            string username = (string)properties["username"];
            string password = (string)properties["password"];
            decimal APIVersion = properties.ContainsKey("api_version") ? decimal.Parse(properties["api_version"].ToString()) : _APIVersion;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("'name', 'host', 'port', 'username' and 'password' cannot be null or empty. Do you have a 'config/plugin.json' file?");
            }

            return new VTMAgent(name, host, port, username, password, APIVersion);
        }
    }
}
