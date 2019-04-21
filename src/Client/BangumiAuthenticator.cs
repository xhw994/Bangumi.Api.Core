using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Bangumi.Api.Core.Extension.StringExtension;

namespace Bangumi.Api.Core.Client
{
    public class BangumiAuthenticator : IAuthenticator
    {
        private readonly string codePath = "https://bgm.tv/oauth/authorize";
        private readonly string tokenPath = "https://bgm.tv/oauth/access_token";

        private readonly string _appId;
        private readonly string _appSecret;

        private string _authHeader;

        private bool _authorized;

        public BangumiAuthenticator(string appId, string appSecret, string domainName = null, string ipv4Adress = null, string route = null, int port = 5994)
        {
            // Value check, future need to check length and domain properties
            if (!IsAlphaNumeric(appId) || !appId.StartsWith("bgm"))
            {
                throw new ArgumentException($"Invalid application ID <{appId}>. Application IDs should start with `bgm` followed by alphanumeric values");
            }
            if (!IsAlphaNumeric(appSecret))
            {
                throw new ArgumentException($"Invalid application secret. Application secrets should only contain alphanumeric values.");
            }

            // Set credentials
            _appId = appId;
            _appSecret = appSecret;

            // Set domain and ipv4 properties
            DomainName = domainName;
            Ipv4Address = ipv4Adress;
            Route = route;
            Port = port;
        }

        public string DomainName { get; set; }
        public string Ipv4Address { get; set; }
        public string Route { get; set; }
        public int Port { get; set; } = 5994;

        #region Authorization code

        private void RequestAuthCode()
        {
            CallbackListner listner = new CallbackListner(Port)
            {
                DomainName = DomainName,
                Ipv4Address = Ipv4Address,
                Route = Route,
                Port = Port
            }.AddPrefixes();

            string codeUrl = $"{codePath}?client_id={_appId}&response_type=code";
            OpenBrowser(codeUrl);
            AuthCode = listner.GetCode();
            authcodeTime = DateTime.Now;
        }

        public string AuthCode { get; set; }
        public bool AuthCodeExpired { get => !string.IsNullOrEmpty(AuthCode) && authcodeTime + TimeSpan.FromMinutes(1) > DateTime.Now; }
        private DateTime authcodeTime;

        #endregion

        #region Access Token

        public string AccessToken { get; set; }

        #endregion








        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                while (!AuthCodeExpired)
                {
                    // Request authorization code with app_id.
                    RequestAuthCode();
                }
            }



            // only add the Authorization parameter if it hasn't been added by a previous Execute
            if (!request.Parameters.Any(p => p.Type.Equals(ParameterType.HttpHeader) &&
                                             p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
                request.AddParameter("Authorization", _authHeader, ParameterType.HttpHeader);
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

        #endregion
    }
}
