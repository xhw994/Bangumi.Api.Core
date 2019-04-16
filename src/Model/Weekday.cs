using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Weekday {
    /// <summary>
    /// Gets or Sets En
    /// </summary>
    [DataMember(Name="en", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "en")]
    public string En { get; set; }

    /// <summary>
    /// Gets or Sets Cn
    /// </summary>
    [DataMember(Name="cn", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "cn")]
    public string Cn { get; set; }

    /// <summary>
    /// Gets or Sets Ja
    /// </summary>
    [DataMember(Name="ja", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "ja")]
    public string Ja { get; set; }

    /// <summary>
    /// Gets or Sets Id
    /// </summary>
    [DataMember(Name="id", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "id")]
    public int? Id { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Weekday {\n");
      sb.Append("  En: ").Append(En).Append("\n");
      sb.Append("  Cn: ").Append(Cn).Append("\n");
      sb.Append("  Ja: ").Append(Ja).Append("\n");
      sb.Append("  Id: ").Append(Id).Append("\n");
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
