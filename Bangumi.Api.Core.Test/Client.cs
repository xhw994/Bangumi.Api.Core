using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bangumi.Api.Core;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;

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
        // √ø»’∑≈ÀÕ
        public void DailyCalendar()
        {
            List<CalendarResponse> res = _api.CalendarGet();
            Assert.IsTrue(res != null && res.Count > 0, "The response is empty");
            foreach (var r in res)
            {
                System.Console.WriteLine(r.ToJson());
            }
        }
    }
}
