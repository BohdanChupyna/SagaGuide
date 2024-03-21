namespace SagaGuide.Core.Domain.EnvironmentAbstractions;

public interface IUserAccessor
{
    Guid GetCurrentUserId();
    string GetCurrentUserName();
}