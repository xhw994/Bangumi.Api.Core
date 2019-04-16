using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Definitions;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class SubjectSmall {
    /// <summary>
    /// 条目 ID
    /// </summary>
    /// <value>条目 ID</value>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public int? Id { get; set; }

    /// <summary>
    /// 条目地址
    /// </summary>
    /// <value>条目地址</value>
    [DataMember(Name="url", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }

    /// <summary>
    /// Gets or Sets Type
    /// </summary>
    [DataMember(Name="type", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "type")]
    public SubjectType Type { get; set; }

    /// <summary>
    /// 条目名称
    /// </summary>
    /// <value>条目名称</value>
    [DataMember(Name="name", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// 条目中文名称
    /// </summary>
    /// <value>条目中文名称</value>
    [DataMember(Name="name_cn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "name_cn")]
    public string NameCn { get; set; }

    /// <summary>
    /// 剧情简介
    /// </summary>
    /// <value>剧情简介</value>
    [DataMember(Name="summary", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "summary")]
    public string Summary { get; set; }

    /// <summary>
    /// 放送开始日期
    /// </summary>
    /// <value>放送开始日期</value>
    [DataMember(Name="air_date", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "air_date")]
    public string AirDate { get; set; }

    /// <summary>
    /// 放送星期
    /// </summary>
    /// <value>放送星期</value>
    [DataMember(Name="air_weekday", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "air_weekday")]
    public int? AirWeekday { get; set; }

    /// <summary>
    /// Gets or Sets Images
    /// </summary>
    [DataMember(Name="images", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "images")]
    public Images4 Images { get; set; }

    /// <summary>
    /// 话数
    /// </summary>
    /// <value>话数</value>
    [DataMember(Name="eps", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eps")]
    public int? Eps { get; set; }

    /// <summary>
    /// 话数
    /// </summary>
    /// <value>话数</value>
    [DataMember(Name="eps_count", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "eps_count")]
    public int? EpsCount { get; set; }

    /// <summary>
    /// Gets or Sets Rating
    /// </summary>
    [DataMember(Name="rating", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "rating")]
    public Rating Rating { get; set; }

    /// <summary>
    /// 排名
    /// </summary>
    /// <value>排名</value>
    [DataMember(Name="rank", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "rank")]
    public int? Rank { get; set; }

    /// <summary>
    /// Gets or Sets Collection
    /// </summary>
    [DataMember(Name="collection", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "collection")]
    public SubjectCollection Collection { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
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
      sb.Append("  Rating: ").Append(Rating).Append("\n");
      sb.Append("  Rank: ").Append(Rank).Append("\n");
      sb.Append("  Collection: ").Append(Collection).Append("\n");
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
