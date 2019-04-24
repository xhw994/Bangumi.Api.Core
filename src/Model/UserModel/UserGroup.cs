using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bangumi.Api.Core.Model.UserModel
{
    /// <summary>
    /// 用户组
    /// </summary>
    public enum UserGroup
    {
        [Description("superadmin")] // 管理员
        SuperAdmin = 1,
        [Description("bangumiAdmin")] // Bangumi管理猿
        BangumiAdmin = 2,
        [Description("tianChuangAdmin")] // 天窗管理猿
        TianChuangAdmin = 3,
        [Description("silenced")] // 禁言用户
        Silenced = 4,
        [Description("prohibited")] // 禁止访问用户
        Prohibited = 5,
        [Description("monoAdmin")] // 人物管理猿
        MonoAdmin = 8,
        [Description("wikiAdmin")] // 维基条目管理猿
        WikiAdmin = 9,
        [Description("user")] // 用户
        User = 10,
        [Description("wiki")] // 维基人
        Wiki = 11
    }
}
