using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using ILogger = Serilog.ILogger;

namespace SagaGuide.Api;

public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
    {
        var host = Host.CreateDefaultBuilder(args);

        host.ConfigureAppConfiguration(x => x.AddConfiguration(configuration));
        host.ConfigureLogging((context, builder) => builder.AddLogging(context.Configuration));

        host.ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .ConfigureKestrel((context, options) =>
                {
                    var port = context.Configuration.GetValue<int>("PORT");
                    options.ListenAnyIP(port, listenOptions =>
                        listenOptions.Protocols = HttpProtocols.Http1);

                    // var grpcPort = context.Configuration.GetValue<int>("GRPC_PORT");
                    // options.ListenAnyIP(grpcPort, listenOptions =>
                    //     listenOptions.Protocols = HttpProtocols.Http2);
                })
                .UseStartup<Startup>();
        });
        return host;
    }

    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();
        Log.Logger = CreateSerilogLogger(configuration);

        try
        {
            var host = CreateHostBuilder(args, configuration).Build();
            Log.Information("Starting web host ...");
            host.Run();
        }
        catch (Exception ex)
        {
            var type = ex.GetType().Name;
            if (type.Equals("HostAbortedException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(ex, "Program terminated unexpectedly!");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static ILogger CreateSerilogLogger(IConfiguration configuration) =>
        new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

    private static IConfiguration GetConfiguration()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{environment}.json", true, true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}