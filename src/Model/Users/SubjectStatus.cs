using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core.Model.Users
{
    /// <summary>
    /// 用户收藏
    /// </summary>
    [DataContract]
    public class SubjectStatus
    {
        /// <summary>
        /// 番剧标题
        /// </summary>
        /// <value>番剧标题</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 章节 ID
        /// </summary>
        /// <value>章节 ID</value>
        [DataMember(Name = "subject_id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subject_id")]
        public int? SubjectId { get; set; }

        /// <summary>
        /// 完成话数
        /// </summary>
        /// <value>完成话数</value>
        [DataMember(Name = "ep_status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ep_status")]
        public int? EpStatus { get; set; }

        /// <summary>
        /// 完成卷数（书籍）
        /// </summary>
        /// <value>完成卷数（书籍）</value>
        [DataMember(Name = "vol_status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "vol_status")]
        public int? VolStatus { get; set; }

        /// <summary>
        /// 上次更新时间
        /// </summary>
        /// <value>上次更新时间</value>
        [DataMember(Name = "lasttouch", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lasttouch")]
        public int? Lasttouch { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subject")]
        public SubjectSmall Subject { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserCollectionResponse {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  SubjectId: ").Append(SubjectId).Append("\n");
            sb.Append("  EpStatus: ").Append(EpStatus).Append("\n");
            sb.Append("  VolStatus: ").Append(VolStatus).Append("\n");
            sb.Append("  Lasttouch: ").Append(Lasttouch).Append("\n");
            if (Subject != null) sb.Append("  Subject: ").Append(Subject).Append("\n");
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
