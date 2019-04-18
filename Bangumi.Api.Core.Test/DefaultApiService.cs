using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;
using Bangumi.Api.Core.Model.Users;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class ApiService
    {
        private readonly int _cowboy_bebop = 253;
        private DefaultApiService _service;

        public ApiService()
        {
            _service = new DefaultApiService();
        }

        [TestMethod]
        public void DailyCalendar()
        {
            IEnumerable<CalendarResponse> res = _service.GetDailyCalendar();
            Assert.IsTrue(res != null && res.Count() > 0, "The response is empty");
            foreach (var r in res)
            {
                Console.WriteLine(r.ToJson());
            }
        }

        [TestMethod]
        public void GetCowboyBeebopSubjectSmall()
        {
            SubjectSmall res = (SubjectSmall)_service.GetSubject(_cowboy_bebop);
            Assert.AreEqual("カウボーイビバップ", res.Name);
            Console.Write(res);
        }

        [TestMethod]
        public void GetCowboyBeebopSubjectMedium()
        {
            SubjectMedium res = (SubjectMedium)_service.GetSubject(_cowboy_bebop, ResponseGroup.Medium);
            Assert.IsTrue(res.Crt.Count > 0, "The character list is empty");
            Assert.IsTrue(res.Staff.Count > 0, "The staff list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetCowboyBeebopSubjectLarge()
        {
            SubjectLarge res = (SubjectLarge)_service.GetSubject(_cowboy_bebop, ResponseGroup.Large);
            Assert.IsTrue(res.Topic.Count > 0, "The character list is empty");
            Assert.IsTrue(res.Blog.Count > 0, "The staff list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetCowboyBeebopSubjectEp()
        {
            SubjectEp res = _service.GetSubjectEps(_cowboy_bebop);
            Assert.IsTrue(res.Eps.Count > 0, "The episode list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void SearchByMadomagiKeyword()
        {
            string xf = "小圆 新房昭之";
            var res = _service.SearchSubjectByKeywords(xf, SubjectType.Anime);
            Assert.IsTrue(res.Results > 0 && res.List.Count > 4, "The search result is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetUser()
        {
            string username = "sai";
            var res = _service.GetUser(username);
            Assert.AreEqual(1, res.Id, "Incorrect Id");
            Assert.AreEqual("http://bgm.tv/user/sai", res.Url, "Incorrect URL");
            Assert.AreEqual(username, res.Username, "Incorrect username");
            Assert.AreEqual("Sai", res.Nickname, "Incorrect nickname");
            Assert.AreEqual(UserGroup.SuperAdmin, res.Usergroup, "Incorrect user group");
            Assert.IsFalse(string.IsNullOrEmpty(res.Sign), "Empty user signature");
            bool validUrl = res.Avatar.Small.TryCreateUrl() && res.Avatar.Medium.TryCreateUrl() && res.Avatar.Large.TryCreateUrl();
            Assert.IsTrue(validUrl, "One of the image is not a valid url");
            Console.Write(res);
        }
    }
}
