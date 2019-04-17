using System;
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

        public SubjectResponseBase GetSubject(int id, ResponseGroup group = ResponseGroup.Small)
        {
            if (id < 1)
            {
                throw new ApiException(400, "Subject Id must be greater than 0");
            }

            // Setup return type and query string
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
                default:
                    throw new ApiException(400, "Invalid response group");
            }
            string path = $"/subject/{id}";

            var queryParams = new Dictionary<string, string>()
            {
                {"responseGroup", group.ToDescriptionString() }
            };
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
                throw new ApiException((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.Content, response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new ApiException((int)response.StatusCode, "Error calling SubjectBySubjectIdGet: " + response.ErrorMessage, response.ErrorMessage);
            }
            return (SubjectResponseBase)ApiClient.Deserialize(response.Content, rgroupType, response.Headers);
        }
    }
}
