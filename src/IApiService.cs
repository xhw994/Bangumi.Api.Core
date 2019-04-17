using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core
{
    public interface IApiService
    {
        /// <summary>
        /// 每日放送 每日放送
        /// </summary>
        /// <returns><see cref="IEnumerable{CalendarResponse}"/></returns>
        IEnumerable<CalendarResponse> GetDailyCalendar();

        /// <summary>
        /// 条目信息 条目信息
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param>
        /// <returns>object</returns>
        SubjectResponseBase GetSubject(int id, ResponseGroup group);
    }
}
