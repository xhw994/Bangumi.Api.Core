using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Model.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Bangumi.Api.Core.Extension.StringExtension;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class GetUserSubjectDetail
    {
        private const int subjectId = 9717;
        private readonly CollectionResponse subjectDetail = new CollectionResponse
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
            Lasttouch = 0,
            User = new User { Id = 490658 }
        };
        private DefaultBangumiService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void GetSubjectDetail()
        {
            CollectionResponse response = _service.GetUserSubjectDetail(subjectId);
            Assert.AreEqual(subjectDetail.Status.Id, response.Status.Id, "Incorrect collection status.");
            Assert.AreEqual(subjectDetail.Status.Type, response.Status.Type, "Incorrect collection status.");
            Assert.AreEqual(subjectDetail.Status.Name, response.Status.Name, "Incorrect collection status.");
            Assert.AreEqual(subjectDetail.Rating, response.Rating, "Incorrect rating.");
            Assert.AreEqual(subjectDetail.Comment, response.Comment, "Incorrect comment.");
            Assert.AreEqual(subjectDetail.Private, response.Private, "Incorrect privacy level.");
            Assert.IsTrue(HaveSameElements(subjectDetail.Tag, response.Tag), "Incorrect tags.");
            Assert.AreEqual(subjectDetail.EpStatus, response.EpStatus, "Incorrect episode status");
            Assert.AreEqual(subjectDetail.User.Id, response.User.Id, "Incorrect user");
        }

        [TestCleanup]
        public void Teardown()
        {
        }
    }
}
