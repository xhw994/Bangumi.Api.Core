using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Bangumi.Api.Core.Model.Users;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 
  /// </summary>
  [DataContract]
  public class Collect {
    /// <summary>
    /// Gets or Sets Status
    /// </summary>
    [DataMember(Name="status", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "status")]
    public CollectionStatus Status { get; set; }

    /// <summary>
    /// Gets or Sets Count
    /// </summary>
    [DataMember(Name="count", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "count")]
    public int? Count { get; set; }

    /// <summary>
    /// Gets or Sets List
    /// </summary>
    [DataMember(Name="list", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "list")]
    public List<SubjectBase> List { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Collect {\n");
      sb.Append("  Status: ").Append(Status).Append("\n");
      sb.Append("  Count: ").Append(Count).Append("\n");
      sb.Append("  List: ").Append(List).Append("\n");
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
