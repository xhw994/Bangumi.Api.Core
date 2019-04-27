using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bangumi.Api.Core.Client
{
    public class BangumiRequest
    {
        public string Path { get; }
        public Dictionary<string, string> QueryParams { get; }
        public Method Method { get; }
        public bool RequireAuth { get; }

        public BangumiRequest(string path, Method method = Method.GET, bool requireAuth = false, Dictionary<string, string> queryParams = null)
        {
            Path = path;
            Method = method;
            RequireAuth = requireAuth;
            QueryParams = queryParams;
        }
    }
}
