using Microsoft.AspNetCore.Authorization;
using SagaGuide.Api.Constants;

namespace SagaGuide.Api.Authorization;

/// <summary>
///     The authorization handler responsible for the evaluation of the requirement's properties. The authorization handler
///     evaluates the requirements against a provided AuthorizationHandlerContext to determine if access is allowed.
///     https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies.
/// </summary>
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == Authorizations.JwtClaimName && c.Issuer == requirement.Issuer)) 
            return Task.CompletedTask;
        
        var gmsClaims = context.User.FindAll(c => c.Type == Authorizations.JwtClaimName && c.Issuer == requirement.Issuer);

        var l = gmsClaims.ToList(); 
        
        // if claim present set requirement satisfied. 
        if (gmsClaims.Any(s => s.Value == requirement.Scope)) 
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}