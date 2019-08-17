using Bangumi.Api.Core.Model.TokenModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using static Bangumi.Api.Core.Configuration;

namespace Bangumi.Api.Core.Client
{
    // TODO: Implement state param, Auto refresh?, Add timeout when requesting code 3 min?
    public class BangumiClient : IBangumiClient
    {
        private readonly RestClient _restClient = new RestClient(ApiBaseUrl);

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>
        {
            { "Accept", "application/json" },
        };

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
                if (Token == null || Token.Expired)
                {
                    // If AuthCode is expired, request AuthCode first
                    if (AuthCode == null || AuthCode.Expired)
                    {
                        AuthCode = RequestCode();
                    }
                    Token = RequestToken(AuthCode);
                }
                if (_restClient.Authenticator == null || ((BangumiAuthenticator)_restClient.Authenticator).AccessToken != Token.AccessToken)
                {
                    _restClient.Authenticator = new BangumiAuthenticator(Token.AccessToken);
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
        public TokenStatus TokenStatus
        {
            get
            {
                if (TokenStatus == null && !Token.Expired)
                {
                    return GetTokenStatus(Token);
                }
                return null;
            }
        }

        public Token RequestToken()
        {
            if (AuthCode == null || AuthCode.Expired)
            {
                AuthCode = RequestCode();
            }
            Token = RequestToken(AuthCode);
            return Token;
        }

        public Token RefreshToken()
        {
            if (Token == null || Token.Expired)
            {
                throw new InvalidOperationException("Cannot refresh empty token.");
            }
            Token = RefreshToken(Token);
            return Token;
        }

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

            Console.Write("completed!" + Environment.NewLine);
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
            BangumiRequest request = new BangumiRequest(TokenUrl, Method.POST, false, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            Token token = Request<Token>(request);
            token.ReceiveTime = now;
            Console.WriteLine("The token is: " + token.AccessToken);
            return token;
        }

        public Token RefreshToken(Token token)
        {
            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                throw new ArgumentException("Empty token", nameof(token));
            }
            if (token.Expired)
            {
                throw new ArgumentException("Token already expired", nameof(token));
            }

            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "refresh_token" },
                { "client_id", AppId },
                { "client_secret", AppSecret },
                { "refresh_token", token.RefreshToken },
                { "redirect_uri", CallbackUrl }
            };
            BangumiRequest request = new BangumiRequest(TokenUrl, Method.POST, false, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            Token newToken = Request<Token>(request);
            newToken.ReceiveTime = now;
            return newToken;
        }

        public TokenStatus GetTokenStatus(Token token)
        {
            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                throw new ArgumentException("Empty token", nameof(token));
            }

            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "access_token", token.AccessToken }
            };
            BangumiRequest request = new BangumiRequest(TokenStatusUrl, Method.POST, false, queryParams);

            // Get the response
            TokenStatus status = Request<TokenStatus>(request);
            return status;
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
