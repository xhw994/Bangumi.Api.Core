using System;
using System.Web;
using System.Collections.Generic;
using RestSharp;
using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model.SubjectModel;
using Bangumi.Api.Core.Model.UserModel;
using static Bangumi.Api.Core.Configuration;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bangumi.Api.Core
{
    public class DefaultBangumiService : IBangumiService
    {
        private readonly BangumiClient _client;

        #region Init

        public DefaultBangumiService() => _client = new BangumiClient();

        #endregion

        #region 用户 User

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

        public IEnumerable<SubjectStatus> GetUserCollection(string username, bool allWatching = false, string ids = null, ResponseGroup group = ResponseGroup.Medium)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            if (ids != null && new Regex(@"(\d+,)*\d+").IsMatch(ids) == false)
            {
                throw new ArgumentException($"Invalid required parameter: {nameof(ids)} should contain positive numbers seperated by commas");
            }

            string path = $"/user/{username}/collection";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "cat", (allWatching? "all_watching " : "watching ") },
                { "responseGroup", (group == ResponseGroup.Small ? "small" : "medium") }
            };
            if (ids != null)
            {
                queryParams.Add("ids", ids);
            }

            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);
            return _client.Request<IEnumerable<SubjectStatus>>(request);
        }

        public IEnumerable<SubjectStatus> GetUserCollection(string username, bool allWatching = false, ResponseGroup group = ResponseGroup.Medium, params int[] ids)
        {
            string idstr = (ids == null || ids.Length == 0) ? null : string.Join(",", ids);
            return GetUserCollection(username, allWatching, idstr, group);
        }

        public IEnumerable<CollectionsByType> GetUserCollectionsByType(string username, SubjectType subjectType, string appId, int? maxResults = null)
        {
            // Verify the required parameter 'username' is set
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            // Verify the required parameter 'appId' is set
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentException($"Missing required parameter {nameof(appId)}");
            }

            // Compose the request
            string path = $"/user/{username}/collections/{subjectType.ToDescriptionString()}";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "app_id", appId }
            };
            if (maxResults != null)
            {
                queryParams.Add("max_results", maxResults.ToString());
            }
            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams); // Setting requireAuth = false because appSecret is not a required param

            return _client.Request<IEnumerable<CollectionsByType>>(request);
        }

        public IEnumerable<CollectionsByType> GetUserCollectionsByType(string username, SubjectType subjectType, int? maxResults = null)
        {
            if (AppId == null)
            {
                throw new InvalidOperationException($"{nameof(AppId)} is empty.");
            }
            return GetUserCollectionsByType(username, subjectType, AppId, maxResults);
        }

        public IEnumerable<CollectionsByType> GetUserCollectionsStatus(string username, string appId = null)
        {
            // Verify the required parameter 'username' is set
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            // Verify the required parameter 'appId' is set
            if (string.IsNullOrEmpty(appId) && string.IsNullOrEmpty(appId))
            {
                throw new ApiException(401, $"The client needs to be authenticated before calling {nameof(GetUserCollectionsStatus)} without an '{nameof(AppId)}' argument.");
            }

            // Compose the request
            string path = $"/user/{username}/collections/status";
            BangumiRequest request = new BangumiRequest(path); // Setting requireAuth = false because appSecret is not a required param

            return _client.Request<IEnumerable<CollectionsByType>>(request);
        }

        public IEnumerable<UserProgress> GetUserProgress(string username, int subjectId)
        {
            // Verify the required parameter 'username' is set
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            // Validate the required parameter 'subjectId'
            if (subjectId < 1)
            {
                throw new ArgumentException($"{nameof(subjectId)} must be greater than 0");
            }

            // Compose the request
            string path = $"/user/{username}/progress";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "subject_id", subjectId.ToString() }
            };
            BangumiRequest request = new BangumiRequest(path, Method.GET, true, queryParams);

            return _client.Request<IEnumerable<UserProgress>>(request);
        }

        #endregion

        #region 条目 Subject

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
            string path = $"/subject/{id}";
            var queryParams = new Dictionary<string, string>()
            {
                {"responseGroup", group.ToDescriptionString() }
            };
            BangumiRequest request = new BangumiRequest(path, Method.GET, false, queryParams);

            // Return based on response group
            switch (group)
            {
                case ResponseGroup.Large:
                    return _client.Request<SubjectLarge>(request);
                case ResponseGroup.Medium:
                    return _client.Request<SubjectMedium>(request);
                default:
                case ResponseGroup.Small:
                    return _client.Request<SubjectSmall>(request);
            }
        }

        public SubjectEp GetSubjectEps(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Subject Id must be greater than 0");
            }

            // Compose the request
            string path = $"/subject/{id}/ep";
            BangumiRequest request = new BangumiRequest(path);

            return _client.Request<SubjectEp>(request);
        }

        #endregion

        #region 搜索 Search

        public SubjectSearchResult SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup group = ResponseGroup.Small, int? start = null, int? maxResults = null)
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
            return _client.Request<SubjectSearchResult>(request);
        }

        #endregion

        #region 进度 Progress

        public StatusCode UpdateOneEpStatus(int id, EpStatus status)
        {
            // Verify the episode id is greater than 0.
            if (id < 1)
            {
                throw new ArgumentException("Episode ID must be greater than 0.");
            }

            // Compose the request, note that somehow this is a GET request
            string path = $"/ep/{id}/status/{status.ToDescriptionString()}";
            BangumiRequest request = new BangumiRequest(path, Method.GET, true);

            return _client.Request<StatusCode>(request);
        }

        public StatusCode UpdateManyEpStatus(EpStatus status, params int[] ids)
        {
            // Verify the IDs are not empty and are valid
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException("The episode IDs are empty.");
            }
            foreach (int id in ids)
            {
                if (id < 1)
                {
                    throw new ArgumentException("Episode ID must be greater than 0.");
                }
            }

            // Compose the request
            int lastId = ids[ids.Length - 1];
            string path = $"/ep/{lastId}/status/{status.ToDescriptionString()}";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "ep_id", string.Join(',', ids) }
            };
            BangumiRequest request = new BangumiRequest(path, Method.POST, true, queryParams);

            return _client.Request<StatusCode>(request);
        }

        public StatusCode UpdateSubjectEpStatus(int subjectId, int? watchedEps, int? watchedVols = null)
        {
            // Validation
            ValidateId(subjectId, ObjectType.Subject);
            if (watchedEps != null) ValidateId(watchedEps.Value, ObjectType.Episode);
            if (watchedVols != null) ValidateId(watchedVols.Value, ObjectType.Volume);
            if (watchedEps == null && watchedVols == null)
            {
                throw new ArgumentNullException($"Both of {nameof(watchedEps)} and {nameof(watchedVols)} cannot be null.");
            }

            // Compose the request
            string path = $"/subject/{subjectId}/update/";
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            if (watchedEps != null) queryParams.Add("watched_eps", watchedEps.Value.ToString());
            if (watchedVols != null) queryParams.Add("watched_vols", watchedVols.Value.ToString());
            BangumiRequest request = new BangumiRequest(path, Method.POST, true, queryParams);

            return _client.Request<StatusCode>(request);
        }

        #endregion

        #region Helpers

        private void ValidateId(int id, ObjectType type)
        {
            string typeString = type.ToString().ToLower();
            // Validate the id is greater than 0.
            if (id < 1)
            {
                throw new ArgumentException(typeString + " ID must be greater than 0.");
            }
        }

        private void ValidateId(int[] ids, ObjectType type)
        {
            string typeString = type.ToString().ToLower();
            // Validate the IDs are not empty and are valid
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentException(typeString + " IDs are empty.");
            }
            foreach (int id in ids)
            {
                if (id < 1)
                {
                    throw new ArgumentException(typeString + " ID must be greater than 0.");
                }
            }
        }

        private enum ObjectType
        {
            Subject,
            Episode,
            Volume
        }

        #endregion
    }
}
