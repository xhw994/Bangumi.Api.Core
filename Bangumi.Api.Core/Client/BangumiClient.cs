using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using static Bangumi.Api.Core.Extension.StringExtension;
using static Bangumi.Api.Core.Configuration;

namespace Bangumi.Api.Core.Client
{
    public class BangumiClient
    {
        private readonly RestClient _restClient;

        /// <summary>
        /// 查询验证器状态
        /// </summary>
        public bool Authenticated { get => Authenticator == null || Authenticator.Authenticated; }
        private BangumiAuthenticator Authenticator {
            get => (BangumiAuthenticator)_restClient.Authenticator;
            set => _restClient.Authenticator = value;
        }

        public BangumiClient(bool authenticate = false)
        {
            _restClient = new RestClient(ApiBaseUrl);
            Headers = DefaultHeaders();

            // Add authentication when appId is present.
            if (authenticate)
            {
                Authenticate();
            }
        }

        public BangumiClient Authenticate()
        {
            if (_restClient.Authenticator == null && !string.IsNullOrEmpty(AppId))
            {
                _restClient.Authenticator = new BangumiAuthenticator();
            }
            if (!Authenticated)
            {
                Authenticator.OAuthAuthenticate(_restClient);
            }
            if (!Authenticated)
            {
                throw new ApiException(401, "Authorization failed.");
            }
            return this;
        }

        public TResponse Request<TResponse>(BangumiRequest request)
        {
            RestRequest restRequest = new RestRequest(request.Path, request.Method);

            // Add default header, if any
            foreach (var header in Headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
            // Add query parameter, if any
            if (request.QueryParams != null && request.QueryParams.Count > 0)
            {
                foreach (var param in request?.QueryParams)
                {
                    restRequest.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
                }
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

        #region Header

        public Dictionary<string, string> Headers { get; set; }

        private Dictionary<string, string> DefaultHeaders() => new Dictionary<string, string>()
        {
            { "Accept", "application/json" },
            // { "Content-Type", "application/x-www-form-urlencoded" }
        };

        #endregion
    }
}
