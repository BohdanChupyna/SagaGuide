using FluentValidation.Results;

namespace SagaGuide.Api.Contract;

public class ResponseBase
{
    public ResponseBase() => Warnings = new List<ValidationFailureResult>();

    public List<ValidationFailureResult> Warnings { get; set; }
}

public class ValidationFailureResult
{
    public ValidationFailureResult()
    {
    }

    public ValidationFailureResult(ValidationFailure validationFailure)
    {
        PropertyName = validationFailure.PropertyName;
        ErrorMessage = validationFailure.ErrorMessage;
        AttemptedValue = validationFailure.AttemptedValue;
        ErrorCode = validationFailure.ErrorCode;
    }

    public object AttemptedValue { get; set; } = null!;
    public string ErrorCode { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
    public string PropertyName { get; set; } = null!;
}