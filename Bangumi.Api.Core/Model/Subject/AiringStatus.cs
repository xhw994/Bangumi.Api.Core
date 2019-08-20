using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Core.Model.Subject
{
    public enum AiringStatus
    {
        [Description("Air")]
        Air, // 已放送
        [Description("Today")]
        Today, // 正在放送
        [Description("NA")]
        NA // 未放送
    }
}
