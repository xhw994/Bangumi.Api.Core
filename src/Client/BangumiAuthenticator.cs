using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Bangumi.Api.Core.Extension.StringExtension;
using static Bangumi.Api.Core.Configuration;
using Bangumi.Api.Core.Model.Token;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Client
{
    public class BangumiAuthenticator : IAuthenticator
    {
        // TODO: Implement state param, Auto refresh?

        private string _authHeader;
        private bool _authorized;

        public BangumiAuthenticator()
        {
            // Value check, future need to check length and domain properties
            if (!IsAlphaNumeric(AppId) || !AppId.StartsWith("bgm"))
            {
                throw new ArgumentException($"Invalid application ID <{AppId}>. Application IDs should start with `bgm` followed by alphanumeric values");
            }
            if (!IsAlphaNumeric(AppSecret))
            {
                throw new ArgumentException($"Invalid application secret <{AppSecret}>. Application secrets should only contain alphanumeric values.");
            }
        }

        #region Authorization code

        private void RequestAuthCode()
        {
            CallbackListner listner = new CallbackListner(CallbackUrl);

            string codeUrl = $"{AuthCodeUrl}?client_id={AppId}&response_type=code";
            OpenBrowser(codeUrl);
            authcodeTime = DateTime.Now;
            AuthCode = listner.GetCode();
        }

        public string AuthCode { get; private set; }
        public bool AuthCodeExpired { get => !string.IsNullOrEmpty(AuthCode) && authcodeTime + TimeSpan.FromMinutes(1) > DateTime.Now; }
        private DateTime authcodeTime;

        #endregion

        #region Access Token

        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public bool TokenExpired { get => !string.IsNullOrEmpty(AccessToken) && tokenExpireTime > DateTime.Now; }
        public bool Authenticated { get => TokenExpired; }
        private DateTime tokenExpireTime;

        private void RequestAccessToken(IRestClient client)
        {
            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", AppId },
                { "client_secret", AppSecret },
                { "code", AuthCode },
                { "redirect_uri", CallbackUrl }
            };
            RestRequest request = ComposePostRequest(TokenUrl, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting access token: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting access token: " + response.ErrorMessage, response.ErrorMessage);
            }
            
            // Deserialize response to token and update fields
            GetTokenResponse tokenResponse = (GetTokenResponse)JsonConvert.DeserializeObject(response.Content, typeof(GetTokenResponse));
            if (string.IsNullOrEmpty(tokenResponse.AccessToken) || string.IsNullOrEmpty(tokenResponse.RefreshToken) || !tokenResponse.ExpiresIn.HasValue)
            {
                throw new ApiException(400, "Invalid response from server: " + tokenResponse.ToString());
            }
            AccessToken = tokenResponse.AccessToken;
            RefreshToken = tokenResponse.RefreshToken;
            tokenExpireTime = now + TimeSpan.FromSeconds(tokenResponse.ExpiresIn.Value - 60); // Reduce 1 min for possible network issues.
        }

        private void RequestTokenRefresh(IRestClient client)
        {
            // Compose the post request
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", AppId },
                { "client_secret", AppSecret },
                { "refresh_token", RefreshToken },
                { "redirect_uri", CallbackUrl }
            };
            RestRequest request = ComposePostRequest(TokenUrl, queryParams);

            // Get the response
            DateTime now = DateTime.Now;
            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting token refresh: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"There is an error when requesting token refresh: " + response.ErrorMessage, response.ErrorMessage);
            }

            // Deserialize response to token and update fields
            RefreshTokenResponse tokenResponse = (RefreshTokenResponse)JsonConvert.DeserializeObject(response.Content, typeof(RefreshTokenResponse));
            if (string.IsNullOrEmpty(tokenResponse.AccessToken) || string.IsNullOrEmpty(tokenResponse.RefreshToken) || !tokenResponse.ExpiresIn.HasValue)
            {
                throw new ApiException(400, "Invalid response from server: " + tokenResponse.ToString());
            }
            AccessToken = tokenResponse.AccessToken;
            RefreshToken = tokenResponse.RefreshToken;
            tokenExpireTime = now + TimeSpan.FromSeconds(tokenResponse.ExpiresIn.Value - 60); // Reduce 1 min for possible network issues.
        }

        #endregion

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            // Request Auth Token if not yet authenticated
            if (TokenExpired)
            {
                // If AuthCode is expired, request AuthCode first
                if (AuthCodeExpired)
                {
                    RequestAuthCode();
                }
                RequestAccessToken(client);
            }
            // Refresh token when it is only valid for no more than 5 minutes
            else if (DateTime.Now + TimeSpan.FromMinutes(5) > tokenExpireTime)
            {
                RequestTokenRefresh(client);
            }

            // If everything is set, add `Authorization: Bear <Token>` to header
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
        }


        #region Helpers

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
            request.AddHeader("Accept", "application/json");
            foreach (var param in queryParams)
            {
                request.AddParameter(param.Key, param.Value, ParameterType.GetOrPost);
            }
            return request;
        }

        #endregion
    }
}
