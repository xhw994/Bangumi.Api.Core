using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class BangumiClientTest
    {
        BangumiClient _client;

        public BangumiClientTest()
        {
            _client = new BangumiClient();
        }

        [TestMethod]
        public void DailyCalendar()
        {
            //var res = _client.Request<IEnumerable<CalendarResponse>>(new DailyCalendarRequest());
            //Assert.AreNotEqual(0, res.Count(), "Client returned empty result.");
        }
    }
}
