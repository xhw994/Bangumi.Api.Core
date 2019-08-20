using Bangumi.Api.Core.Model.User;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bangumi.Api.Core.Model
{
    /// <summary>
    /// 条目收藏信息
    /// </summary>
    [DataContract]
    public class CollectionResponse
    {
        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "status")]
        public CollectionStatusInfo Status { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        /// <value>评分</value>
        [DataMember(Name = "rating", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        /// <value>评论</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or Sets Private
        /// </summary>
        [DataMember(Name = "private", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "private")]
        public Privacy Private { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        /// <value>标签</value>
        [DataMember(Name = "tag", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "tag")]
        public IEnumerable<string> Tag { get; set; }

        /// <summary>
        /// 完成话数
        /// </summary>
        /// <value>完成话数</value>
        [DataMember(Name = "ep_status", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "ep_status")]
        public int EpStatus { get; set; }

        /// <summary>
        /// 上次更新时间
        /// </summary>
        /// <value>上次更新时间</value>
        [DataMember(Name = "lasttouch", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "lasttouch")]
        public int Lasttouch { get; set; }

        /// <summary>
        /// Gets or Sets User
        /// </summary>
        [DataMember(Name = "user", EmitDefaultValue = false)]
        [JsonProperty(PropertyName = "user")]
        public User.User User { get; set; }


        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>string presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CollectionResponse {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Rating: ").Append(Rating).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  Private: ").Append(Private).Append("\n");
            sb.Append("  Tag: ").Append(Tag).Append("\n");
            sb.Append("  EpStatus: ").Append(EpStatus).Append("\n");
            sb.Append("  Lasttouch: ").Append(Lasttouch).Append("\n");
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
