using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Bangumi.Api.Core.Model
{
    /// <summary>
    /// 状态码
    /// </summary>
    [DataContract]
    public enum StatusCode
    {
        [Description("OK")]
        OK = 200,
        [Description("Accepted")]
        Accepted = 202,
        [Description("Not Modified")]
        NotModified = 304,
        [Description("Not Modified: Collection already exists")]
        NotModifiedCollectionExists = 30401,
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Error: Nothing found with that ID")]
        NoMatch = 40001,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Error: Auth failed over 5 times")]
        AuthFailed = 40101,
        [Description("Error: Username is not an Email address")]
        InvalidUserName = 40102,
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,
        [Description("Not Found")]
        NotFound = 404
    }
}