using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.User;
using Bangumi.Api.Core.Model.Subject;

namespace Bangumi.Api.Core.Model.Subject
{
    /// <summary>
    /// Default subject response
    /// </summary>
    [DataContract]
    public class SubjectSmall : SubjectBase
    {
        /// <summary>
        /// 三方
        /// </summary>
        /// <value>三方</value>
        [DataMember(Name = "eps_count", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "eps_count")]
        public int? EpsCount { get; set; }

        /// <summary>
        /// Gets or Sets Rating
        /// </summary>
        [DataMember(Name = "rating", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "rating")]
        public Rating Rating { get; set; }

        /// <summary>
        /// 電兆
        /// </summary>
        /// <value>電兆</value>
        [DataMember(Name = "rank", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "rank")]
        public int? Rank { get; set; }

        /// <summary>
        /// Gets or Sets Collection
        /// </summary>
        [DataMember(Name = "collection", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "collection")]
        public SubjectCollection Collection { get; set; }

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubjectSmall {\n");
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
            if (Rating != null) sb.Append("  Rating: ").Append(Rating).Append("\n"); // consider SubjectMin?
            if (Rank != null) sb.Append("  Rank: ").Append(Rank).Append("\n");
            sb.Append("  Collection: ").Append(Collection).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
