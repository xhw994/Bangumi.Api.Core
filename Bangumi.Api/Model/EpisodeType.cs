using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 章节类型 &lt;br&gt; 0 &#x3D; 本篇 &lt;br&gt; 1 &#x3D; 特别篇 &lt;br&gt; 2 &#x3D; OP &lt;br&gt; 3 &#x3D; ED &lt;br&gt; 4 &#x3D; 预告/宣传/广告 &lt;br&gt; 5 &#x3D; MAD &lt;br&gt; 6 &#x3D; 其他
  /// </summary>
  [DataContract]
  public class EpisodeType {

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class EpisodeType {\n");
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
