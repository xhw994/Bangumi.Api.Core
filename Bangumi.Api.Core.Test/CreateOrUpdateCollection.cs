using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Bangumi.Api.Core.Model.UserModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using static Bangumi.Api.Core.Extension.StringExtension;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class CreateOrUpdateCollection
    {
        private const int subjectId = 9717;
        private const EpStatus statusBefore = EpStatus.Watched;
        private const EpStatus statusAfter = EpStatus.Drop;
        private readonly CollectionResponse detailBefore = new CollectionResponse
        {
            Status = new CollectionStatusInfo
            {
                Id = CollectionStatus.Collect,
                Type = CollectionStatus.Collect.ToDescriptionString(),
                Name = CollectionStatus.Collect.ToCnName(SubjectType.Anime),
            },
            Rating = 10,
            Comment = "商业与实验气质并重的奇迹作品",
            Private = Privacy.Public,
            Tag = new string[] { "TV", "SHAFT", "原创" },
            EpStatus = 12,
            User = new User { Id = 490658 }
        };

        private readonly CollectionResponse detailAfter = new CollectionResponse
        {
            Status = new CollectionStatusInfo
            {
                Id = CollectionStatus.OnHold,
                Type = CollectionStatus.OnHold.ToDescriptionString(),
                Name = CollectionStatus.OnHold.ToCnName(SubjectType.Anime),
            },
            Rating = 1,
            Comment = "愚蠢的圆黑",
            Private = Privacy.Private,
            Tag = new string[] { "里番", "黑深残" },
            User = new User { Id = 490658 }
        };


        private DefaultBangumiService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void UpdateSubjectDetail()
        {
            CollectionResponse response = _service.CreateOrUpdateCollection(subjectId,
                detailBefore.Status.Id,
                detailAfter.Comment,
                string.Join(",", detailAfter.Tag),
                detailAfter.Rating,
                detailAfter.Private
            );
            Console.WriteLine(response);
            Assert.AreEqual(detailAfter.Status.Id, response.Status.Id, "Incorrect collection status.");
            Assert.AreEqual(detailAfter.Status.Type, response.Status.Type, "Incorrect collection status.");
            Assert.AreEqual(detailAfter.Status.Name, response.Status.Name, "Incorrect collection status.");
            Assert.AreEqual(detailAfter.Rating, response.Rating, "Incorrect rating.");
            Assert.AreEqual(detailAfter.Comment, response.Comment, "Incorrect comment.");
            Assert.AreEqual(detailAfter.Private, response.Private, "Incorrect privacy level.");
            Assert.IsTrue(HaveSameElements(detailAfter.Tag, response.Tag), "Incorrect tags.");
            Assert.AreEqual(detailBefore.User.Id, response.User.Id, "Incorrect user");
            Thread.Sleep(5000);
        }

        [TestCleanup]
        public void Teardown()
        {
            CollectionResponse response = _service.CreateOrUpdateCollection(subjectId,
                detailBefore.Status.Id,
                detailBefore.Comment,
                string.Join(",", detailBefore.Tag),
                detailBefore.Rating,
                detailBefore.Private
            );
            Console.WriteLine(response);
            Assert.AreEqual(detailBefore.Status.Id, response.Status.Id, "Incorrect collection status.");
            Assert.AreEqual(detailBefore.Status.Type, response.Status.Type, "Incorrect collection status.");
            Assert.AreEqual(detailBefore.Status.Name, response.Status.Name, "Incorrect collection status.");
            Assert.AreEqual(detailBefore.Rating, response.Rating, "Incorrect rating.");
            Assert.AreEqual(detailBefore.Comment, response.Comment, "Incorrect comment.");
            Assert.AreEqual(detailBefore.Private, response.Private, "Incorrect privacy level.");
            Assert.IsTrue(HaveSameElements(detailBefore.Tag, response.Tag), "Incorrect tags.");
            Assert.AreEqual(detailBefore.EpStatus, response.EpStatus, "Incorrect episode status");
            Assert.AreEqual(detailBefore.User.Id, response.User.Id, "Incorrect user");
        }
    }
}