using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Model.Subjects
{

    /// <summary>
    /// 章节类型 &lt;br&gt; 0 &#x3D; 本篇 &lt;br&gt; 1 &#x3D; 特别篇 &lt;br&gt; 2 &#x3D; OP &lt;br&gt; 3 &#x3D; ED &lt;br&gt; 4 &#x3D; 预告/宣传/广告 &lt;br&gt; 5 &#x3D; MAD &lt;br&gt; 6 &#x3D; 其他
    /// </summary>
    [DataContract]
    public enum EpisodeType
    {
        Main = 0, // 本篇 
        Special = 1, // 特别篇 
        Opening = 2, // OP 
        Ending = 3, // ED 
        Commercial = 4, // 预告/宣传/广告 
        Mad = 5, // MAD <-怎么还有这玩意儿 
        Misc = 6 // 其他
    }
}
