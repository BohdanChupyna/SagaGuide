namespace SagaGuide.Api.Constants;

/// <summary>
///     The token authorization scopes for the endpoints on the controller.
/// </summary>
public static class Authorizations
{
    /// <summary>
    ///     All scopes.
    /// </summary>
    public static string[] All = { ReadGurpsRules, ReadCharacters, WriteCharacter };

    /// <summary>
    ///     Calculations read only scope.
    /// </summary>
    
    public const string ReadGurpsRules = "read:gurpsrules";
    public const string ReadCharacters = "read:characters";
    public const string WriteCharacter = "write:characters";

    public const string MaintenanceId = "86780538-63e3-4694-8c82-6a38d56aebc6";
    public const string JwtClaimName = "gmsclaims";

}