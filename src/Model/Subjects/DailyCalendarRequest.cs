using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Bangumi.Api.Core.Model.Subjects
{
    public class DailyCalendarRequest : IRequest
    {
        public string Path { get; set; }
        public Dictionary<string, string> QueryParams { get; set; }
        public Method Method { get; set; }
        public bool RequireAuth { get; set; }

        public DailyCalendarRequest()
        {
            Path = "/calendar";
            QueryParams = new Dictionary<string, string>();
            Method = Method.GET;
            RequireAuth = false;
        }
    }
}
