using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace GoSmartValue.Integration.Tests.Setup;

/// <summary>
/// https://stebet.net/mocking-jwt-tokens-in-asp-net-core-integration-tests/
/// We now have a base class that we can have our integration tests inherit from to customize the configuration further.
/// The trick here is to set the Configuration property on the JwtBearerOptions to the values defined in our mock.
/// hen that value has been set, the JWT authentication handler will see that it already has the information it needs
/// to validate the JWT tokens we generate, and will not try to download the required metadata.
/// This is documented behavior:
/// </summary>
public static class MockJwtTokens
{
    public static string Issuer { get; } = Guid.NewGuid().ToString();
    public static SecurityKey SecurityKey { get; }
    public static SigningCredentials SigningCredentials { get; }

    private static readonly JwtSecurityTokenHandler s_tokenHandler = new JwtSecurityTokenHandler();
    private static readonly RandomNumberGenerator s_rng = RandomNumberGenerator.Create();
    private static readonly byte[] s_key = new byte[32];

    static MockJwtTokens()
    {
        s_rng.GetBytes(s_key);
        SecurityKey = new SymmetricSecurityKey(s_key) { KeyId = Guid.NewGuid().ToString() };
        SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
    }

    public static string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        return s_tokenHandler.WriteToken(new JwtSecurityToken(Issuer, null, claims, null, DateTime.UtcNow.AddMinutes(20), SigningCredentials));
    }
}