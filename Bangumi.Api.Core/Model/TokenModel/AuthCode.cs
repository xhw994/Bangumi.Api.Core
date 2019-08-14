using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bangumi.Api.Core.Model.TokenModel
{
    [DataContract]
    public class AuthCode
    {
        /// <summary>
        /// Gets or Sets RequestTime
        /// </summary>
        [DataMember(Name = "request_time", EmitDefaultValue = false)]
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// Gets or Sets ReceiveTime
        /// </summary>
        [DataMember(Name = "receive_time", EmitDefaultValue = false)]
        public DateTime ReceiveTime { get; set; }
        /// <summary>
        /// Gets or Sets Code
        /// </summary>
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public string Code { get; set; }

        public bool Expired { get => string.IsNullOrEmpty(Code) || ReceiveTime + TimeSpan.FromMinutes(1) < DateTime.Now; }
    }
}
