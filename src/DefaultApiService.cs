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
                throw new ApiException((int)response.StatusCode, "Error calling GetDailyCalendar: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, "Error calling GetDailyCalendar: " + response.ErrorMessage, response.ErrorMessage);
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
                throw new ApiException((int)response.StatusCode, $"Error calling {(group == ResponseGroup.Ep ? "GetSubject" : "GetSubject")}: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, $"Error calling {(group == ResponseGroup.Ep ? "GetSubject" : "GetSubject")}: " + response.ErrorMessage, response.ErrorMessage);
            }
            return (SubjectBase)ApiClient.Deserialize(response.Content, rgroupType, response.Headers);
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
                throw new ApiException(400, "Missing required parameter 'keywords'");
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
            if (start != null) queryParams.Add("start", ApiClient.ParameterToString(start)); // query parameter
            if (maxResults != null) queryParams.Add("max_results", ApiClient.ParameterToString(maxResults)); // query parameters
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
                throw new ApiException((int)response.StatusCode, "Error calling SearchSubjectByKeywords: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, "Error calling SearchSubjectByKeywords: " + response.ErrorMessage, response.ErrorMessage);
            }

            return (SearchSubjectResponse)ApiClient.Deserialize(response.Content, typeof(SearchSubjectResponse), response.Headers);
        }

        #endregion
    }
}
