using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Model.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using static Bangumi.Api.Core.Configuration;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class NonAuthenticatingService
    {
        // private readonly int _init_epoch = 1167609600; // 2007.1.1 00:00:00

        private IBangumiService _service;

        [TestInitialize]
        public void InitAndAuthorize()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void SetupConfiguration()
        {
            Assert.IsFalse(string.IsNullOrEmpty(AppId), $"Unable to read {nameof(AppId)} from configuration");
            Assert.IsFalse(string.IsNullOrEmpty(AppSecret), $"Unable to read {nameof(AppSecret)} from configuration");
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
        public void GetAnimeSubjectSmall()
        {
            SubjectSmall res = (SubjectSmall)_service.GetSubject(animeSubjectData.Id.Value, ResponseGroup.Small);
            Assert.AreEqual(animeSubjectData.Id, res.Id, "Incorrect subject ID.");
            Assert.AreEqual(animeSubjectData.Name, res.Name, "Incorrect subject name.");
            Assert.AreEqual(animeSubjectData.NameCn, res.NameCn, "Incorrect subject Chinese name.");
            Assert.AreEqual(animeSubjectData.Url, res.Url, "Incorrect subject URL.");
            Assert.AreEqual(animeSubjectData.Type, res.Type, "Incorrect subject type.");
            Assert.AreEqual(animeSubjectData.Eps, res.Eps, "Incorrect subject episode count.");
            Assert.AreEqual(animeSubjectData.AirDate, res.AirDate, "Incorrect subject airing date.");
            Assert.AreEqual(animeSubjectData.AirWeekday, res.AirWeekday, "Incorrect subject airing weekday.");
        }

        [TestMethod]
        public void GetAnimeSubjectMedium()
        {
            SubjectMedium res = (SubjectMedium)_service.GetSubject(animeSubjectData.Id.Value, ResponseGroup.Medium);
            Assert.IsTrue(res.Crt.Count > 0, "The character list is empty");
            Assert.IsTrue(res.Staff.Count > 0, "The staff list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetAnimeSubjectLarge()
        {
            SubjectLarge res = (SubjectLarge)_service.GetSubject(animeSubjectData.Id.Value, ResponseGroup.Large);
            Assert.IsTrue(res.Topic.Count > 0, "The character list is empty");
            Assert.IsTrue(res.Blog.Count > 0, "The staff list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetCowboyBeebopSubjectEp()
        {
            SubjectEp res = _service.GetSubjectAndEpisodes(animeSubjectData.Id.Value);
            Assert.IsTrue(res.Eps.Count > 0, "The episode list is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void SearchSubject()
        {
            string xf = "小圆 新房昭之";
            var res = _service.SearchSubjectByKeywords(xf, SubjectType.Anime, ResponseGroup.Small);
            Assert.IsTrue(res.Results > 0 && res.List.Count > 4, "The search result is empty");
            Console.Write(res);
        }

        [TestMethod]
        public void GetUser()
        {
            User res = _service.GetUser(userData.Username);
            if (res != userData)
            {
                Assert.AreEqual(userData.Id, res.Id, "Incorrect Id.");
                Assert.AreEqual(userData.Url, res.Url, "Incorrect URL.");
                Assert.AreEqual(userData.Username, res.Username, "Incorrect username.");
                Assert.AreEqual(userData.Nickname, res.Nickname, "Incorrect nickname.");
                Assert.AreEqual(userData.Usergroup, res.Usergroup, "Incorrect user group.");
                Assert.AreEqual(userData.Sign, res.Sign, "Incorrect signature.");
                Assert.AreEqual(userData.Avatar.Large, res.Avatar.Large, "Incorrect large avatar.");
                Assert.AreEqual(userData.Avatar.Medium, res.Avatar.Medium, "Incorrect medium avatar.");
                Assert.AreEqual(userData.Avatar.Small, res.Avatar.Small, "Incorrect small avatar.");
                Console.Write(res);
            }
        }

        [TestMethod]
        public void GetCollectionSmall()
        {
            IEnumerable<SubjectStatus> res = _service.GetCollection(userData.Username, true, null, ResponseGroup.Small);
            Assert.IsTrue(res != null && res.Count() > 0, "Empty collection response.");
            Assert.AreEqual(3, res.Count(), "Incorrect collection count.");
            SubjectStatus status = res.Where(s => s.SubjectId == bookStatusData.SubjectId).FirstOrDefault();
            Assert.IsNotNull(status, "Response does not contain the target subject.");

            if (status != bookStatusData)
            {
                Assert.AreEqual(bookStatusData.SubjectId, status.SubjectId, "Incorrect subject ID.");
                Assert.AreEqual(bookStatusData.Name, status.Name, "Incorrect subject name.");
                Assert.AreEqual(bookStatusData.VolStatus, status.VolStatus, "Incorrect subject volume status.");
                Assert.AreEqual(bookStatusData.EpStatus, status.EpStatus, "Incorrect subject episode status.");
                Assert.AreEqual(bookStatusData.Lasttouch, status.Lasttouch, "Incorrect subject last touch time.");
            }
        }

        [TestMethod]
        public void GetCollectionMedium()
        {
            var res = _service.GetCollection(userData.Username, false, null, ResponseGroup.Medium);
            Assert.IsTrue(res != null && res.Count() > 0, "Empty collection response.");
            Assert.AreEqual(1, res.Count(), "Incorrect collection count.");
            SubjectStatus status = res.Where(s => s.SubjectId == animeStatusData.SubjectId).FirstOrDefault();
            Assert.IsNotNull(status, "Response does not contain the target subject.");

            if (status != animeStatusData)
            {
                Assert.AreEqual(animeStatusData.SubjectId, status.SubjectId, "Incorrect subject ID.");
                Assert.AreEqual(animeStatusData.Name, status.Name, "Incorrect subject name.");
                Assert.AreEqual(animeStatusData.VolStatus, status.VolStatus, "Incorrect subject volume status.");
                Assert.AreEqual(animeStatusData.EpStatus, status.EpStatus, "Incorrect subject episode status.");
                Assert.IsTrue(animeStatusData.Lasttouch < status.Lasttouch, "Incorrect subject last touch time.");
                Assert.AreEqual(animeStatusData.Subject.Id, status.Subject.Id, "Incorrect subject details.");
            }
        }

        [TestMethod]
        public void GetCollectionsByType()
        {
            int maxResult = 5;
            SubjectType type = SubjectType.Anime;

            var res = _service.GetCollectionsByType(userData.Username, SubjectType.Anime, maxResult);
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

        [TestMethod]
        public void GetCollectionStatus()
        {
            IEnumerable<CollectionsByType> status = _service.GetCollectionsStatus(userData.Username);
        }

        #region TestData
        private static readonly User userData = new User
        {
            Id = 490658,
            Username = "490658",
            Nickname = "xhw994",
            Url = "http://bgm.tv/user/490658",
            Sign = "Pray4KyoAni",
            Avatar = new Avatar
            {
                Large = "http://lain.bgm.tv/pic/user/l/icon.jpg",
                Medium = "http://lain.bgm.tv/pic/user/m/icon.jpg",
                Small = "http://lain.bgm.tv/pic/user/s/icon.jpg"
            },
            Usergroup = UserGroup.User
        };

        private static readonly SubjectStatus bookStatusData = new SubjectStatus
        {
            SubjectId = 27684,
            Name = "ドラえもん",
            EpStatus = 15,
            VolStatus = 45,
            Lasttouch = 1565588926,
        };

        private static readonly SubjectStatus animeStatusData = new SubjectStatus
        {
            SubjectId = 253,
            Name = "カウボーイビバップ",
            EpStatus = 0,
            VolStatus = 0,
            Lasttouch = 1565594084,
            Subject = new SubjectSmall { Id = 253 }
        };

        private static readonly SubjectBase animeSubjectData = new SubjectBase
        {
            Id = 253,
            Url = "http://bgm.tv/subject/253",
            Type = SubjectType.Anime,
            Name = "カウボーイビバップ",
            NameCn = "星际牛仔",
            Eps = 26,
            AirDate = "1998-10-23",
            AirWeekday = 5,
        };
        #endregion
    }
}
