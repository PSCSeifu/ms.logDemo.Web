using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ms.logDemo.Web.Common
{
    public interface IHttpClientsFactory
    {
        Dictionary<string, HttpClient> Clients();
        HttpClient Client(string key);
    }
    public class HttpClientsFactory : IHttpClientsFactory
    {
        public static Dictionary<string, HttpClient> HttpClients { get; set; }
        private readonly ILogger<HttpClientsFactory> _logger;
        private readonly IOptions<ProjectAppSettings> _projectAppSettingsAccessor;

        public HttpClientsFactory(ILogger<HttpClientsFactory> logger,IOptions<ProjectAppSettings> projectAppSettingsAccessor)
        {
            _logger = logger;
            _projectAppSettingsAccessor = projectAppSettingsAccessor;
            HttpClients = new Dictionary<string, HttpClient>();
            Initialise();
        }

        private void Initialise()
        {
            HttpClient client = new HttpClient();

            //ADD LocalServer API Url
            var localAPIServer = _projectAppSettingsAccessor.Value.LocalAPIServer;
            client.BaseAddress = new Uri(localAPIServer);
            HttpClients.Add("LocalAPIServer", client);

            //ADD AzureAPIServer Url
            var azureAPIServer = _projectAppSettingsAccessor.Value.AzureAPIServer;
            client.BaseAddress = new Uri(azureAPIServer);
            HttpClients.Add("AzureAPIServer", client);
        }

        public Dictionary<string, HttpClient> Clients() => HttpClients;
        public HttpClient Client(string key) =>  Clients()[key];
    }
}
