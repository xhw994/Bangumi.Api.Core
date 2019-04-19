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
            BangumiClient _client = new BangumiClient();
            DefaultBangumiService _service = new DefaultBangumiService();

            string username = "renkomei";

            var res = _service.GetUserCollection(username, false, "1,2,3,4", ResponseGroup.Medium);
            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }
    }
}
