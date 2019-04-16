using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Bangumi.Api.Core.Extensions;

namespace Bangumi.Api.Core.Test
{
    [TestClass]
    public class Extensions
    {
        [TestMethod]
        public void ReplacePathVariables()
        {
            string path = @"/collection/{subject_id}/{action}";
            string[] vars = { "9527", "wish" };
            string[] vars_longer = { "9527", "wish", "should_not_exist" };
            string[] vars_shorter = { "9527" };
            int jweq = 9527;

            Assert.AreEqual(string.Empty, string.Empty.ReplacePathVariables(vars));
            Assert.AreEqual(@"/collection/9527/wish", path.ReplacePathVariables(vars));
            Assert.AreEqual(@"/collection/9527/wish", path.ReplacePathVariables(vars_longer));
            Assert.AreEqual(@"/collection/9527/{action}", path.ReplacePathVariables(vars_shorter));
            Assert.AreEqual(@"/collection/9527/{action}", path.ReplacePathVariables(jweq));
        }
    }
}
