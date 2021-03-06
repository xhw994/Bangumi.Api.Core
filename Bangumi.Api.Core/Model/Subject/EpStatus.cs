using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Bangumi.Api.Core.Model.Subject
{
    /// <summary>
    /// 章节状态 &lt;br&gt; 2 &#x3D; watched &#x3D; 看过 &lt;br&gt; 1 &#x3D; queue &#x3D; 想看 &lt;br&gt; 3 &#x3D; drop &#x3D; 抛弃 &lt;br&gt; ? &#x3D; remove &#x3D; 撤销
    /// </summary>
    [DataContract]
    public enum EpStatus
    {
        [Description("watched")]
        Watched = 1, // 看过
        [Description("queue")]
        Queue = 2, //想看
        [Description("drop")]
        Drop = 3, // 抛弃
        [Description("remove")]
        Remove = 4, // 撤销
    }
}
