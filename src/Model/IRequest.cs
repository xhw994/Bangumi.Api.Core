using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Bangumi.Api.Core.Model
{
    public interface IRequest
    {
        string Path { get; set; }

        Dictionary<string, string> QueryParams { get; set; }

        Method Method { get; set; }

        bool RequireAuth { get; set; }
    }
}
