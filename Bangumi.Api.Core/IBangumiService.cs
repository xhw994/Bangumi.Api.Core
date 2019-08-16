﻿using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;
using Bangumi.Api.Core.Model.UserModel;

namespace Bangumi.Api.Core
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IBangumiService
    {
        /// <summary>
        /// 每日放送
        /// </summary>
        /// <returns><see cref="IEnumerable{CalendarResponse}"/></returns>
        IEnumerable<CalendarResponse> GetDailyCalendar();

        /// <summary>
        /// 条目信息
        /// </summary>
        /// <param name="subjectId">条目 ID</param>
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param>
        /// <returns>object</returns>
        SubjectBase GetSubject(int id, ResponseGroup group);

        /// <summary>
        /// 章节数据
        /// </summary>
        /// <param name="subjectId">条目 ID</param> 
        /// <returns>SubjectEpResponse</returns>            
        SubjectEp GetSubjectEps(int id);

        /// <summary>
        /// 条目搜索
        /// </summary>
        /// <param name="keywords">关键词 &lt;br&gt; 需要 URL Encode</param>
        /// <param name="type">条目类型，参考 [SubjectType](#model-SubjectType)</param>
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param>
        /// <param name="start">开始条数</param>
        /// <param name="maxResults">每页条数 &lt;br&gt; 最多 25</param>
        /// <returns>SearchSubjectResponse</returns>
        SubjectSearchResult SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup group, int? start, int? maxResults);

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param>
        /// <returns>User</returns>
        User GetUser(string username);

        /// <summary>
        /// 用户收藏
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="getAllWatching">收藏类型 &lt;br&gt; False - watching &#x3D; 在看的动画与三次元条目 &lt;br&gt; True - all_watching &#x3D; 在看的动画三次元与书籍条目</param> 
        /// <param name="ids">收藏条目 ID &lt;br&gt; 批量查询收藏状态，将条目 ID 以半角逗号分隔，如 1,2,4,6</param> 
        /// <param name="responseGroup">medium / small &lt;br&gt; 默认为 medium。small 时不返回条目详细信息</param> 
        /// <returns>List&lt;UserCollectionResponse&gt;</returns>            
        IEnumerable<SubjectStatus> GetUserCollection(string username, bool getAllWatching, string ids, ResponseGroup group);

        /// <summary>
        /// 用户收藏概览
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="subjectType">条目类型，详见 [SubjectTypeName](#model-SubjectTypeName)</param> 
        /// <param name="appId">[https://bgm.tv/dev/app](https://bgm.tv/dev/app) 申请到的 App ID</param> 
        /// <param name="maxResults">显示条数 &lt;br&gt; 最多 25</param> 
        /// <returns>List&lt;UserCollectionsResponse&gt;</returns>            
        IEnumerable<CollectionsByType> GetUserCollectionsByType(string username, SubjectType subjectType, string appId, int? maxResults);

        /// <summary>
        /// 用户收藏统计
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param>
        /// <param name="appId">[https://bgm.tv/dev/app](https://bgm.tv/dev/app) 申请到的 App ID</param>
        /// <returns>List&lt;UserCollectionsStatusResponse&gt;</returns>
        IEnumerable<CollectionsByType> GetUserCollectionsStatus(string username, string appId);

        /// <summary>
        /// 用户收视进度
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param>
        /// <param name="subjectId">条目 ID &lt;br&gt; 获取指定条目收视进度</param>
        /// <returns>List&lt;UserProgressResponse&gt;</returns>
        IEnumerable<UserProgress> GetUserProgress(string username, int subjectId);

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
    }
}
