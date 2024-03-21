using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SagaGuide.Api.ExceptionHandling;

public class ExceptionHandlingConfiguration
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>();

    public ExceptionHandlingConfiguration()
    {
        MapInternalServerException<Exception>();
        MapException<ValidationException>(StatusCodes.Status400BadRequest, exception => exception.ToValidationProblemDetails());
    }

    public Action<ExceptionContext> GetHandler(Type exceptionType)
    {
        if (!_exceptionHandlers.TryGetValue(exceptionType, out var contextConfigureAction))
            contextConfigureAction = _exceptionHandlers[typeof(Exception)];

        return contextConfigureAction;
    }

    public ExceptionHandlingConfiguration MapException<TException>(int httpStatusCode,
        Func<TException, ValidationProblemDetails> validationProblemDetailsFactory)
        where TException : Exception =>
        MapException(httpStatusCode, (Func<TException, ProblemDetails>)validationProblemDetailsFactory);

    public ExceptionHandlingConfiguration MapException<TException>(int httpStatusCode, Func<TException, ProblemDetails> problemDetailsFactory)
        where TException : Exception
    {
        _exceptionHandlers.Add(typeof(TException), context =>
        {
            context.Result = new ObjectResult(problemDetailsFactory((TException)context.Exception)) { StatusCode = httpStatusCode };
            context.ExceptionHandled = true;
        });
        return this;
    }

    private ExceptionHandlingConfiguration MapInternalServerException<TException>() where TException : Exception => MapException<TException>(
        StatusCodes.Status500InternalServerError, _ => new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred while processing your request."
        });
}