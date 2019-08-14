using Bangumi.Api.Core.Client;
using Bangumi.Api.Core.Model.TokenModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bangumi.Api.Core.Client
{
    public interface IBangumiClient
    {
        TResponse Request<TResponse>(BangumiRequest request);
        AuthCode RequestCode();
        Token RequestToken(AuthCode code);
        Token RefreshToken(Token refreshToken);
    }
}
