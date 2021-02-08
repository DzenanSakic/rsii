using System;
using System.Security.Claims;

namespace AMA.Api.JWT
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveRefreshTokenByUserName(string username);
    }
}