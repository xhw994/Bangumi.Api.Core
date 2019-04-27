using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.UserModel;
using Bangumi.Api.Core.Model.SubjectModel;

namespace Bangumi.Api.Core.Model.SubjectModel
{
    /// <summary>
    /// Subject response for episodes
    /// </summary>
    [DataContract]
    public class SubjectEp: SubjectBase
    {
        /// <summary>
        /// 各集信息
        /// </summary>
        /// <value>各集信息</value>
        [DataMember(Name = "eps", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "eps")]
        new public List<Episode> Eps { get; set; }

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
            sb.Append("}\n");
            return sb.ToString();
        }
    }
}
