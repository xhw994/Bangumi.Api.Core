using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model {

  /// <summary>
  /// 状态码 &lt;br&gt; 200 OK &lt;br&gt; 202 Accepted &lt;br&gt; 304 Not Modified &lt;br&gt; 30401 Not Modified: Collection already exists &lt;br&gt; 400 Bad Request &lt;br&gt; 40001 Error: Nothing found with that ID &lt;br&gt; 401 Unauthorized &lt;br&gt; 40101 Error: Auth failed over 5 times &lt;br&gt; 40102 Error: Username is not an Email address &lt;br&gt; 405 Method Not Allowed &lt;br&gt; 404 Not Found
  /// </summary>
  [DataContract]
  public class Code {

    /// <summary>
    /// Get the string presentation of the object
    /// </summary>
    /// <returns>string presentation of the object</returns>
    public override string ToString()  {
      var sb = new StringBuilder();
      sb.Append("class Code {\n");
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
