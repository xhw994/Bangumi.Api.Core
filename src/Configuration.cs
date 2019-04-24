using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bangumi.Api.Core
{
    public static class Configuration
    {
        private readonly static IConfiguration _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();


        public static string AppId { get => _config["Bangumi:AppId"]; }
        public static string AppSecret { get => _config["Bangumi:AppSecret"]; }
        public static string CallbackUrl { get => _config["Bangumi:CallbackUrl"]; }
        public static string AppUrl { get => _config["Bangumi:AppUrl"]; }

        public static string ApiBaseUrl { get => _config["Api:BaseUrl"]; }
        public static string AuthCodeUrl { get => _config["Api:AuthCodeUrl"]; }
        public static string TokenUrl { get => _config["Api:TokenUrl"]; }
    }
}
