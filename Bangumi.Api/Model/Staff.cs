using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Staff {
    /// <summary>
    /// 人物 ID
    /// </summary>
    /// <value>人物 ID</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public int? Id { get; set; }

    /// <summary>
    /// 人物地址
    /// </summary>
    /// <value>人物地址</value>
    [DataMember(Name="url", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    /// <value>姓名</value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets Images
    /// </summary>
    [DataMember(Name="images", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "images")]
    public Images Images { get; set; }

    /// <summary>
    /// 简体中文名
    /// </summary>
    /// <value>简体中文名</value>
    [DataMember(Name="name_cn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name_cn")]
    public string NameCn { get; set; }

    /// <summary>
    /// 回复数量
    /// </summary>
    /// <value>回复数量</value>
    [DataMember(Name="comment", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "comment")]
    public int? Comment { get; set; }

    /// <summary>
    /// 收藏人数
    /// </summary>
    /// <value>收藏人数</value>
    [DataMember(Name="collects", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "collects")]
    public int? Collects { get; set; }

    /// <summary>
    /// Gets or Sets Info
    /// </summary>
    [DataMember(Name="info", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "info")]
    public MonoInfo Info { get; set; }

    /// <summary>
    /// 人物类型
    /// </summary>
    /// <value>人物类型</value>
    [DataMember(Name="role_name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "role_name")]
    public string RoleName { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    /// <value>职位</value>
    [DataMember(Name="jobs", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "jobs")]
    public List<string> Jobs { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Staff {\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
      sb.Append("  Url: ").Append(Url).Append("\n");
      sb.Append("  Name: ").Append(Name).Append("\n");
      sb.Append("  Images: ").Append(Images).Append("\n");
      sb.Append("  NameCn: ").Append(NameCn).Append("\n");
      sb.Append("  Comment: ").Append(Comment).Append("\n");
      sb.Append("  Collects: ").Append(Collects).Append("\n");
      sb.Append("  Info: ").Append(Info).Append("\n");
      sb.Append("  RoleName: ").Append(RoleName).Append("\n");
      sb.Append("  Jobs: ").Append(Jobs).Append("\n");
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
