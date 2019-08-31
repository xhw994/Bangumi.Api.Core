using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Model.User;
using System.Collections.Generic;

namespace Bangumi.Api.Core
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBangumiService
    {
        #region 用户 User
        /// <summary>
        /// 获取用户基础信息。
        /// </summary>
        /// <param name="username">用户名，也可使用 UID。此为必须参数。</param>
        /// <returns>用户信息。</returns>
        User GetUser(string username);

        /// <summary>
        /// 获取用户收藏列表。
        /// </summary>
        /// <param name="username">用户名，也可使用 UID。此为必须参数。</param>
        /// <param name="allWatching">收藏类型。如果值为<see langword="false"/>，返回在看的动画与三次元条目。如果为<see langword="true"/>，返回在看的动画三次元与书籍条目。默认为<see langword="false"/>。</param> 
        /// <param name="ids">收藏条目ID。如果批量查询收藏状态，将条目ID以半角逗号分隔，如 1,2,4,6。</param> 
        /// <param name="responseGroup">返回内容的格式。目前可以选择meduim或者small，默认为 medium。small时不返回条目详细信息。</param>
        /// <returns>用户收藏。</returns>
        IEnumerable<SubjectStatus> GetCollection(string username, bool allWatching = false, int[] ids = null, ResponseGroup responseGroup = ResponseGroup.Medium);

        /// <summary>
        /// 获取用户指定类型的收藏概览，固定返回最近更新的收藏，不支持翻页。
        /// </summary>
        /// <param name="username">用户名，也可使用 UID。此为必须参数。</param>
        /// <param name="subjectType">条目类型，详见<see cref="SubjectType"/>。此为必须参数。</param> 
        /// <param name="appId">申请到的App ID。此为必须参数。</param> 
        /// <param name="maxResults">显示条数。最多25条。</param> 
        /// <returns>用户收藏概览。</returns>            
        IEnumerable<CollectionsByType> GetCollectionsByType(string username, SubjectType subjectType, int? maxResults = null);

        /// <summary>
        /// 获取用户所有收藏信息。
        /// </summary>
        /// <param name="username">用户名，也可使用 UID。此为必须参数。</param>
        /// <param name="appId">申请到的App ID。此为必须参数。</param> 
        /// <returns>用户收藏统计。</returns>
        IEnumerable<CollectionsByType> GetCollectionsStatus(string username);

        /// <summary>
        /// 获取用户收视进度。
        /// </summary>
        /// <param name="username">用户名，也可使用 UID。此为必须参数。</param>
        /// <param name="subjectId">收藏条目ID。</param>
        /// <returns>用户收视进度。</returns>
        IEnumerable<UserProgress> GetProgress(string username, int subjectId);
        #endregion

        #region 条目 Subject
        /// <summary>
        /// 获取条目信息。
        /// </summary>
        /// <param name="subjectId">条目 ID。</param>
        /// <param name="responseGroup">返回数据大小，可选small、medium、large，默认为small。</param>
        /// <returns>条目信息。根据<paramref name="group"/>的值，返回<see cref="SubjectSmall"/>，<see cref="SubjectMedium"/>，或<see cref="SubjectLarge"/>。需要进行相应的类型转换。</returns>
        SubjectBase GetSubject(int subjectId, ResponseGroup responseGroup = ResponseGroup.Small);

        /// <summary>
        /// 获取带章节数据的条目信息。
        /// </summary>
        /// <param name="subjectId">条目 ID。</param> 
        /// <returns>带章节数据的条目信息。</returns>            
        SubjectEp GetSubjectAndEpisodes(int subjectId);

        /// <summary>
        /// 每日放送
        /// </summary>
        /// <returns>每日放送</returns>
        IEnumerable<CalendarResponse> GetDailyCalendar();
        #endregion

        #region 搜索
        /// <summary>
        /// 条目搜索。
        /// </summary>
        /// <param name="keywords">关键词，需要 URL Encode。</param>
        /// <param name="type">条目类型。参考<see cref="SubjectType"/>，如为<see langword="null"/>则返回所有类型。</param>
        /// <param name="responseGroup">返回数据大小。参考<see cref="ResponseGroup"/>，默认为small。</param>
        /// <param name="start">开始条数，默认为0（索引以0为起点）。</param>
        /// <param name="maxResults">每页条数。最多25，默认为10（某些情况可能小于10）。</param>
        /// <returns>搜索结果。</returns>
        SubjectSearchResult SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup responseGroup = ResponseGroup.Small, int start = 0, int maxResults = 10);
        #endregion

        /// <summary>
        /// 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param>
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param>
        /// <returns>StatusCode</returns>
        StatusCodeInfo UpdateOneEpStatus(int id, EpStatus status);

        /// <summary>
        /// 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param>
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param>
        /// <param name="epId">使用 POST 批量更新 &lt;br&gt; 将章节以半角逗号分隔，如 &#x60;3697,3698,3699&#x60;。请求时 URL 中的 ep_id 为最后一个章节 ID</param>
        /// <returns>StatusCode</returns>
        StatusCodeInfo UpdateMultipleEpStatus(int[] ids, EpStatus status);

        /// <summary>
        /// 批量更新收视进度
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="watchedEps">如看到 123 话则 POST &#x60;123&#x60; &lt;br&gt; 书籍条目传 watched_eps 与 watched_vols 至少其一</param>
        /// <param name="watchedVols">如看到第 3 卷则 POST &#x60;3&#x60;, 仅对书籍条目有效</param>
        /// <returns>StatusCode</returns>
        StatusCodeInfo BatchUpdateSubjectEpStatus(int subjectId, int watchedEps, int? watchedVols);

        /// <summary>
        /// 获取指定条目收藏信息
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <returns>条目收藏信息</returns>
        CollectionResponse GetUserSubjectDetail(int subjectId);

        /// <summary>
        /// 添加或更新收藏
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="status">条目状态</param>
        /// <param name="comment">简评</param>
        /// <param name="tags">标签（以半角空格分割）</param>
        /// <param name="rating">评分 &lt;br&gt; 1-10</param>
        /// <param name="privacy">收藏隐私 &lt;br&gt; 0 &#x3D; 公开 &lt;br&gt; 1 &#x3D; 私密</param>
        /// <returns>更新后的条目收藏信息</returns>
        CollectionResponse CreateOrUpdateCollection(int subjectId, CollectionStatus status, string comment, string tags, int? rating, Privacy? privacy);
    }
}
