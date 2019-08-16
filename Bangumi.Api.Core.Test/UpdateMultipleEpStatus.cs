using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class UpdateMultipleEpStatus
    {
        private readonly int[] _cbEp234 = new int[] { 7027, 7028, 7029 };
        private DefaultBangumiService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void SetMultipleEpisodeAsWatched()
        {
            StatusCodeInfo _response = _service.UpdateMultipleEpStatus(_cbEp234, EpStatus.Watched);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to watched.");
        }

        [TestCleanup]
        public void Teardown()
        {
            StatusCodeInfo _response = _service.UpdateMultipleEpStatus(_cbEp234, EpStatus.Watched);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to remove.");
        }
    }
}
