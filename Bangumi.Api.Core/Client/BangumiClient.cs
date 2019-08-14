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
using Bangumi.Api.Core.Model.TokenModel;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bangumi.Api.Core.Client
{
    public class BangumiClient : IBangumiClient
    {
        private readonly RestClient _restClient;

        /// <summary>
        /// 查询验证器状态
        /// </summary>
        private BangumiAuthenticator Authenticator
        {
            get => (BangumiAuthenticator)_restClient.Authenticator;
            set => _restClient.Authenticator = value;
        }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>
        {
            { "Accept", "application/json" },
        };

        public BangumiClient(bool authenticate = false)
        {
            _restClient = new RestClient(ApiBaseUrl);

            if (authenticate)
            {
                AuthCode = RequestCode();
                Token = RequestToken(AuthCode);
                _restClient.Authenticator = new BangumiAuthenticator(Token.AccessToken);
            }
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

            if (request.RequireAuth)
            {
                if (Token.Expired)
                {
                    // If AuthCode is expired, request AuthCode first
                    if (AuthCode == null || AuthCode.Expired)
                    {
                        AuthCode = RequestCode();
                    }
                    Token = RequestToken(AuthCode);
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

        #region Authentication
        public AuthCode AuthCode { get; private set; }
        public Token Token { get; private set; }

        public AuthCode RequestCode()
        {
            if (!HttpListener.IsSupported)
            {
                throw new NotSupportedException($"The OS does not support {nameof(HttpListener)}, please upgrade your system.");
            }

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(CallbackUrl);

            string codeUrl = $"{AuthCodeUrl}?client_id={AppId}&response_type=code";
            OpenBrowser(codeUrl);

            listener.Start();
            Console.Write("Please authenticate yourself in the browser tab... ");

            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            string requestUrl = request.Url.ToString().ToLower();
            // Get code from URL
            string code = default;
            string identifier = "?code=";
            DateTime receive = default;
            if (requestUrl.Contains(identifier)) // received request from bangumi.
            {
                receive = DateTime.Now;
                code = requestUrl.Substring(requestUrl.IndexOf(identifier) + identifier.Length);
            }

            // Construct a response.
            string responseString = "<HTML><BODY>Authorization code received! Please close the browser tab.</BODY></HTML>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            // Write response from an output stream.
            HttpListenerResponse response = context.Response;
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }

            Console.Write("completed! The authentication code is: " + code + Environment.NewLine);
            AuthCode authCode = new AuthCode
            {
                Code = code,
                ReceiveTime = receive
            };

            return authCode;
        }

        public Token RequestToken(AuthCode authCode)
        {
            // Validate auth code, request if necessary.
            if (authCode == null || authCode.Expired)
            {
                authCode = RequestCode();
            }

            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", AppId },
                { "client_secret", AppSecret },
                { "code",  authCode.Code },
                { "redirect_uri", CallbackUrl }
            };
            RestRequest request = ComposePostRequest(TokenUrl, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            IRestResponse response = _restClient.Execute(request);
            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting access token: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting access token: " + response.ErrorMessage, response.ErrorMessage);
            }

            // Deserialize response to token and update fields
            Token token = (Token)JsonConvert.DeserializeObject(response.Content, typeof(Token));
            if (string.IsNullOrEmpty(token.AccessToken) || string.IsNullOrEmpty(token.RefreshToken))
            {
                throw new ApiException(400, "Invalid response from server: " + token.ToString());
            }
            token.ReceiveTime = now;
            return token;
        }

        public Token RefreshToken(Token token)
        {
            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", AppId },
                { "client_secret", AppSecret },
                { "refresh_token", token.RefreshToken },
                { "redirect_uri", CallbackUrl }
            };
            RestRequest request = ComposePostRequest(TokenUrl, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            IRestResponse response = _restClient.Execute(request);
            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting token refresh: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting token refresh: " + response.ErrorMessage, response.ErrorMessage);
            }

            // Deserialize response to token and update fields
            Token newToken = (Token)JsonConvert.DeserializeObject(response.Content, typeof(Token));
            if (string.IsNullOrEmpty(newToken.AccessToken) || string.IsNullOrEmpty(newToken.RefreshToken))
            {
                throw new ApiException(400, "Invalid response from server: " + newToken.ToString());
            }
            newToken.ReceiveTime = now;
            return newToken;
        }

        #endregion

        #region Helper
        private void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = $"/c start {url.Replace("&", "^&")}"
                    };
                    Process.Start(psi);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        private RestRequest ComposePostRequest(string url, Dictionary<string, string> queryParams)
        {
            RestRequest request = new RestRequest(url, Method.POST);
            foreach (var header in Headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
            foreach (var param in queryParams)
            {
                request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
            }
            return request;
        }
        #endregion
    }
}
