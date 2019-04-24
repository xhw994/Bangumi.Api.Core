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

        public bool Authenticated { get => _restClient?.Authenticator != null; }

        #region Header

        public Dictionary<string, string> Headers { get; }

        public void AddHeader(string key, string value) => Headers.Add(key, value);
        public void RemoveHeader(string key, string value) => Headers.Remove(key);
        public void ClearHeader() => Headers.Clear();

        #endregion

        public BangumiClient()
        {
            _restClient = new RestClient(ApiBaseUrl);
            Headers = DefaultHeaders();
            if (!string.IsNullOrEmpty(AppId))
            {
                Authenticate(AppId, AppSecret);
            }
        }

        public BangumiClient(string appId, string appSecret)
        {
            _restClient = new RestClient(ApiBaseUrl);
            Headers = DefaultHeaders();
            Authenticate(appId, appSecret);
        }

        private Dictionary<string, string> DefaultHeaders()
        {
            return new Dictionary<string, string>()
            {
                { "Accept", "application/json" },
                { "Content-Type", "application/x-www-form-urlencoded" }
            };
        }

        public BangumiClient Authenticate(string appId, string appSecret)
        {
            // To lowercase values
            appId = appId.ToLower();
            appSecret = appSecret.ToLower();

            // Value check, future need to check length
            if (!IsAlphaNumeric(appId) || !appId.StartsWith("bgm"))
            {
                throw new ArgumentException($"Invalid application ID <{appId}>. Application IDs should start with `bgm` followed by alphanumeric values");
            }
            if (!IsAlphaNumeric(appSecret))
            {
                throw new ArgumentException($"Invalid application secret. Application secrets should only contain alphanumeric values.");
            }

            // Add Authenticator
            _restClient.Authenticator = new HttpBasicAuthenticator(appId, appSecret);
            return this;
        }

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
