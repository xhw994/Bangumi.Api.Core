using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bangumi.Api.Core
{
    /// <summary>
    /// 包含从 appSettings.json 导入的全局变量
    /// </summary>
    public static class Configuration
    {
        private readonly static IConfiguration _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

        /// <summary>
        /// 注册应用时获取的ID
        /// </summary>
        public static string AppId { get => _config["Bangumi:AppId"]; }
        /// <summary>
        /// 注册应用时获取的密钥
        /// </summary>
        public static string AppSecret { get => _config["Bangumi:AppSecret"]; }
        /// <summary>
        /// 在后台设置的回调地址，用于OAuth认证
        /// </summary>
        public static string CallbackUrl { get => _config["Bangumi:CallbackUrl"]; }
        /// <summary>
        /// 在后台设置的应用主页地址
        /// </summary>
        public static string AppUrl { get => _config["Bangumi:AppUrl"]; }
        
        /// <summary>
        /// Bangumi API 基础地址
        /// </summary>
        public static string ApiBaseUrl { get => _config["Api:BaseUrl"]; }
        /// <summary>
        /// 请求 Code的地址
        /// </summary>
        public static string AuthCodeUrl { get => _config["Api:AuthCodeUrl"]; }
        /// <summary>
        /// 请求 Access Token 的地址
        /// </summary>
        public static string TokenUrl { get => _config["Api:TokenUrl"]; }
    }
}
