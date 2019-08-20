using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Extension;

namespace Bangumi.Api.Core.Model.Subject
{
    /// <summary>
    /// 条目搜索
    /// </summary>
    [DataContract]
    public class SubjectSearchResult
    {
        /// <summary>
        /// 总条数
        /// </summary>
        /// <value>总条数</value>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "results")]
        public int? Results { get; set; }

        /// <summary>
        /// 结果列表
        /// </summary>
        /// <value>结果列表</value>
        [DataMember(Name = "list", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "list")]
        public List<SubjectSmall> List { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            string s = "{" + Environment.NewLine;
            s += "Results: " + Results + Environment.NewLine;
            s += "List: [" + Environment.NewLine;
            s += string.Join("," + Environment.NewLine, List.Select(x => x.ToString())) + Environment.NewLine;
            s += "}";
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
