using System.ComponentModel;
using System.Runtime.Serialization;

namespace Bangumi.Api.Core.Model.User
{
    /// <summary>
    /// 收藏状态 &lt;br&gt; 1 &#x3D; wish &#x3D; 想做 &lt;br&gt; 2 &#x3D; collect &#x3D; 做过 &lt;br&gt; 3 &#x3D; do &#x3D; 在做 &lt;br&gt; 4 &#x3D; on_hold &#x3D; 搁置 &lt;br&gt; 5 &#x3D; dropped &#x3D; 抛弃
    /// </summary>
    [DataContract]
    public enum CollectionStatus
    {
        [Description("wish")] // 想做
        Wish = 1,
        [Description("collect")] //做过
        Collect = 2,
        [Description("do")] // 在做
        Do = 3,
        [Description("on_hold")] // 搁置
        OnHold = 4,
        [Description("dropped")] // 抛弃
        Dropped = 5
    }
}
