using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.User;
using Bangumi.Api.Core.Model.Subject;

namespace Bangumi.Api.Core.Model
{
    /// <summary>
    /// 用户收藏概览
    /// </summary>
    [DataContract]
    public class CollectionsByType
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "type")]
        public SubjectType Type { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 条目类型中文名
        /// </summary>
        /// <value>条目类型中文名</value>
        [DataMember(Name = "name_cn", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "name_cn")]
        public string NameCn { get; set; }

        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <value>收藏列表</value>
        [DataMember(Name = "collects", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "collects")]
        public IEnumerable<Collect> Collects { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            string s = $"class {nameof(CollectionsByType)} {{" + Environment.NewLine;
            s += $"  {nameof(Type)}: {Type}" + Environment.NewLine;
            s += $"  {nameof(Name)}: {Name}" + Environment.NewLine;
            s += $"  {nameof(NameCn)}: {NameCn}" + Environment.NewLine;
            s += $"  {nameof(Collects)}: [" + Environment.NewLine;
            foreach (Collect cl in Collects)
            {
                s += "  " + cl;
            }
            s += "]" + Environment.NewLine + "}" + Environment.NewLine;
            return s;
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
