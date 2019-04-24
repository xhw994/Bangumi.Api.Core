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
        /// <remarks>
        /// 注意这只代表此实例是否拥有<see cref="BangumiAuthenticator"/>，并不确保用户不需要进行验证。
        /// </remarks>
        public bool Authenticated { get => _restClient?.Authenticator != null; }

        #region Header

        public Dictionary<string, string> Headers { get; }

        public void AddHeader(string key, string value) => Headers.Add(key, value);
        public void RemoveHeader(string key, string value) => Headers.Remove(key);
        public void ClearHeader() => Headers.Clear();
        private Dictionary<string, string> DefaultHeaders() => new Dictionary<string, string>()
        {
            { "Accept", "application/json" },
            // { "Content-Type", "application/x-www-form-urlencoded" }
        };

        #endregion

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
            return this;
        }

        //public TokenStatusResponse GetAuthenticationStatus()

        public TResponse Request<TResponse>(BangumiRequest request)
        {
            RestRequest restRequest = new RestRequest(request.Path, request.Method);

            if (request.RequireAuth)
            {
                if (!Authenticated)
                {
                    throw new ApiException(401, $"The client needs to be authenticated for calling {nameof(request)}");
                }

                // Add auth headers?
            }

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
    }
}
