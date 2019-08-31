using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Extension;
using Bangumi.Api.Core.Model;
using Bangumi.Api.Core.Model.Subject;
using Bangumi.Api.Core.Model.User;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using static Bangumi.Api.Core.Configuration;

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
            // Validation.
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)} when calling {nameof(GetUser)}");
            }

            string path = $"/user/{username}";

            BangumiRequest request = new BangumiRequest(path);
            return _client.Request<User>(request);
        }

        public IEnumerable<SubjectStatus> GetCollection(string username, bool allWatching = false, int[] ids = null, ResponseGroup responseGroup = ResponseGroup.Medium)
        {
            // Validation.
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }

            // Compose the request.
            string path = $"/user/{username}/collection";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "cat", allWatching ? "all_watching" : "watching" },
                { "responseGroup", responseGroup.ToDescriptionString() }
            };
            if (ids != null && ids.Length > 0)
            {
                queryParams.Add("ids", string.Join(",", ids));
            }
            BangumiRequest request = new BangumiRequest(path, queryParams: queryParams);

            return _client.Request<IEnumerable<SubjectStatus>>(request);
        }

        public IEnumerable<CollectionsByType> GetCollectionsByType(string username, SubjectType subjectType, int? maxResults = null)
        {
            // Verify 'AppId' exists.
            if (AppId == null)
            {
                throw new ArgumentException($"{nameof(AppId)} is empty.", nameof(AppId));
            }
            // Verify the required argument 'username' is set.
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}", nameof(username));
            }
            // Max result cannot be greater than 25.
            if (maxResults != null && maxResults.Value > 25)
            {
                maxResults = 25;
            }
            // Max result cannot be smaller than 1.
            if (maxResults != null && maxResults.Value < 1)
            {
                maxResults = null;
            }

            // Compose the request.
            string path = $"/user/{username}/collections/{subjectType.ToDescriptionString()}";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "app_id", AppId }
            };
            if (maxResults != null)
            {
                queryParams.Add("max_results", maxResults.ToString());
            }
            BangumiRequest request = new BangumiRequest(path, queryParams: queryParams);

            return _client.Request<IEnumerable<CollectionsByType>>(request);
        }

        public IEnumerable<CollectionsByType> GetCollectionsStatus(string username)
        {
            // Verify the required parameter 'AppId' is set.
            if (AppId == null)
            {
                throw new ArgumentException($"Missing required argument {nameof(AppId)}", nameof(AppId));
            }
            // Verify the required parameter 'username' is set
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required argument {nameof(username)}", nameof(username));
            }

            // Compose the request
            string path = $"/user/{username}/collections/status";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "app_id", AppId }
            };
            BangumiRequest request = new BangumiRequest(path, queryParams: queryParams);

            return _client.Request<IEnumerable<CollectionsByType>>(request);
        }

        public IEnumerable<UserProgress> GetProgress(string username, int subjectId)
        {
            // Verify the required parameter 'username' is set
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"Missing required parameter {nameof(username)}");
            }
            // Validate the required parameter 'subjectId'
            ValidateId(subjectId, ObjectType.Subject);

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
        public SubjectBase GetSubject(int subjectId, ResponseGroup responseGroup = ResponseGroup.Small)
        {
            // Validation.
            ValidateId(subjectId, ObjectType.Subject);

            // Compose the request
            string path = $"/subject/{subjectId }";
            var queryParams = new Dictionary<string, string>()
            {
                {"responseGroup", responseGroup.ToDescriptionString() }
            };
            BangumiRequest request = new BangumiRequest(path, queryParams: queryParams);

            return GetSubject(subjectId, ResponseGroup.Small);
        }

        public SubjectEp GetSubjectAndEpisodes(int subjectId)
        {
            ValidateId(subjectId, ObjectType.Subject);

            // Compose the request
            string path = $"/subject/{subjectId}/ep";
            BangumiRequest request = new BangumiRequest(path);

            return _client.Request<SubjectEp>(request);
        }

        public IEnumerable<CalendarResponse> GetDailyCalendar()
        {
            BangumiRequest request = new BangumiRequest("/calendar");

            return _client.Request<IEnumerable<CalendarResponse>>(request);
        }
        #endregion

        #region 搜索 Search
        public SubjectSearchResult SearchSubjectByKeywords(string keywords, SubjectType type, ResponseGroup responseGroup = ResponseGroup.Small, int start = 0, int maxResults = 10)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                throw new ArgumentException("Missing required parameter 'keywords'");
            }

            string path = $"/search/subject/{HttpUtility.UrlEncode(keywords)}";
            var queryParams = new Dictionary<string, string>()
            {
                { "type", type.ToDescriptionString() },
                { "responseGroup", responseGroup.ToDescriptionString() }
            };
            if (start > 0)
            {
                queryParams.Add("start", start.ToString());
            }
            if (maxResults > 0 && maxResults != 10)
            {
                if (maxResults > 25)
                {
                    maxResults = 25;
                }
                queryParams.Add("max_results", maxResults.ToString());
            }

            BangumiRequest request = new BangumiRequest(path, queryParams: queryParams);
            return _client.Request<SubjectSearchResult>(request);
        }

        #endregion

        #region 进度 Progress

        public StatusCodeInfo UpdateOneEpStatus(int id, EpStatus status)
        {
            ValidateId(id, ObjectType.Episode);

            // Compose the request, note that somehow this is a GET request
            string path = $"/ep/{id}/status/{status.ToDescriptionString()}";
            BangumiRequest request = new BangumiRequest(path, Method.GET, true);

            return _client.Request<StatusCodeInfo>(request);
        }

        public StatusCodeInfo UpdateMultipleEpStatus(int[] ids, EpStatus status)
        {
            ValidateId(ids, ObjectType.Episode);

            // Compose the request
            int lastId = ids[ids.Length - 1];
            string path = $"/ep/{lastId}/status/{status.ToDescriptionString()}";
            Dictionary<string, string> queryParams = new Dictionary<string, string>()
            {
                { "ep_id", string.Join(',', ids) }
            };
            BangumiRequest request = new BangumiRequest(path, Method.POST, true, queryParams);

            return _client.Request<StatusCodeInfo>(request);
        }

        public StatusCodeInfo BatchUpdateSubjectEpStatus(int subjectId, int watchedEps, int? watchedVols)
        {
            // Validation
            ValidateId(subjectId, ObjectType.Subject);
            ValidateId(watchedEps, ObjectType.Episode);
            if (watchedVols != null) ValidateId(watchedVols.Value, ObjectType.Volume);

            // Compose the request
            string path = $"/subject/{subjectId}/update";
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "watched_eps", watchedEps.ToString() }
            };
            if (watchedVols != null) queryParams.Add("watched_vols", watchedVols.Value.ToString());
            BangumiRequest request = new BangumiRequest(path, Method.POST, true, queryParams);

            return _client.Request<StatusCodeInfo>(request);
        }

        #endregion

        #region 收藏 Collection
        public CollectionResponse GetUserSubjectDetail(int subjectId)
        {
            ValidateId(subjectId, ObjectType.Subject);

            // Compose the request
            string path = $"/collection/{subjectId}";
            BangumiRequest request = new BangumiRequest(path, Method.GET, true);

            return _client.Request<CollectionResponse>(request);
        }

        public CollectionResponse CreateOrUpdateCollection(int subjectId, CollectionStatus status, string comment, string tags, int? rating, Privacy? privacy)
        {
            ValidateId(subjectId, ObjectType.Subject);

            // Compose the request
            string path = $"/collection/{subjectId}/update";
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "status ", status.ToDescriptionString() }
            };
            if (comment != null) queryParams.Add("comment", comment);
            if (tags != null) queryParams.Add("tags", tags);
            if (rating != null) queryParams.Add("rating", rating.ToString());
            if (privacy != null) queryParams.Add("privacy", ((int)privacy.Value).ToString());
            BangumiRequest request = new BangumiRequest(path, Method.POST, true, queryParams);

            return _client.Request<CollectionResponse>(request);
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
