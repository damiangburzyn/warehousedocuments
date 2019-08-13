using log4net;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDocuments.Rest.Client
{

    public class RestReqClient
    {

        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<OAuthResult> OAuthQuery()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var restClient = new RestClient();
            Dictionary<string, string> headers = new Dictionary<string, string>();
            var baseUrl = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            headers.Add("content-type", "application/x-www-form-urlencoded");
            var request = RequestFactory.CreateRequest(string.Empty, headers, Method.POST, parameters);
            restClient.Proxy = WebRequest.DefaultWebProxy;
            var response = await restClient.ExecuteTaskAsync<OAuthResult>(request);
            if (response.ErrorException != null || response.ErrorMessage != null)
            {
                _logger.Error($"Wystąpił błąd autoryzacji   {response.ErrorMessage ?? string.Empty}", response.ErrorException);
            }
            return response.Data;
        }
    }
}
