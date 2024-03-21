using SagaGuide.Application.Commands.Character;
using MediatR;
using SagaGuide.Api.Queries.Skill;

namespace SagaGuide.Api.MediatRBehavior;

public static class MediatrDependencyInjectionHelper
{
    public static IServiceCollection AddAllNotificationHandlers(
        this IServiceCollection services,
        ServiceLifetime withLifetime = ServiceLifetime.Transient)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(new []{ typeof(CreateCharacterCommand)})
            .AddClasses(classes =>
                classes.AssignableTo(typeof(INotificationHandler<>))
                    .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
            .AsSelfWithInterfaces()
            .WithLifetime(withLifetime)
        );
    }

    public static IServiceCollection AddAllQueryAndCommandsHandlers(
        this IServiceCollection services,
        ServiceLifetime withLifetime = ServiceLifetime.Transient)
    {
        return services.Scan(scan => scan
            .FromAssembliesOf(new []{ typeof(GetSkillsQuery), typeof(CreateCharacterCommand)})
            .AddClasses(classes =>
                classes.AssignableTo(typeof(IRequestHandler<,>))
                    .Where(c => !c.IsAbstract && !c.IsGenericTypeDefinition))
            .AsSelfWithInterfaces()
            .WithLifetime(withLifetime)
        );
    }
}