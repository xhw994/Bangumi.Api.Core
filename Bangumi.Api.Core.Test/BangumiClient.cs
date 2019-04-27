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
            //string callbackUrl = "http://174.1.60.140:5994/";
            string callbackUrl = "http://localhost:5994/";
            CallbackListner listner = new CallbackListner(callbackUrl);

            string code = listner.GetCode();
            Assert.IsFalse(string.IsNullOrEmpty(code), "Listener returned empty code");
            Console.WriteLine(code);
        }
    }
}
