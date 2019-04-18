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

            string username = "renkomei";

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ApiException(400, "Missing required parameter 'username' when calling GetUser");
            }

            string path = $"/user/{username}";

            BangumiRequest request = new BangumiRequest(path);
            var res = _client.Request<User>(request);
            Console.WriteLine(res);
        }
    }
}
