using Bangumi.Api.Core.Model.TokenModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class BangumiClient
    {
        [TestMethod]
        [Timeout(1000 * 60 * 2)]
        public void GetCode()
        {
            Client.BangumiClient client = new Client.BangumiClient();
            AuthCode code = client.RequestCode();

            Assert.IsFalse(code == null || string.IsNullOrEmpty(code.Code), "Empty auth code");
        }

        [TestMethod]
        [Timeout(1000 * 60 * 3)]
        public void GetAccessToken()
        {
            Client.BangumiClient client = new Client.BangumiClient();
            Token token = client.RequestToken();

            Assert.IsNotNull(token, "Empty token response.");
            Assert.IsFalse(string.IsNullOrEmpty(token.AccessToken), "Empty access token");
            Assert.IsFalse(string.IsNullOrEmpty(token.RefreshToken), "Empty refresh token");
            Assert.IsFalse(token.Expired, "Token has expired");
        }

        [TestMethod]
        [Timeout(1000 * 60 * 4)]
        public void RefreshToken()
        {
            Client.BangumiClient client = new Client.BangumiClient();
            client.RequestToken();
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Token refresh = client.RefreshToken();

            Assert.IsNotNull(refresh, "Empty token response.");
            Assert.IsFalse(string.IsNullOrEmpty(refresh.AccessToken), "Empty access token");
            Assert.IsFalse(string.IsNullOrEmpty(refresh.RefreshToken), "Empty refresh token");
            Assert.IsFalse(refresh.Expired, "Token has expired");
        }

        [TestMethod]
        [Timeout(1000 * 60 * 3)]
        public void GetTokenStatus()
        {
            Client.BangumiClient client = new Client.BangumiClient();
            client.RequestToken();
            Thread.Sleep(TimeSpan.FromSeconds(10));
            TokenStatus status = client.TokenStatus;

            Assert.IsNotNull(status, "Empty token response.");
            Assert.IsFalse(string.IsNullOrEmpty(status.AccessToken), "Empty access token");
        }
    }
}
