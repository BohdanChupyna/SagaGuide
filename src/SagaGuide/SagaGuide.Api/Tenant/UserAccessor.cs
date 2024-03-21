using System.Security.Claims;
using SagaGuide.Core.Domain.EnvironmentAbstractions;

namespace SagaGuide.Api.Tenant;

public class UserAccessor : IUserAccessor
{
    private readonly ClaimsPrincipal _claimsPrincipal;

    public UserAccessor(ClaimsPrincipal claimsPrincipal) => _claimsPrincipal = claimsPrincipal;

    public Guid GetCurrentUserId()
    {
        var userIdClaim = _claimsPrincipal.Claims
            ?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase))?.Value;
        Guid.TryParse(userIdClaim, out var result);
        return result;
    }

    public string GetCurrentUserName()
    {
        var userClaim = _claimsPrincipal.Claims
            ?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))?.Value;

        if (string.IsNullOrEmpty(userClaim))
        {
            userClaim = _claimsPrincipal.Claims
                ?.FirstOrDefault(x => x.Type.ToString().Equals("preferred_username", StringComparison.OrdinalIgnoreCase))?.Value;


            if (string.IsNullOrEmpty(userClaim))
                userClaim = _claimsPrincipal.Claims
                    ?.FirstOrDefault(x => x.Type.ToString().Equals("username", StringComparison.OrdinalIgnoreCase))?.Value;

            if (string.IsNullOrEmpty(userClaim)) return "SYSTEM";
        }

        return userClaim;
    }
}