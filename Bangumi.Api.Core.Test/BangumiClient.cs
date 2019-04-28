using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Bangumi.Api.Core.Client;
using RestSharp;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class BangumiClient
    {
        [TestMethod]
        [Timeout(1000 * 60 * 2)]
        public void GetCodeFromListener()
        {
            BangumiAuthenticator authenticator = new BangumiAuthenticator();
            authenticator.RequestAuthCode();
            Assert.IsFalse(string.IsNullOrEmpty(authenticator.AuthCode), "Empty auth code");
            Console.WriteLine("The auth code is: " + authenticator.AuthCode);
        }

        [TestMethod]
        [Timeout(1000 * 60 * 3)]
        public void GetAccessToken()
        {
            // Get Auth code
            BangumiAuthenticator authenticator = new BangumiAuthenticator();
            authenticator.RequestAuthCode();
            Assert.IsFalse(string.IsNullOrEmpty(authenticator.AuthCode), "Empty auth code");
            Console.WriteLine("The auth code is: " + authenticator.AuthCode);

            // Get token
            Assert.IsFalse(authenticator.AuthCodeExpired, "Auth code has expired");
            var restClient = new RestClient(Configuration.ApiBaseUrl);
            authenticator.RequestAccessToken(restClient);

            // Output
            Console.WriteLine("The access token is: " + authenticator.AccessToken);
            Assert.IsFalse(string.IsNullOrEmpty(authenticator.AccessToken), "Empty access token");
            Console.WriteLine("The refresh token is: " + authenticator.AccessToken);
            Assert.IsFalse(string.IsNullOrEmpty(authenticator.RefreshToken), "Empty refresh token");
            Console.WriteLine("The token expires on: " + authenticator.TokenExpireTime);
            Assert.IsFalse(authenticator.TokenExpired, "Token has expired");
        }
    }
}
