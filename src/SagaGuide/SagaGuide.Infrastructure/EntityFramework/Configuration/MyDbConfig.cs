namespace SagaGuide.Infrastructure.EntityFramework.Configuration;

public class MyDbConfig
{
    public const string ConfigSection = "MyDb";

    public string ConnectionString()
    {
        if (string.IsNullOrEmpty(SslMode))
        {
            return $"Host={Host};Port={Port};Database={DbName};Username={UserName};Password={Password};";    
        }
       
        return $"Host={Host};Port={Port};Database={DbName};Username={UserName};Password={Password};SSL Mode={SslMode};Trust Server Certificate=true;";
    }

    public string DbName { get; init; } = null!;

    public string Host { get; init; } = null!;

    public string? Password { get; init; }

    public int Port { get; init; }
    
    public string? SslMode { get; init; }
    
    public string? UserName { get; init; }
    public string? GcsMasterLibraryPath { get; init; }
}