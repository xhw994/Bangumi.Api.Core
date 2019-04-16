using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model
{
    /// <summary>
    /// 日志
    /// </summary>
    [DataContract]
    public class Blog
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <value>ID</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        /// <value>地址</value>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        /// <value>标题</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// 概览
        /// </summary>
        /// <value>概览</value>
        [DataMember(Name = "summary", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        /// <value>图片</value>
        [DataMember(Name = "image", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        /// <value>回复数</value>
        [DataMember(Name = "replies", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "replies")]
        public int? Replies { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        /// <value>发布时间</value>
        [DataMember(Name = "timestamp", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "timestamp")]
        public int? Timestamp { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        /// <value>发布时间</value>
        [DataMember(Name = "dateline", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "dateline")]
        public string Dateline { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Blog {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Url: ").Append(Url).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  Summary: ").Append(Summary).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Replies: ").Append(Replies).Append("\n");
            sb.Append("  Timestamp: ").Append(Timestamp).Append("\n");
            sb.Append("  Dateline: ").Append(Dateline).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
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
