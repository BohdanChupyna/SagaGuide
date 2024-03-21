namespace SagaGuide.Api.Configuration;

public class AuthConfig
{
    public const string ConfigSection = "Auth";

    public string TestKey { get; set; } = string.Empty;
    public string JwtPublicKey { get; set; } = string.Empty;

    public string ExternalIssuer { get; set; } = string.Empty;

    public string InternalIssuer { get; set; } = string.Empty;
}