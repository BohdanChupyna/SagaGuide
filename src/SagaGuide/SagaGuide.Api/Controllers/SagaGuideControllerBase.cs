using System.Net;
using SagaGuide.Core;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SagaGuide.Api.Contract;

namespace SagaGuide.Api.Controllers;

[EnableCors]
public class SagaGuideControllerBase : ControllerBase
{
    protected IActionResult RequestForValidationResult(ValidationResult result, Func<object> okValueMapper)
    {
        var errors = result.Errors.Where(x => x.Severity == Severity.Error).ToList();
        if (errors.Any()) return RequestForValidationResult(result);
        return RequestForValidationResult(result, okValueMapper());
    }

    protected IActionResult RequestForValidationResult(ValidationResult result, object? okValue = null)
    {
        var errors = result.Errors.Where(x => x.Severity == Severity.Error).ToList();
        if (errors.Any())
        {
            foreach (var error in errors) ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            return BadRequest(ModelState);
        }

        var warnings = result.Errors.Where(e => e.Severity == Severity.Warning).ToList();
        if (okValue != null)
        {
            if (okValue is ResponseBase) ((ResponseBase)okValue).Warnings = warnings.Select(x => new ValidationFailureResult(x)).ToList();
            return Ok(okValue);
        }

        return Ok(new
        {
            warnings
        });
    }

    protected IActionResult ActionResultForCommandResponse<T>(CommandResponse<T> response)
    {
        if (response.Messages.Count != 1)
            return BadRequest(response);
        
        var message = response.Messages.Single();
        return message.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(response.Value),
            StatusCodes.Status201Created => Created(Request.GetDisplayUrl(), response.Value),
            StatusCodes.Status204NoContent => NoContent(),
            StatusCodes.Status404NotFound => NotFound(message.Message),
            _ => BadRequest(response)
        };
        
    }
}