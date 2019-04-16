using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 人物信息
  /// </summary>
  [DataContract]
  public class MonoInfo {
    /// <summary>
    /// 生日
    /// </summary>
    /// <value>生日</value>
    [DataMember(Name="birth", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "birth")]
    public string Birth { get; set; }

    /// <summary>
    /// 身高
    /// </summary>
    /// <value>身高</value>
    [DataMember(Name="height", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "height")]
    public string Height { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    /// <value>性别</value>
    [DataMember(Name="gender", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "gender")]
    public string Gender { get; set; }

    /// <summary>
    /// Gets or Sets Alias
    /// </summary>
    [DataMember(Name="alias", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "alias")]
    public Alias Alias { get; set; }

    /// <summary>
    /// 引用来源
    /// </summary>
    /// <value>引用来源</value>
    [DataMember(Name="source", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "source")]
    public object Source { get; set; }

    /// <summary>
    /// 简体中文名
    /// </summary>
    /// <value>简体中文名</value>
    [DataMember(Name="name_cn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name_cn")]
    public string NameCn { get; set; }

    /// <summary>
    /// 声优
    /// </summary>
    /// <value>声优</value>
    [DataMember(Name="cv", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cv")]
    public string Cv { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class MonoInfo {\n");
      sb.Append("  Birth: ").Append(Birth).Append("\n");
      sb.Append("  Height: ").Append(Height).Append("\n");
      sb.Append("  Gender: ").Append(Gender).Append("\n");
      sb.Append("  Alias: ").Append(Alias).Append("\n");
      sb.Append("  Source: ").Append(Source).Append("\n");
      sb.Append("  NameCn: ").Append(NameCn).Append("\n");
      sb.Append("  Cv: ").Append(Cv).Append("\n");
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
