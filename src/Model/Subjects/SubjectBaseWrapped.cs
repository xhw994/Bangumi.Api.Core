using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bangumi.Api.Core.Model.Subjects
{
    [DataContract]
    public class SubjectBaseWrapped
    {
        [DataMember(Name = "subject_id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subject_id")]
        public int? SubjectId { get; set; }

        [DataMember(Name = "subject", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "subject")]
        public SubjectBase Subject { get; set; }

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            string s = $"class {nameof(SubjectBaseWrapped)}" + Environment.NewLine;
            s += $"  {nameof(SubjectId)}: {SubjectId}" + Environment.NewLine;
            s += $"  {nameof(SubjectBase)}: {Subject}" + Environment.NewLine;
            s += "}" + Environment.NewLine;
            return s;
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
