using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Bangumi.Api.Core.Client;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class BangumiClient
    {
        [TestMethod]
        public void GetCodeFromListener()
        {
            BangumiAuthenticator authenticator = new BangumiAuthenticator();
            authenticator.RequestAuthCode();
            Console.WriteLine(authenticator.AuthCode);
        }
    }
}
