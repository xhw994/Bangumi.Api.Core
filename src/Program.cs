using System;
using System.Collections.Generic;
using System.Web;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;
using Bangumi.Api.Core.Model.Users;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultBangumiService _service = new DefaultBangumiService();
            Configuration config = new Configuration();

            string username = "renkomei";
            _service.Authenticate(config.AppId, config.AppSecret);
            var res = _service.GetUserProgress(username, 253);
            foreach (var r in res) Console.WriteLine(r);
        }
    }
}
