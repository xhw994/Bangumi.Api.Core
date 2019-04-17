using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model
{

    /// <summary>
    /// 收藏人数
    /// </summary>
    [DataContract]
    public class SubjectCollection
    {
        /// <summary>
        /// 想做
        /// </summary>
        /// <value>想做</value>
        [DataMember(Name = "wish", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "wish")]
        public int? Wish { get; set; }

        /// <summary>
        /// 做过
        /// </summary>
        /// <value>做过</value>
        [DataMember(Name = "collect", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "collect")]
        public int? Collect { get; set; }

        /// <summary>
        /// 在做
        /// </summary>
        /// <value>在做</value>
        [DataMember(Name = "doing", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "doing")]
        public int? Doing { get; set; }

        /// <summary>
        /// 搁置
        /// </summary>
        /// <value>搁置</value>
        [DataMember(Name = "on_hold", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "on_hold")]
        public int? OnHold { get; set; }

        /// <summary>
        /// 抛弃
        /// </summary>
        /// <value>抛弃</value>
        [DataMember(Name = "dropped", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dropped")]
        public int? Dropped { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubjectCollection {\n");
            sb.Append("  Wish: ").Append(Wish).Append("\n");
            sb.Append("  Collect: ").Append(Collect).Append("\n");
            sb.Append("  Doing: ").Append(Doing).Append("\n");
            sb.Append("  OnHold: ").Append(OnHold).Append("\n");
            sb.Append("  Dropped: ").Append(Dropped).Append("\n");
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
