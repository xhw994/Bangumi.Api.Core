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
    /// 状态码 &lt;br&gt; 200 OK &lt;br&gt; 202 Accepted &lt;br&gt; 304 Not Modified &lt;br&gt; 30401 Not Modified: Collection already exists &lt;br&gt; 400 Bad Request &lt;br&gt; 40001 Error: Nothing found with that ID &lt;br&gt; 401 Unauthorized &lt;br&gt; 40101 Error: Auth failed over 5 times &lt;br&gt; 40102 Error: Username is not an Email address &lt;br&gt; 405 Method Not Allowed &lt;br&gt; 404 Not Found
    /// </summary>
    [DataContract]
    public enum Code
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