using Microsoft.AspNetCore.Http;

namespace SagaGuide.Core;

public class CommandResponseBase
{
    public List<CommandResponseMessage> Messages { get; init; } = new();
    public bool IsSuccess => Messages.All(m => m.IsOk);

    public void AddError(int statusCode, string message) => Messages.Add(CommandResponseMessage.Error(statusCode, message));
    public void AddInformation(int statusCode, string message = "") => Messages.Add(CommandResponseMessage.Information(statusCode, message));
}

public class CommandResponse<T> : CommandResponseBase
{
    public T Value { get; set; } = default!;
}

public class CommandResponseMessage
{
    public enum MessageTypeEnum
    {
        Information,
        Warning,
        Error
    }
    
    public MessageTypeEnum Type { get; set; }
    public string Message { get; init; } = null!;
    public int StatusCode { get; init; }

    public static CommandResponseMessage Error(int statusCode, string message)
    {
        return new CommandResponseMessage
        {
            Type = MessageTypeEnum.Error,
            Message = message,
            StatusCode = statusCode
        };
    }
    
    public static CommandResponseMessage Information(int statusCode, string message = "")
    {
        return new CommandResponseMessage
        {
            Type = MessageTypeEnum.Information,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static CommandResponseMessage Ok()
    {
        return new CommandResponseMessage
        {
            Type = MessageTypeEnum.Information,
            Message = string.Empty,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public bool IsOk => StatusCode is StatusCodes.Status200OK or StatusCodes.Status201Created or StatusCodes.Status204NoContent;
}