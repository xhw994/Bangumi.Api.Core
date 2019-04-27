using System;
using System.Collections.Generic;
using System.Web;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Bangumi.Api.Core.Model.UserModel;

namespace Bangumi.Api.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            DefaultBangumiService _service = new DefaultBangumiService();
            //string callbackUrl = "http://174.1.60.140:5994/";
            BangumiAuthenticator authenticator = new BangumiAuthenticator();
            authenticator.RequestAuthCode();
            Console.WriteLine(authenticator.AuthCode);
        }
    }
}
