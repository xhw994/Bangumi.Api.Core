using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UserProgressResponse {
    /// <summary>
    /// 条目 ID
    /// </summary>
    /// <value>条目 ID</value>
    [DataMember(Name="subject_id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "subject_id")]
    public int? SubjectId { get; set; }

    /// <summary>
    /// 章节列表
    /// </summary>
    /// <value>章节列表</value>
    [DataMember(Name="eps", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eps")]
    public List<Episode> Eps { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserProgressResponse {\n");
      sb.Append("  SubjectId: ").Append(SubjectId).Append("\n");
      sb.Append("  Eps: ").Append(Eps).Append("\n");
      sb.Append("}\n");
      return sb.ToString();
    }

    /// <summary>
    /// Get the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson() {
      return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

}
}
