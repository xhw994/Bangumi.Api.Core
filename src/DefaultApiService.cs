using System;
using System.Web;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model.Subjects;

namespace Bangumi.Api.Core
{
    public class DefaultApiService : IApiService
    {
        /// <summary>
        /// Gets or sets the API client.
        /// </summary>
        /// <value>An instance of the ApiClient</value>
        public ApiClient ApiClient { get; }

        public DefaultApiService()
        {
            ApiClient = Configuration.DefaultApiClient;
        }

        #region 条目

        public IEnumerable<CalendarResponse> GetDailyCalendar()
        {
            var path = "/calendar";

            var queryParams = new Dictionary<string, string>();
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;

            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, "Error calling CalendarGet: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, "Error calling CalendarGet: " + response.ErrorMessage, response.ErrorMessage);
            }

            return (IEnumerable<CalendarResponse>)ApiClient.Deserialize(response.Content, typeof(List<CalendarResponse>), response.Headers);
        }

        public SubjectBase GetSubject(int id, ResponseGroup group = ResponseGroup.Small)
        {
            if (id < 1)
            {
                throw new ApiException(400, "Subject Id must be greater than 0");
            }

            // Set request body
            var queryParams = new Dictionary<string, string>()
            {
                {"responseGroup", group.ToDescriptionString() }
            };
            var headerParams = new Dictionary<string, string>();
            var formParams = new Dictionary<string, string>();
            var fileParams = new Dictionary<string, FileParameter>();
            string postBody = null;

            // Set query string and return type
            string path = $"/subject/{id}";
            Type rgroupType;
            switch (group)
            {
                case ResponseGroup.Small:
                    rgroupType = typeof(SubjectSmall);
                    break;
                case ResponseGroup.Medium:
                    rgroupType = typeof(SubjectMedium);
                    break;
                case ResponseGroup.Large:
                    rgroupType = typeof(SubjectLarge);
                    break;
                case ResponseGroup.Ep:
                    rgroupType = typeof(SubjectEp);
                    queryParams.Clear();
                    path += @"/ep";
                    break;
                default:
                    throw new ApiException(400, "Invalid response group");
            }

            // authentication setting, if any
            string[] authSettings = new string[] { "auth" };

            // make the HTTP request
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if ((int)response.StatusCode >= 400)
            {
                throw new ApiException((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.ErrorMessage, response.ErrorMessage);
            }
            return (SubjectBase)ApiClient.Deserialize(response.Content, rgroupType, response.Headers);
        }
       
        public SubjectEp GetSubjectEps(int id)
        {
            return (SubjectEp)GetSubject(id, ResponseGroup.Ep);
        }

        #endregion

        #region 搜索

        public SearchSubjectResponse SearchSubjectByKeywords(string keywords, SubjectType type, int? start, int? maxResults, ResponseGroup group = ResponseGroup.Small)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                throw new ApiException(400, "Missing required parameter 'keywords'");
            }
            if (maxResults > 25)
            {
                maxResults = 25;
            }

            string path = $"/search/subject/{HttpUtility.UrlEncode(keywords)}";

            return null;
        }

        #endregion

        /// <summary>
        /// 条目搜索 条目搜索
        /// </summary>
        /// <param name="keywords">关键词 &lt;br&gt; 需要 URL Encode</param> 
        /// <param name="type">条目类型，参考 [SubjectType](#model-SubjectType)</param> 
        /// <param name="responseGroup">返回数据大小，参考 [ResponseGroup](#model-ResponseGroup) &lt;br&gt; 默认为 small</param> 
        /// <param name="start">开始条数</param> 
        /// <param name="maxResults">每页条数 &lt;br&gt; 最多 25</param> 
        /// <returns>SearchSubjectResponse</returns>            
        public SearchSubjectResponse SearchSubjectByKeywordsGet(string keywords, string type, string responseGroup, int? start, int? maxResults)
        {

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
            IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            if (((int)response.StatusCode) >= 400)
                throw new ApiException((int)response.StatusCode, "Error calling SearchSubjectByKeywordsGet: " + response.Content, response.Content);
            else if (((int)response.StatusCode) == 0)
                throw new ApiException((int)response.StatusCode, "Error calling SearchSubjectByKeywordsGet: " + response.ErrorMessage, response.ErrorMessage);

            return (SearchSubjectResponse)ApiClient.Deserialize(response.Content, typeof(SearchSubjectResponse), response.Headers);
        }
    }
}
