using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseDocuments.Rest.Client
{
    internal class RequestFactory
    {

        internal static IRestRequest CreateRequest(string path, Dictionary<string, string> headers, Method requestMethod, Dictionary<string, string> parameters)
        {
            var request = new RestRequest(path);
            if (parameters != null && parameters.Count() > 0)
            {
                foreach (var item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }

            }
            if (headers != null && headers.Count() > 0)
            {
                foreach (var item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }
            request.Method = requestMethod;
            //request.DateFormat = "yyyy-MM-dd HH:mm:ss";
            return request;
        }
    }
}
