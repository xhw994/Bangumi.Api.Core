using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Definition;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class UserCollectionsResponse {
    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public SubjectType Type { get; set; }

    /// <summary>
    /// Gets or Sets Name
    /// </summary>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public SubjectTypeName Name { get; set; }

    /// <summary>
    /// 条目类型中文名
    /// </summary>
    /// <value>条目类型中文名</value>
    [DataMember(Name="name_cn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name_cn")]
    public string NameCn { get; set; }

    /// <summary>
    /// 收藏列表
    /// </summary>
    /// <value>收藏列表</value>
    [DataMember(Name="collects", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "collects")]
    public List<Collect> Collects { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserCollectionsResponse {\n");
      sb.Append("  Type: ").Append(Type).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  NameCn: ").Append(NameCn).Append("\n");
      sb.Append("  Collects: ").Append(Collects).Append("\n");
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
