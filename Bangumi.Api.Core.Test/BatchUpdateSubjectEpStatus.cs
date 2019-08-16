using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class BatchUpdateSubjectEpStatus
    {
        private const int drem = 31808;
        private const int dremBeforeVol = 19;
        private const int dremBeforeEp = 0;
        private const int dremAfterVol = 24;
        private const int dremAfterEp = 5;
        private DefaultBangumiService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void BatchUpdateAsWatched()
        {
            StatusCodeInfo _response = _service.BatchUpdateSubjectEpStatus(drem, dremAfterEp, dremAfterVol);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to watched.");
        }

        [TestCleanup]
        public void Teardown()
        {
            StatusCodeInfo _response = _service.BatchUpdateSubjectEpStatus(drem, dremBeforeEp, dremBeforeVol);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to remove.");
        }
    }
}