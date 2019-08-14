using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static Bangumi.Api.Core.Extension.StringExtension;
using static Bangumi.Api.Core.Configuration;
using Bangumi.Api.Core.Model.TokenModel;
using Newtonsoft.Json;

namespace Bangumi.Api.Core.Client
{
    public class BangumiAuthenticator : OAuth2Authenticator
    {
        // TODO: Implement state param, Auto refresh?, Add timeout when requesting code 3 min?

        private string AuthorizationValue { get => "Bearer " + AccessToken; }

        public BangumiAuthenticator(string accessToken) : base(accessToken) { }

        public override void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                request.AddParameter("Authorization", AuthorizationValue, ParameterType.HttpHeader);
            }
        }

        public void RequestAuthCode()
        {
            throw new NotImplementedException();
        }

        public void RequestAccessToken(IRestClient client)
        {
            throw new NotImplementedException();
        }

        public void RequestTokenRefresh(IRestClient client)
        {
            throw new NotImplementedException();
        }
    }
}
