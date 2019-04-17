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
    /// Medium-sized subject response
    /// </summary>
    [DataContract]
    public class SubjectMedium : SubjectSmall
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        /// <value>角色信息</value>
        [DataMember(Name = "crt", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "crt")]
        public List<Crt> Crt { get; set; }

        /// <summary>
        /// 制作人员信息
        /// </summary>
        /// <value>制作人员信息</value>
        [DataMember(Name = "staff", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "staff")]
        public List<Staff> Staff { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class SubjectMedium {\n");
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
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
