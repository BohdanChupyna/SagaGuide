namespace SagaGuide.Core.Domain.Prerequisite;

public class StringCriteria
{
    public enum ComparisonType
    {
        Any,
        Is,
        IsNot,
        Contains,
        DoesNotContain,
        StartsWith,
        DoesNotStartWith,
        EndsWith,
        DoesNotEndWith,
    }
    
    public ComparisonType Comparison { get; set; }
    public string Qualifier { get; set; } = null!;
}

