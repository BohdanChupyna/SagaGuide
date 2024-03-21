using System.Diagnostics;
using SagaGuide.Infrastructure.JsonConverters;
using MediatR;

namespace SagaGuide.Api.MediatRBehavior;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        sw.Start();
        _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
        var response = await next();
        sw.Stop();
        var responseJson = JsonConverterWrapper.Serialize(response, JsonSettingsWrapper.CreateNewtonsoftSettingsForMediatorLoggingBehavior());
        _logger.LogInformation("----- Command {CommandName} handled - response: {Response} - time(ms): {ElapsedMilliseconds}",
            request.GetGenericTypeName(), responseJson.Substring(0, responseJson.Length < 200 ? responseJson.Length : 200), sw.ElapsedMilliseconds);
        return response;
    }
}