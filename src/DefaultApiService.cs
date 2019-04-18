using System;
using System.Web;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model.Subjects;
using Bangumi.Api.Core.Model.Users;
using System.Text.RegularExpressions;

namespace Bangumi.Api.Core
{
    public class DefaultApiService : IApiService
    {
        private readonly BangumiClient _client;

        public DefaultApiService()
        {
            _client = new BangumiClient();
        }

        #region 用户

        public User GetUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)} when calling {nameof(GetUser)}");
            }

            string path = $"/user/{username}";

            BangumiRequest request = new BangumiRequest(path);
            return _client.Request<User>(request);
        }

        public IEnumerable<UserCollectionResponse> GetUserCollection(string username, bool getAllWatching, string ids = null, ResponseGroup group = ResponseGroup.Medium)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            if (ids != null && new Regex(@"(\d*+,)*\d*").IsMatch(ids) == false)
            {
                throw new ArgumentException($"Invalid required parameter: {nameof(ids)} should contain positive numbers seperated by commas");
            }

            string path = $"/user/{username}/collection";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "cat", (getAllWatching? "all_watching " : "watching ") },
                { "responseGroup", (group == ResponseGroup.Small ? "small" : "medium") }
            };
            if (ids != null)
            {
                queryParams.Add("ids", ids);
            }

            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);
            return _client.Request<IEnumerable<UserCollectionResponse>>(request);
        }

        public IEnumerable<UserCollectionResponse> GetUserCollection(string username, bool getAll, ResponseGroup group = ResponseGroup.Medium, params int[] ids)
        {
            string idstr = (ids == null || ids.Length == 0) ? null : string.Join(",", ids);
            return GetUserCollection(username, getAll, idstr, group);
        }

        /// <summary>
        /// 用户收藏 用户收藏
        /// </summary>
        /// <param name="username">用户名 &lt;br&gt; 也可使用 UID</param> 
        /// <param name="cat">收藏类型 &lt;br&gt; watching &#x3D; 在看的动画与三次元条目 &lt;br&gt; all_watching &#x3D; 在看的动画三次元与书籍条目</param> 
        /// <param name="ids">收藏条目 ID &lt;br&gt; 批量查询收藏状态，将条目 ID 以半角逗号分隔，如 1,2,4,6</param> 
        /// <param name="responseGroup">medium / small &lt;br&gt; 默认为 medium。small 时不返回条目详细信息</param> 
        /// <returns>List&lt;UserCollectionResponse&gt;</returns>            
        public List<UserCollectionResponse> UserCollectionByUsernameGet(string username, string cat, string ids, string responseGroup)
        {
            //var queryParams = new Dictionary<string, string>();
            //var headerParams = new Dictionary<string, string>();
            //var formParams = new Dictionary<string, string>();
            //var fileParams = new Dictionary<string, FileParameter>();
            //string postBody = null;

            //if (cat != null) queryParams.Add("cat", ApiClient.ParameterToString(cat)); // query parameter
            //if (ids != null) queryParams.Add("ids", ApiClient.ParameterToString(ids)); // query parameter
            //if (responseGroup != null) queryParams.Add("responseGroup", ApiClient.ParameterToString(responseGroup)); // query parameter

            //// authentication setting, if any
            //string[] authSettings = new string[] { "auth" };

            //// make the HTTP request
            //IRestResponse response = (IRestResponse)ApiClient.CallApi(path, Method.GET, queryParams, postBody, headerParams, formParams, fileParams, authSettings);

            //if (((int)response.StatusCode) >= 400)
            //    throw new ApiException((int)response.StatusCode, "Error calling UserCollectionByUsernameGet: " + response.Content, response.Content);
            //else if (((int)response.StatusCode) == 0)
            //    throw new ApiException((int)response.StatusCode, "Error calling UserCollectionByUsernameGet: " + response.ErrorMessage, response.ErrorMessage);

            //return (List<UserCollectionResponse>)ApiClient.Deserialize(response.Content, typeof(List<UserCollectionResponse>), response.Headers);

            return null;
        }

        #endregion

        #region 条目

        public IEnumerable<CalendarResponse> GetDailyCalendar()
        {
            BangumiRequest request = new BangumiRequest("/calendar");

            return _client.Request<IEnumerable<CalendarResponse>>(request);
        }

        public SubjectBase GetSubject(int id, ResponseGroup group = ResponseGroup.Small)
        {
            if (id < 1)
            {
                throw new ArgumentException("Subject Id must be greater than 0");
            }

            // Compose the request
            string path = $"/subject/{id}" + (group == ResponseGroup.Ep ? @"/ep" : string.Empty);
            var queryParams = group == ResponseGroup.Ep ? null : new Dictionary<string, string>()
            {
                {"responseGroup", group.ToDescriptionString() }
            };
            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);

            // Return based on response group
            switch (group)
            {
                default:
                case ResponseGroup.Small:
                    return _client.Request<SubjectSmall>(request);
                case ResponseGroup.Medium:
                    return _client.Request<SubjectMedium>(request);
                case ResponseGroup.Large:
                    return _client.Request<SubjectLarge>(request);
                case ResponseGroup.Ep:
                    return _client.Request<SubjectEp>(request);
            }
        }
       
        public SubjectEp GetSubjectEps(int id)
        {
            return (SubjectEp)GetSubject(id, ResponseGroup.Ep);
        }

        #endregion

        #region 搜索

        public SearchSubjectResponse SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup group = ResponseGroup.Small, int ? start = null, int? maxResults = null)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                throw new ArgumentException("Missing required parameter 'keywords'");
            }
            if (maxResults > 25)
            {
                maxResults = 25;
            }

            string path = $"/search/subject/{HttpUtility.UrlEncode(keywords)}";
            var queryParams = new Dictionary<string, string>()
            {
                { "type", type.ToDescriptionString() },
                { "responseGroup", group.ToDescriptionString() }
            };
            if (start != null)
            {
                queryParams.Add("start", Convert.ToString(start));
            }
            if (maxResults != null)
            {
                queryParams.Add("max_results", Convert.ToString(maxResults));
            }

            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);
            return _client.Request<SearchSubjectResponse>(request);
        }

        #endregion
    }
}
