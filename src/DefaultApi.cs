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
                this.ApiClient = Client.Configuration.DefaultApiClient; 
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
    }
}
