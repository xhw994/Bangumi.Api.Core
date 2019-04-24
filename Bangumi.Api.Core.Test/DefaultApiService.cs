using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Bangumi.Api.Core.Extension;
using static Bangumi.Api.Core.Extension.StringExtension;
using static Bangumi.Api.Core.Configuration;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Bangumi.Api.Core.Model.UserModel;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class ApiService
    {
        private readonly int _cowboy_bebop = 253;
        private readonly string _username = "sai";
        private readonly int _init_epoch = 1167609600; // 2007.1.1 00:00:00

        private DefaultBangumiService _service;

        public ApiService()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void SetupConfiguration()
        {
            _service = new DefaultBangumiService();
            Assert.IsNotNull(AppId, "Unable to get the configuration");
            Console.WriteLine("Successfully get the configuration. As an example, the app ID is: " + AppId);
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
            var res = _service.GetUser(_username);
            Assert.AreEqual(1, res.Id, "Incorrect Id");
            Assert.AreEqual("http://bgm.tv/user/sai", res.Url, "Incorrect URL");
            Assert.AreEqual(_username, res.Username, "Incorrect username");
            Assert.AreEqual("Sai", res.Nickname, "Incorrect nickname");
            Assert.AreEqual(UserGroup.SuperAdmin, res.Usergroup, "Incorrect user group");
            Assert.IsFalse(string.IsNullOrEmpty(res.Sign), "Empty user signature");
            bool validUrl = IsHttpOrHttpsUrl(res.Avatar.Small) && IsHttpOrHttpsUrl(res.Avatar.Medium) && IsHttpOrHttpsUrl(res.Avatar.Large);
            Assert.IsTrue(validUrl, "One of the image is not a valid url");
            Console.Write(res);
        }

        [TestMethod]
        public void GetUserCollectionSmall()
        {
            var res = _service.GetUserCollection(_username, false, ResponseGroup.Small);
            Assert.AreNotEqual(0, res.Count(), "Empty response");
            foreach (var sj in res)
            {
                Assert.IsFalse(string.IsNullOrEmpty(sj.Name), "Empty subject name");
                Assert.IsFalse(sj.SubjectId < 1, "Invalid subject Id: " + sj.SubjectId);
                Assert.IsFalse(sj.EpStatus < 0, "Invalid episode status: " + sj.EpStatus);
                Assert.IsFalse(sj.VolStatus < 0, "Invalid volume status: " + sj.VolStatus);
                Assert.IsFalse(sj.Lasttouch < _init_epoch, "Invalid last touch epoch: " + sj.Lasttouch);
            }
        }

        [TestMethod]
        public void GetUserCollectionMedium()
        {
            var res = _service.GetUserCollection(_username, false, ResponseGroup.Medium);
            Assert.AreNotEqual(0, res.Count(), "Empty response");
            foreach (var sj in res)
            {
                Assert.IsFalse(string.IsNullOrEmpty(sj.Name), "Empty subject name");
                Assert.IsFalse(sj.SubjectId < 1, "Invalid subject Id: " + sj.SubjectId);
                Assert.IsFalse(sj.EpStatus < 0, "Invalid episode status: " + sj.EpStatus);
                Assert.IsFalse(sj.VolStatus < 0, "Invalid volume status: " + sj.VolStatus);
                Assert.IsFalse(sj.Lasttouch < _init_epoch, "Invalid last touch epoch: " + sj.Lasttouch);

                Assert.IsNotNull(sj.Subject, "Null subject content");
            }
        }

        [TestMethod]
        public void GetUserCollectionsByType()
        {
            int maxResult = 5;
            SubjectType type = SubjectType.Anime;

            var res = _service.GetUserCollectionsByType(_username, SubjectType.Anime, AppId, maxResult);
            Assert.AreEqual(1, res.Count(), "The outer layer should only contain 1 element.");

            CollectionsByType collections = res.ElementAt(0);
            Assert.AreEqual(type, collections.Type, "Type enum value mismatch");
            Assert.AreEqual(type.ToDescriptionString(), collections.Type.ToDescriptionString(), "Type name mismatch");
            Assert.AreEqual(type.ToCnName(), collections.Type.ToCnName(), "Type CN name mismatch");

            IEnumerable<Collect> collects = collections.Collects;
            Assert.AreEqual(5, collects.Count(), $"Collection does not contain 5 categories based on {nameof(CollectionStatus)}");

            string msgGt = $"This might be a bug in this package, or it might be caused by the server.";
            string msgLt = $"You may want to decrease {maxResult}";
            foreach (Collect collect in collects)
            {
                Assert.AreEqual(maxResult, collect.List.Count,
                    $"The number of collections of {collect.Status.Name} type is {(collect.List.Count > maxResult ? "greater" : "less")} " +
                    $"than the expected value {maxResult}. {(collect.List.Count > maxResult ? msgGt : msgLt)}"
                );
            }

            // Only test <CollectionStatus> on the collected ones
            CollectionStatus collected = CollectionStatus.Collect;
            Collect first = collects.First(c => c.Status.Id == collected);
            Assert.AreEqual(first.Status.Type, collected.ToDescriptionString(), "CollectionStatus type mismatch");
            // Will not bother validating CN name here because there are too much variants
        }
    }
}
