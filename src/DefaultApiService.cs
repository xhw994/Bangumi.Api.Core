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
        private readonly BangumiClient _client;

        public DefaultApiService()
        {
            ApiClient = Configuration.DefaultApiClient;
            _client = new BangumiClient();
        }

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
                throw new ApiException(400, "Subject Id must be greater than 0");
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
            if (start != null)
            {
                queryParams.Add("start", ApiClient.ParameterToString(start));
            }
            if (maxResults != null)
            {
                queryParams.Add("max_results", ApiClient.ParameterToString(maxResults));
            }

            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);
            return _client.Request<SearchSubjectResponse>(request);
        }

        #endregion
    }
}
