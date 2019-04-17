using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Bangumi.Api.Core;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class ApiService
    {
        private DefaultApiService _service;

        public ApiService()
        {
            _service = new DefaultApiService();
        }

        [TestMethod]
        // 每日放送
        public void DailyCalendar()
        {
            IEnumerable<CalendarResponse> res = _service.GetDailyCalendar();
            Assert.IsTrue(res != null && res.Count() > 0, "The response is empty");
            foreach (var r in res)
            {
                System.Console.WriteLine(r.ToJson());
            }
        }

        [TestMethod]
        public void GetCowboyBeebopSubject()
        {
            const int id = 253;

            SubjectSmall small = (SubjectSmall)_service.GetSubject(id);
            Assert.AreEqual("カウボーイビバップ", small.Name);

            SubjectMedium medium = (SubjectMedium)_service.GetSubject(id, ResponseGroup.Medium);
            Assert.IsTrue(medium.Crt.Count > 0, "The character list is empty");
            Assert.IsTrue(medium.Staff.Count > 0, "The staff list is empty");

            SubjectLarge large = (SubjectLarge)_service.GetSubject(id, ResponseGroup.Large);
            Assert.IsTrue(large.Topic.Count > 0, "The character list is empty");
            Assert.IsTrue(large.Blog.Count > 0, "The staff list is empty");

            SubjectEp ep = _service.GetSubjectEps(id);
            Assert.IsTrue(ep.Eps.Count > 0, "The episode list is empty");
        }
    }
}
