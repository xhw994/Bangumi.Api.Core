using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Linq;

namespace Bangumi.Api.Core.Client
{
    public class BangumiAuthenticator : OAuth2Authenticator
    {
        private string AuthorizationValue { get => "Bearer " + AccessToken; }

        public BangumiAuthenticator(string accessToken) : base(accessToken) { }

        public override void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                request.AddParameter("Authorization", AuthorizationValue, ParameterType.HttpHeader);
            }
        }
    }
}
