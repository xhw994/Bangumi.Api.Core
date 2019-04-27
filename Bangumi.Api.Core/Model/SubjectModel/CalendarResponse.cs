using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model.SubjectModel
{
    /// <summary>
    /// √ø»’∑≈ÀÕ
    /// </summary>
    [DataContract]
    public class CalendarResponse
    {
        /// <summary>
        /// Gets or Sets Weekday
        /// </summary>
        [DataMember(Name = "weekday", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "weekday")]
        public Weekday Weekday { get; set; }

        /// <summary>
        /// Gets or Sets Items
        /// </summary>
        [DataMember(Name = "items", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "items")]
        public List<SubjectSmall> Items { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CalendarResponse {\n");
            sb.Append("  Weekday: ").Append(Weekday).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
