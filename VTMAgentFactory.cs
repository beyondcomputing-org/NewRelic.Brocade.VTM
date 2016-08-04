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
            string name = (string)properties["name"];
            string host = (string)properties["host"];
            int port = int.Parse((string)properties["port"]);
            string username = (string)properties["username"];
            string password = (string)properties["password"];
            string APIVersion = "3.3";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("'name', 'host', 'port', 'username' and 'password' cannot be null or empty. Do you have a 'config/plugin.json' file?");
            }

            return new VTMAgent(name, host, port, username, password, APIVersion);
        }
    }
}
