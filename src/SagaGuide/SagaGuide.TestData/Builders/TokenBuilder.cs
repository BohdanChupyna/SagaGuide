using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SagaGuide.Api.Constants;
using SagaGuide.Infrastructure.JsonConverters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace SagaGuide.TestData.Builders;

public class TokenBuilder
{
    private readonly List<Claim> claims = new();
    private readonly List<string> scopes = new();

    public string Build()
    {
        foreach (var scope in scopes)
        {
            claims.Add(new Claim(Authorizations.JwtClaimName, scope));
        }
        
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TestConstants.TestKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            TestConstants.TestIssuer,
            TestConstants.TestIssuer,
            claims,
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(1),
            credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public TokenBuilder WithClaim(Claim claim)
    {
        claims.Add(claim);

        return this;
    }

    public TokenBuilder WithScope(string scope)
    {
        scopes.Add(scope);

        return this;
    }
}