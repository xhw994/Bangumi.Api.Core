using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 评分
  /// </summary>
  [DataContract]
  public class Rating {
    /// <summary>
    /// 总评分人数
    /// </summary>
    /// <value>总评分人数</value>
    [DataMember(Name="total", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "total")]
    public int? Total { get; set; }

    /// <summary>
    /// 各分值评分人数
    /// </summary>
    /// <value>各分值评分人数</value>
    [DataMember(Name="count", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "count")]
    public Dictionary<string, int?> Count { get; set; }

    /// <summary>
    /// 评分
    /// </summary>
    /// <value>评分</value>
    [DataMember(Name="score", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "score")]
    public double? Score { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Rating {\n");
      sb.Append("  Total: ").Append(Total).Append("\n");
      sb.Append("  Count: ").Append(Count).Append("\n");
      sb.Append("  Score: ").Append(Score).Append("\n");
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
