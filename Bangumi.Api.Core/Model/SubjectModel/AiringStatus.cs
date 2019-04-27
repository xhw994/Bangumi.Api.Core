using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Core.Model.SubjectModel
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
