using System;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core
{ 
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public class DefaultApi : IDefaultApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <param name="apiClient"> an instance of ApiClient (optional)</param>
        /// <returns></returns>
        public DefaultApi(ApiClient apiClient = null)
        {
            if (apiClient == null) // use the default one in Configuration
                this.ApiClient = Configuration.DefaultApiClient; 
            else
                this.ApiClient = apiClient;
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultApi"/> class.
        /// </summary>
        /// <returns></returns>
        public DefaultApi(string basePath)
        {
            this.ApiClient = new ApiClient(basePath);
        }
    
        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public void SetBasePath(string basePath)
        {
            this.ApiClient.BasePath = basePath;
        }
    
        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <param name="basePath">The base path</param>
        /// <value>The base path</value>
        public string GetBasePath(string basePath)
        {
            return this.ApiClient.BasePath;
        }
    
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient {get; set;}
   
    
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
        public CollectionResponse CollectionBySubjectIdAndActionPost (int? subjectId, string action, string status, string comment, string tags, int? rating, string privacy)
        {
            
            // verify the required parameter 'subjectId' is set
            if (subjectId == null) throw new ApiException(400, "Missing required parameter 'subjectId' when calling CollectionBySubjectIdAndActionPost");
            
            // verify the required parameter 'action' is set
            if (action == null) throw new ApiException(400, "Missing required parameter 'action' when calling CollectionBySubjectIdAndActionPost");
            
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling CollectionBySubjectIdAndActionPost");
            
    
            var path = "/collection/{subject_id}/{action}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subject_id" + "}", ApiClient.ParameterToString(subjectId));
            path = path.Replace("{" + "action" + "}", ApiClient.ParameterToString(action));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
            if (status != null) queryParams.Add("status", ApiClient.ParameterToString(status)); // query parameter
            if (comment != null) queryParams.Add("comment", ApiClient.ParameterToString(comment)); // query parameter
            if (tags != null) queryParams.Add("tags", ApiClient.ParameterToString(tags)); // query parameter
            if (rating != null) queryParams.Add("rating", ApiClient.ParameterToString(rating)); // query parameter
            if (privacy != null) queryParams.Add("privacy", ApiClient.ParameterToString(privacy)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectionBySubjectIdAndActionPost: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling CollectionBySubjectIdAndActionPost: " + response.ErrorMessage, response.ErrorMessage);
    
            return (CollectionResponse) ApiClient.Deserialize(response.Content, typeof(CollectionResponse), response.Headers);
        }
    
        /// <summary>
        /// 更新收视进度 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param> 
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param> 
        /// <returns>StatusCode</returns>            
        public StatusCode EpStatusByIdAndStatusGet (int? id, string status)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EpStatusByIdAndStatusGet");
            
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling EpStatusByIdAndStatusGet");
            
    
            var path = "/ep/{id}/status/{status}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
path = path.Replace("{" + "status" + "}", ApiClient.ParameterToString(status));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
                                                    
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling EpStatusByIdAndStatusGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EpStatusByIdAndStatusGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (StatusCode) ApiClient.Deserialize(response.Content, typeof(StatusCode), response.Headers);
        }
    
        /// <summary>
        /// 更新收视进度 更新收视进度
        /// </summary>
        /// <param name="id">章节 ID</param> 
        /// <param name="status">收视类型，参考 [EpStatusType](#model-EpStatusType)</param> 
        /// <param name="epId">使用 POST 批量更新 &lt;br&gt; 将章节以半角逗号分隔，如 &#x60;3697,3698,3699&#x60;。请求时 URL 中的 ep_id 为最后一个章节 ID</param> 
        /// <returns>StatusCode</returns>            
        public StatusCode EpStatusByIdAndStatusPost (int? id, string status, string epId)
        {
            
            // verify the required parameter 'id' is set
            if (id == null) throw new ApiException(400, "Missing required parameter 'id' when calling EpStatusByIdAndStatusPost");
            
            // verify the required parameter 'status' is set
            if (status == null) throw new ApiException(400, "Missing required parameter 'status' when calling EpStatusByIdAndStatusPost");
            
    
            var path = "/ep/{id}/status/{status}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "id" + "}", ApiClient.ParameterToString(id));
path = path.Replace("{" + "status" + "}", ApiClient.ParameterToString(status));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (epId != null) queryParams.Add("ep_id", ApiClient.ParameterToString(epId)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling EpStatusByIdAndStatusPost: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling EpStatusByIdAndStatusPost: " + response.ErrorMessage, response.ErrorMessage);
    
            return (StatusCode) ApiClient.Deserialize(response.Content, typeof(StatusCode), response.Headers);
        }
    
        /// <summary>
        /// 条目搜索 条目搜索
        /// </summary>
        /// <param name="keywords">关键词 &lt;br&gt; 需要 URL Encode</param> 
        /// <param name="type">条目类型，参考 [SubjectType](#model-SubjectType)</param> 
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param> 
        /// <param name="start">开始条数</param> 
        /// <param name="maxResults">每页条数 &lt;br&gt; 最多 25</param> 
        /// <returns>SearchSubjectResponse</returns>            
        public SearchSubjectResponse SearchSubjectByKeywordsGet (string keywords, string type, string responseGroup, int? start, int? maxResults)
        {
            
            // verify the required parameter 'keywords' is set
            if (keywords == null) throw new ApiException(400, "Missing required parameter 'keywords' when calling SearchSubjectByKeywordsGet");
            
    
            var path = "/search/subject/{keywords}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "keywords" + "}", ApiClient.ParameterToString(keywords));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (type != null) queryParams.Add("type", ApiClient.ParameterToString(type)); // query parameter
 if (responseGroup != null) queryParams.Add("responseGroup", ApiClient.ParameterToString(responseGroup)); // query parameter
 if (start != null) queryParams.Add("start", ApiClient.ParameterToString(start)); // query parameter
 if (maxResults != null) queryParams.Add("max_results", ApiClient.ParameterToString(maxResults)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SearchSubjectByKeywordsGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SearchSubjectByKeywordsGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (SearchSubjectResponse) ApiClient.Deserialize(response.Content, typeof(SearchSubjectResponse), response.Headers);
        }
    
        /// <summary>
        /// 条目信息 条目信息
        /// </summary>
        /// <param name="subjectId">条目 ID</param> 
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param> 
        /// <returns>object</returns>            
        public object SubjectBySubjectIdGet (int? subjectId, string responseGroup)
        {
            
            // verify the required parameter 'subjectId' is set
            if (subjectId == null) throw new ApiException(400, "Missing required parameter 'subjectId' when calling SubjectBySubjectIdGet");
            
    
            var path = "/subject/{subject_id}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subject_id" + "}", ApiClient.ParameterToString(subjectId));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (responseGroup != null) queryParams.Add("responseGroup", ApiClient.ParameterToString(responseGroup)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (object) ApiClient.Deserialize(response.Content, typeof(object), response.Headers);
        }
    
        /// <summary>
        /// 章节数据 章节数据
        /// </summary>
        /// <param name="subjectId">条目 ID</param> 
        /// <returns>SubjectEpResponse</returns>            
        public SubjectEpResponse SubjectEpBySubjectIdGet (int? subjectId)
        {
            
            // verify the required parameter 'subjectId' is set
            if (subjectId == null) throw new ApiException(400, "Missing required parameter 'subjectId' when calling SubjectEpBySubjectIdGet");
            
    
            var path = "/subject/{subject_id}/ep";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subject_id" + "}", ApiClient.ParameterToString(subjectId));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
                                                    
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectEpBySubjectIdGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectEpBySubjectIdGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (SubjectEpResponse) ApiClient.Deserialize(response.Content, typeof(SubjectEpResponse), response.Headers);
        }
    
        /// <summary>
        /// 批量更新收视进度 批量更新收视进度
        /// </summary>
        /// <param name="subjectId">条目 ID</param> 
        /// <param name="watchedEps">如看到 123 话则 POST &#x60;123&#x60; &lt;br&gt; 书籍条目传 watched_eps 与 watched_vols 至少其一</param> 
        /// <param name="watchedVols">如看到第 3 卷则 POST &#x60;3&#x60;, 仅对书籍条目有效</param> 
        /// <returns>StatusCode</returns>            
        public StatusCode SubjectUpdateWatchedEpsBySubjectIdPost (int? subjectId, string watchedEps, string watchedVols)
        {
            
            // verify the required parameter 'subjectId' is set
            if (subjectId == null) throw new ApiException(400, "Missing required parameter 'subjectId' when calling SubjectUpdateWatchedEpsBySubjectIdPost");
            
            // verify the required parameter 'watchedEps' is set
            if (watchedEps == null) throw new ApiException(400, "Missing required parameter 'watchedEps' when calling SubjectUpdateWatchedEpsBySubjectIdPost");
            
    
            var path = "/subject/{subject_id}/update/watched_eps";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "subject_id" + "}", ApiClient.ParameterToString(subjectId));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (watchedEps != null) queryParams.Add("watched_eps", ApiClient.ParameterToString(watchedEps)); // query parameter
 if (watchedVols != null) queryParams.Add("watched_vols", ApiClient.ParameterToString(watchedVols)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.POST, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectUpdateWatchedEpsBySubjectIdPost: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling SubjectUpdateWatchedEpsBySubjectIdPost: " + response.ErrorMessage, response.ErrorMessage);
    
            return (StatusCode) ApiClient.Deserialize(response.Content, typeof(StatusCode), response.Headers);
        }
    
        /// <summary>
        /// 用户信息 用户信息
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <returns>User</returns>            
        public User UserByUsernameGet (string username)
        {
            
            // verify the required parameter 'username' is set
            if (username == null) throw new ApiException(400, "Missing required parameter 'username' when calling UserByUsernameGet");
            
    
            var path = "/user/{username}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "username" + "}", ApiClient.ParameterToString(username));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
                                                    
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserByUsernameGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserByUsernameGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (User) ApiClient.Deserialize(response.Content, typeof(User), response.Headers);
        }
    
        /// <summary>
        /// 用户收藏 用户收藏
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="cat">收藏类型 &lt;br&gt; watching &#x3D; 在看的动画与三次元条目 &lt;br&gt; all_watching &#x3D; 在看的动画三次元与书籍条目</param> 
        /// <param name="ids">收藏条目 ID &lt;br&gt; 批量查询收藏状态，将条目 ID 以半角逗号分隔，如 1,2,4,6</param> 
        /// <param name="responseGroup">medium / small &lt;br&gt; 默认为 medium。small 时不返回条目详细信息</param> 
        /// <returns>List&lt;UserCollectionResponse&gt;</returns>            
        public List<UserCollectionResponse> UserCollectionByUsernameGet (string username, string cat, string ids, string responseGroup)
        {
            
            // verify the required parameter 'username' is set
            if (username == null) throw new ApiException(400, "Missing required parameter 'username' when calling UserCollectionByUsernameGet");
            
            // verify the required parameter 'cat' is set
            if (cat == null) throw new ApiException(400, "Missing required parameter 'cat' when calling UserCollectionByUsernameGet");
            
    
            var path = "/user/{username}/collection";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "username" + "}", ApiClient.ParameterToString(username));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (cat != null) queryParams.Add("cat", ApiClient.ParameterToString(cat)); // query parameter
 if (ids != null) queryParams.Add("ids", ApiClient.ParameterToString(ids)); // query parameter
 if (responseGroup != null) queryParams.Add("responseGroup", ApiClient.ParameterToString(responseGroup)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionByUsernameGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionByUsernameGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<UserCollectionResponse>) ApiClient.Deserialize(response.Content, typeof(List<UserCollectionResponse>), response.Headers);
        }
    
        /// <summary>
        /// 用户收藏概览 用户收藏概览
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="subjectType">条目类型，详见 [SubjectTypeName](#model-SubjectTypeName)</param> 
        /// <param name="appId">[https://bgm.tv/dev/app](https://bgm.tv/dev/app) 申请到的 App ID</param> 
        /// <param name="maxResults">显示条数 &lt;br&gt; 最多 25</param> 
        /// <returns>List&lt;UserCollectionsResponse&gt;</returns>            
        public List<UserCollectionsResponse> UserCollectionsByUsernameAndSubjectTypeGet (string username, string subjectType, string appId, int? maxResults)
        {
            
            // verify the required parameter 'username' is set
            if (username == null) throw new ApiException(400, "Missing required parameter 'username' when calling UserCollectionsByUsernameAndSubjectTypeGet");
            
            // verify the required parameter 'subjectType' is set
            if (subjectType == null) throw new ApiException(400, "Missing required parameter 'subjectType' when calling UserCollectionsByUsernameAndSubjectTypeGet");
            
            // verify the required parameter 'appId' is set
            if (appId == null) throw new ApiException(400, "Missing required parameter 'appId' when calling UserCollectionsByUsernameAndSubjectTypeGet");
            
    
            var path = "/user/{username}/collections/{subject_type}";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "username" + "}", ApiClient.ParameterToString(username));
path = path.Replace("{" + "subject_type" + "}", ApiClient.ParameterToString(subjectType));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (appId != null) queryParams.Add("app_id", ApiClient.ParameterToString(appId)); // query parameter
 if (maxResults != null) queryParams.Add("max_results", ApiClient.ParameterToString(maxResults)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionsByUsernameAndSubjectTypeGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionsByUsernameAndSubjectTypeGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<UserCollectionsResponse>) ApiClient.Deserialize(response.Content, typeof(List<UserCollectionsResponse>), response.Headers);
        }
    
        /// <summary>
        /// 用户收藏统计 用户收藏统计
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="appId">[https://bgm.tv/dev/app](https://bgm.tv/dev/app) 申请到的 App ID</param> 
        /// <returns>List&lt;UserCollectionsStatusResponse&gt;</returns>            
        public List<UserCollectionsStatusResponse> UserCollectionsStatusByUsernameGet (string username, string appId)
        {
            
            // verify the required parameter 'username' is set
            if (username == null) throw new ApiException(400, "Missing required parameter 'username' when calling UserCollectionsStatusByUsernameGet");
            
            // verify the required parameter 'appId' is set
            if (appId == null) throw new ApiException(400, "Missing required parameter 'appId' when calling UserCollectionsStatusByUsernameGet");
            
    
            var path = "/user/{username}/collections/status";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "username" + "}", ApiClient.ParameterToString(username));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (appId != null) queryParams.Add("app_id", ApiClient.ParameterToString(appId)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionsStatusByUsernameGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserCollectionsStatusByUsernameGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<UserCollectionsStatusResponse>) ApiClient.Deserialize(response.Content, typeof(List<UserCollectionsStatusResponse>), response.Headers);
        }
    
        /// <summary>
        /// 用户收视进度 用户收视进度
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="subjectId">条目 ID &lt;br&gt; 获取指定条目收视进度</param> 
        /// <returns>List&lt;UserProgressResponse&gt;</returns>            
        public List<UserProgressResponse> UserProgressByUsernameGet (string username, int? subjectId)
        {
            
            // verify the required parameter 'username' is set
            if (username == null) throw new ApiException(400, "Missing required parameter 'username' when calling UserProgressByUsernameGet");
            
    
            var path = "/user/{username}/progress";
            path = path.Replace("{format}", "json");
            path = path.Replace("{" + "username" + "}", ApiClient.ParameterToString(username));
    
            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;
    
             if (subjectId != null) queryParams.Add("subject_id", ApiClient.ParameterToString(subjectId)); // query parameter
                                        
            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };
    
            // make the HTTP request
            IRestResponse response = (IRestResponse) ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);
    
            if (((int)response.StatusCode) >= 400)
                throw new ApiException ((int)response.StatusCode, "Error calling UserProgressByUsernameGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException ((int)response.StatusCode, "Error calling UserProgressByUsernameGet: " + response.ErrorMessage, response.ErrorMessage);
    
            return (List<UserProgressResponse>) ApiClient.Deserialize(response.Content, typeof(List<UserProgressResponse>), response.Headers);
        }
    
    }
}
