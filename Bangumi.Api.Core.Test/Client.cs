using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bangumi.Api.Core;
using Bangumi.Api.Core.Model;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class Client
    {
        private DefaultApi _api;

        public Client()
        {
            _api = new DefaultApi();
        }

        [TestMethod]
        public void TestMethod1()
        {
            List<CalendarResponse> res = _api.CalendarGet();
            Assert.IsTrue(res != null && res.Count > 0, "The response is empty");
        }
    }
}
