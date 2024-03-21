using Microsoft.AspNetCore.Mvc.Filters;

namespace SagaGuide.Api.ExceptionHandling;

internal class ExceptionHandlingFilterAttribute : ExceptionFilterAttribute
{
    private readonly ExceptionHandlingConfiguration _exceptionHandlingConfiguration;
    private readonly ILogger<ExceptionHandlingFilterAttribute> _logger;

    public ExceptionHandlingFilterAttribute(ExceptionHandlingConfiguration exceptionHandlingConfiguration, ILogger<ExceptionHandlingFilterAttribute> logger)
    {
        _exceptionHandlingConfiguration = exceptionHandlingConfiguration;
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var contextConfigureAction = _exceptionHandlingConfiguration.GetHandler(context.Exception.GetType());
        contextConfigureAction(context);
        _logger.LogError(context.Exception, "Exception accurate.");

        base.OnException(context);
    }
}