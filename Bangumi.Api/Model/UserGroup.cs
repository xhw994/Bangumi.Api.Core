using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 用户组 &lt;br&gt; 1 &#x3D; 管理员 &lt;br&gt; 2 &#x3D; Bangumi 管理猿 &lt;br&gt; 3 &#x3D; 天窗管理猿 &lt;br&gt; 4 &#x3D; 禁言用户 &lt;br&gt; 5 &#x3D; 禁止访问用户 &lt;br&gt; 8 &#x3D; 人物管理猿 &lt;br&gt; 9 &#x3D; 维基条目管理猿 &lt;br&gt; 10 &#x3D; 用户 &lt;br&gt; 11 &#x3D; 维基人
  /// </summary>
  [DataContract]
  public class UserGroup {

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class UserGroup {\n");
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
