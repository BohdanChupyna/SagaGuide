namespace SagaGuide.Core.Domain.Prerequisite;

public class IntegerCriteria
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
    public int Qualifier { get; set; }
}