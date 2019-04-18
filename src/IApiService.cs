using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;
using Bangumi.Api.Core.Model.Users;

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
        SubjectBase GetSubject(int id, ResponseGroup group);

        /// <summary>
        /// 章节数据 章节数据
        /// </summary>
        /// <param name="subjectId">条目 ID</param> 
        /// <returns>SubjectEpResponse</returns>            
        SubjectEp GetSubjectEps(int id);

        /// <summary>
        /// 条目搜索 条目搜索
        /// </summary>
        /// <param name="keywords">关键词 &lt;br&gt; 需要 URL Encode</param>
        /// <param name="type">条目类型，参考 [SubjectType](#model-SubjectType)</param>
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param>
        /// <param name="start">开始条数</param>
        /// <param name="maxResults">每页条数 &lt;br&gt; 最多 25</param>
        /// <returns>SearchSubjectResponse</returns>
        SearchSubjectResponse SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup group, int? start, int? maxResults);

        /// <summary>
        /// 用户信息 用户信息
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param>
        /// <returns>User</returns>
        User GetUser(string username);
    }
}
