namespace SagaGuide.Core.Domain.Common;

public interface IIdentifiable<out T> : IIdentifiable
    where T : IEquatable<T>
{
    new T Id { get; }
}

public interface IIdentifiable
{
    object Id { get; }
}