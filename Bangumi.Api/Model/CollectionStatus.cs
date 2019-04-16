using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Model
{

    /// <summary>
    /// 收藏状态 &lt;br&gt; 1 &#x3D; wish &#x3D; 想做 &lt;br&gt; 2 &#x3D; collect &#x3D; 做过 &lt;br&gt; 3 &#x3D; do &#x3D; 在做 &lt;br&gt; 4 &#x3D; on_hold &#x3D; 搁置 &lt;br&gt; 5 &#x3D; dropped &#x3D; 抛弃
    /// </summary>
    [DataContract]
    public enum CollectionStatus
    {
        Wish = 1, // 想做
        Collect = 2, //做过
        Do = 3, // 在做
        On_Hold = 4, // 搁置
        Dropped = 5 // 抛弃
    }
}
