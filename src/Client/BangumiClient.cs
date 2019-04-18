using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Authenticators;
using Bangumi.Api.Core.Model;

namespace Bangumi.Api.Core.Client
{
    public class BangumiClient
    {
        public string ApiBasePath { get; } = "https://api.bgm.tv/";

        /// <summary>
        /// Gets or sets the RestClient.
        /// </summary>
        /// <value>An instance of the RestClient</value>
        private readonly RestClient _restClient;

        public Dictionary<string, string> Headers { get; }

        public void AddHeader(string key, string value) => Headers.Add(key, value);
        public void RemoveHeader(string key, string value) => Headers.Remove(key);
        public void ClearHeader() => Headers.Clear();

        public BangumiClient()
        {
            _restClient = new RestClient(ApiBasePath);
            Headers = new Dictionary<string, string>();
        }

        public BangumiClient(string appId, string appSecret)
        {
            _restClient = new RestClient(ApiBasePath)
            {
                Authenticator = new HttpBasicAuthenticator(appId, appSecret)
            };
        }

        public TResponse Request<TResponse>(IRequest request)
        {
            RestRequest restRequest = new RestRequest(request.Path, request.Method);

            if (request.RequireAuth)
            {
                // Add authorization
            }

            // Add default header, if any
            foreach (var header in Headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
            // Add query parameter, if any
            foreach (var param in request.QueryParams)
            {
                restRequest.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
            }

            IRestResponse response = _restClient.Execute(restRequest);
            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, $"Error calling {nameof(request)}: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"Error calling {nameof(request)}: " + response.ErrorMessage, response.ErrorMessage);
            }

            return (TResponse)JsonConvert.DeserializeObject(response.Content, typeof(TResponse));
        }
    }
}
