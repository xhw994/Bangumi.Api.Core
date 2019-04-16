using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 封面
  /// </summary>
  [DataContract]
  public class Images4 {
    /// <summary>
    /// Gets or Sets Large
    /// </summary>
    [DataMember(Name="large", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "large")]
    public string Large { get; set; }

    /// <summary>
    /// Gets or Sets Common
    /// </summary>
    [DataMember(Name="common", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "common")]
    public string Common { get; set; }

    /// <summary>
    /// Gets or Sets Medium
    /// </summary>
    [DataMember(Name="medium", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "medium")]
    public string Medium { get; set; }

    /// <summary>
    /// Gets or Sets Small
    /// </summary>
    [DataMember(Name="small", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "small")]
    public string Small { get; set; }

    /// <summary>
    /// Gets or Sets Grid
    /// </summary>
    [DataMember(Name="grid", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "grid")]
    public string Grid { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Images4 {\n");
      sb.Append("  Large: ").Append(Large).Append("\n");
      sb.Append("  Common: ").Append(Common).Append("\n");
      sb.Append("  Medium: ").Append(Medium).Append("\n");
      sb.Append("  Small: ").Append(Small).Append("\n");
      sb.Append("  Grid: ").Append(Grid).Append("\n");
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
