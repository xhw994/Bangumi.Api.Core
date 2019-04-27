using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model {

  /// <summary>
  /// 别名（另外添加出来的 key 为 0 开始的数字）
  /// </summary>
  [DataContract]
  public class Alias {
    /// <summary>
    /// 日文名
    /// </summary>
    /// <value>日文名</value>
    [DataMember(Name="jp", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "jp")]
    public string Jp { get; set; }

    /// <summary>
    /// 纯假名
    /// </summary>
    /// <value>纯假名</value>
    [DataMember(Name="kana", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "kana")]
    public string Kana { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    /// <value>昵称</value>
    [DataMember(Name="nick", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "nick")]
    public string Nick { get; set; }

    /// <summary>
    /// 罗马字
    /// </summary>
    /// <value>罗马字</value>
    [DataMember(Name="romaji", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "romaji")]
    public string Romaji { get; set; }

    /// <summary>
    /// 第二中文名
    /// </summary>
    /// <value>第二中文名</value>
    [DataMember(Name="zh", EmitDefaultValue=false)]
    [JsonProperty(PropertyName = "zh")]
    public string Zh { get; set; }


    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Alias {\n");
      sb.Append("  Jp: ").Append(Jp).Append("\n");
      sb.Append("  Kana: ").Append(Kana).Append("\n");
      sb.Append("  Nick: ").Append(Nick).Append("\n");
      sb.Append("  Romaji: ").Append(Romaji).Append("\n");
      sb.Append("  Zh: ").Append(Zh).Append("\n");
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
