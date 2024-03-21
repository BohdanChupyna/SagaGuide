using SagaGuide.Application.Commands.Character;
using MediatR;
using SagaGuide.Api.MediatRBehavior;
using Serilog;

namespace SagaGuide.Api;

public static class BootstrapExtensions
{
    public static void AddLogging(this ILoggingBuilder loggingBuilder, IConfiguration configuration)
    {
        loggingBuilder.AddSerilog(dispose: true);
    }

    /// <summary>
    ///     Register all MediatR RequestHandlers and NotificationHandlers
    /// </summary>
    /// <param name="services"></param>
    public static void AddMediatrHandlers(this IServiceCollection services)
    {
        services.AddMediatR(ConfigureMediatr);

        services.AddAllNotificationHandlers();
        services.AddAllQueryAndCommandsHandlers();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
    }
    
    private static void ConfigureMediatr(MediatRServiceConfiguration configuration)
    {
        configuration.RegisterServicesFromAssemblyContaining<CreateCharacterCommand>();
    }
}