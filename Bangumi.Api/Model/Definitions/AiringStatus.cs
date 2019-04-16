using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Model.Definitions
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
