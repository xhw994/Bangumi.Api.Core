using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// 用户 id
        /// </summary>
        /// <value>用户 id</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// 用户主页地址
        /// </summary>
        /// <value>用户主页地址</value>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <value>用户名</value>
        [DataMember(Name = "username", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        /// <value>昵称</value>
        [DataMember(Name = "nickname", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像组
        /// </summary>
        /// <value>头像组</value>
        [DataMember(Name = "avatar", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "avatar")]
        public Avatar Avatar { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        /// <value>签名</value>
        [DataMember(Name = "sign", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }

        /// <summary>
        /// Gets or Sets Usergroup
        /// </summary>
        [DataMember(Name = "usergroup", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "usergroup")]
        public UserGroup Usergroup { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class User {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Username: ").Append(Username).Append("\n");
            sb.Append("  Nickname: ").Append(Nickname).Append("\n");
            sb.Append("  Avatar: ").Append(Avatar).Append("\n");
            sb.Append("  Sign: ").Append(Sign).Append("\n"); // Has correct value but does not display correctly in CMD?
            sb.Append("  Usergroup: ").Append((int)Usergroup).Append("\n");
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
