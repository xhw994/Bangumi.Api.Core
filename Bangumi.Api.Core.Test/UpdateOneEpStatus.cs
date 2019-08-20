using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class UpdateOneEpStatus
    {
        private const int _cbEp1 = 519;
        private DefaultBangumiService _service;
        
        [TestInitialize]
        public void Setup()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void SetOneEpisodeAsWatched()
        {
            StatusCodeInfo _response = _service.UpdateOneEpStatus(_cbEp1, EpStatus.Watched);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to watched.");
        }
         
        [TestCleanup]
        public void Teardown()
        {
            StatusCodeInfo _response = _service.UpdateOneEpStatus(_cbEp1, EpStatus.Remove);
            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized.");
            Assert.AreEqual(StatusCode.OK, _response.Code, "Failed to update status to remove.");
        }
    }
}
