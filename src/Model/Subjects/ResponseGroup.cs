using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel;

namespace Bangumi.Api.Core.Model.Subjects
{
    /// <summary>
    /// 返回数据大小
    /// </summary>
    [DataContract]
    public enum ResponseGroup
    {
        [Description("small")]
        Small,
        [Description("medium")]
        Medium,
        [Description("large")]
        Large,
        [Description("ep")]
        Ep
    }
}
