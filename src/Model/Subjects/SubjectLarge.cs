using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Definition;

namespace Bangumi.Api.Core.Model.Subjects
{
    /// <summary>
    /// Large size subject response
    /// </summary>
    [DataContract]
    public class SubjectLarge : SubjectMedium
    {
        /// <summary>
        /// 讨论版
        /// </summary>
        /// <value>讨论版</value>
        [DataMember(Name = "topic", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "topic")]
        public List<Topic> Topic { get; set; }

        /// <summary>
        /// 评论日志
        /// </summary>
        /// <value>评论日志</value>
        [DataMember(Name = "blog", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "blog")]
        public List<Blog> Blog { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubjectLarge {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  NameCn: ").Append(NameCn).Append("\n");
            sb.Append("  Summary: ").Append(Summary).Append("\n");
            sb.Append("  AirDate: ").Append(AirDate).Append("\n");
            sb.Append("  AirWeekday: ").Append(AirWeekday).Append("\n");
            sb.Append("  Images: ").Append(Images).Append("\n");
            sb.Append("  Eps: ").Append(Eps).Append("\n");
            sb.Append("  EpsCount: ").Append(EpsCount).Append("\n");
            sb.Append("  Rating: ").Append(Rating).Append("\n");
            sb.Append("  Rank: ").Append(Rank).Append("\n");
            sb.Append("  Collection: ").Append(Collection).Append("\n");
            sb.Append("  Crt: ").Append(Crt).Append("\n");
            sb.Append("  Staff: ").Append(Staff).Append("\n");
            sb.Append("  Topic: ").Append(Topic).Append("\n");
            sb.Append("  Blog: ").Append(Blog).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
