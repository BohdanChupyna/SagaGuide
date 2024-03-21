namespace SagaGuide.Core.Domain.Prerequisite;

public class DoubleCriteria
{
    public enum ComparisonType
    {
        Any,
        Equal,
        NotEqual,
        AtLeast,
        AtMost
    }

    public ComparisonType Comparison { get; set; }
    public double Qualifier { get; set; }
}