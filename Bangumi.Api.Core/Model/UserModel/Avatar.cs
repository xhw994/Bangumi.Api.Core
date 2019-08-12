using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model.UserModel
{
    /// <summary>
    /// 头像地址
    /// </summary>
    [DataContract]
    public class Avatar
    {
        /// <summary>
        /// 大号头像
        /// </summary>
        [DataMember(Name = "large", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "large")]
        public string Large { get; set; }

        /// <summary>
        /// 中号头像
        /// </summary>
        [DataMember(Name = "medium", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; set; }

        /// <summary>
        /// 小号头像
        /// </summary>
        [DataMember(Name = "small", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "small")]
        public string Small { get; set; }

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Avatar {\n");
            sb.Append("  Large: ").Append(Large).Append("\n");
            sb.Append("  Medium: ").Append(Medium).Append("\n");
            sb.Append("  Small: ").Append(Small).Append("\n");
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
