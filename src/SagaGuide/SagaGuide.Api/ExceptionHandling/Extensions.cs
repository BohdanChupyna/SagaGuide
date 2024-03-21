using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SagaGuide.Api.ExceptionHandling;

public static class Extensions
{
    public static void AddExceptionHandling(this IServiceCollection services, Action<ExceptionHandlingConfiguration>? configureAction = null)
    {
        var configuration = new ExceptionHandlingConfiguration();
        configureAction?.Invoke(configuration);

        services.AddSingleton(configuration);
    }

    public static ValidationProblemDetails ToValidationProblemDetails(this ValidationException exception)
    {
        return new ValidationProblemDetails(exception.Errors.GroupBy(failure => failure.PropertyName, failure => failure.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()));
    }

    public static MvcOptions UseExceptionHandling(this MvcOptions options)
    {
        options.Filters.Add<ExceptionHandlingFilterAttribute>();

        return options;
    }
}