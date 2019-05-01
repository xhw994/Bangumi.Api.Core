using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model
{
    /// <summary>
    /// 响应状态（HTTP 状态码都为 200）
    /// </summary>
    [DataContract]
    public class StatusCode
    {
        /// <summary>
        /// 当前请求的地址
        /// </summary>
        /// <value>当前请求的地址</value>
        [DataMember(Name = "request", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "request")]
        public string Request { get; set; }

        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "code")]
        public Code Code { get; set; }

        /// <summary>
        /// 状态信息
        /// </summary>
        /// <value>状态信息</value>
        [DataMember(Name = "error", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StatusCode {\n");
            sb.Append("  Request: ").Append(Request).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
            sb.Append("  Error: ").Append(Error).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
