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
using RestSharp;
using Bangumi.Api.Core.Client;

namespace Bangumi.Api.Core.Test.API
{
    [TestClass]
    public class UpdateOneEpStatus
    {
        private DefaultBangumiService _service;
        private StatusCodeInfo _response;

        [TestInitialize]
        public void InitAndAuthorize()
        {
            _service = new DefaultBangumiService();
        }

        [TestMethod]
        public void UpdateAnime()
        {
            int gintamaEp1 = 595;
            _response = _service.UpdateOneEpStatus(gintamaEp1, EpStatus.Watched);
            Console.Write(_response);

            Assert.AreNotEqual(StatusCode.Unauthorized, _response.Code, "The client was unauthorized ");
        }

        [TestMethod]
        public void ValidateField()
        {
            
        }
    }
}
