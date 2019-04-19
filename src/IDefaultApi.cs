using System;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IDefaultApi
    {
        /// <summary>
        /// 管理收藏 管理收藏
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="action">收藏动作 &lt;br&gt; create &#x3D; 添加收藏 &lt;br&gt; update &#x3D; 更新收藏 &lt;br&gt; 可以统一使用 &#x60;update&#x60;，系统会自动判断需要新建还是更新收藏</param>
        /// <param name="status">章节状态，参考 [EpStatusType](#model-EpStatusType)</param>
        /// <param name="comment">简评</param>
        /// <param name="tags">标签 &lt;br&gt; 以半角空格分割</param>
        /// <param name="rating">评分 &lt;br&gt; 1-10</param>
        /// <param name="privacy">收藏隐私 &lt;br&gt; 0 &#x3D; 公开 &lt;br&gt; 1 &#x3D; 私密</param>
        /// <returns>CollectionResponse</returns>
        CollectionResponse CollectionBySubjectIdAndActionPost(int? subjectId, string action, string status, string comment, string tags, int? rating, string privacy);

        /// <summary>
        /// 更新收视进度 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param>
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param>
        /// <returns>StatusCode</returns>
        StatusCode EpStatusByIdAndStatusGet(int? id, string status);

        /// <summary>
        /// 更新收视进度 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param>
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param>
        /// <param name="epId">使用 POST 批量更新 &lt;br&gt; 将章节以半角逗号分隔，如 &#x60;3697,3698,3699&#x60;。请求时 URL 中的 ep_id 为最后一个章节 ID</param>
        /// <returns>StatusCode</returns>
        StatusCode EpStatusByIdAndStatusPost(int? id, string status, string epId);

        /// <summary>
        /// 批量更新收视进度 批量更新收视进度
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="watchedEps">如看到 123 话则 POST &#x60;123&#x60; &lt;br&gt; 书籍条目传 watched_eps 与 watched_vols 至少其一</param>
        /// <param name="watchedVols">如看到第 3 卷则 POST &#x60;3&#x60;, 仅对书籍条目有效</param>
        /// <returns>StatusCode</returns>
        StatusCode SubjectUpdateWatchedEpsBySubjectIdPost(int? subjectId, string watchedEps, string watchedVols);        

        /// <summary>
        /// 用户收视进度 用户收视进度
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param>
        /// <param name="subjectId">条目 ID &lt;br&gt; 获取指定条目收视进度</param>
        /// <returns>List&lt;UserProgressResponse&gt;</returns>
        List<UserProgressResponse> UserProgressByUsernameGet(string username, int? subjectId);
    }
}
