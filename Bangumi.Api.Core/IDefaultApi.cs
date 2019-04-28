using System;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.SubjectModel;

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

        

                

        
    }
}
