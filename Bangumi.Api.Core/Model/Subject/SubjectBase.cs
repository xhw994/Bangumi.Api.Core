using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.User;

namespace Bangumi.Api.Core.Model.Subject
{
    public class SubjectBase
    {
        /// <summary>
        /// 条目 ID
        /// </summary>
        /// <value>条目 ID</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// 条目地址
        /// </summary>
        /// <value>条目地址</value>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public SubjectType Type { get; set; }

        /// <summary>
        /// 条目名称
        /// </summary>
        /// <value>条目名称</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 条目中文名称
        /// </summary>
        /// <value>条目中文名称</value>
        [DataMember(Name = "name_cn", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name_cn")]
        public string NameCn { get; set; }

        /// <summary>
        /// 剧情简介
        /// </summary>
        /// <value>剧情简介</value>
        [DataMember(Name = "summary", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// 话数，此处值等同于eps_count。如果是<see cref="SubjectLarge"/>则为<see cref="List{Episode.Ep}"/>。
        /// </summary>
        /// <value>话数</value>
        [DataMember(Name = "eps", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "eps")]
        public int? Eps { get; set; }

        /// <summary>
        /// 放送开始日期
        /// </summary>
        /// <value>放送开始日期</value>
        [DataMember(Name = "air_date", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "air_date")]
        public string AirDate { get; set; }

        /// <summary>
        /// 放送星期
        /// </summary>
        /// <value>放送星期</value>
        [DataMember(Name = "air_weekday", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "air_weekday")]
        public int? AirWeekday { get; set; }

        /// <summary>
        /// Gets or Sets Images
        /// </summary>
        [DataMember(Name = "images", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "images")]
        public Images Images { get; set; }


        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
