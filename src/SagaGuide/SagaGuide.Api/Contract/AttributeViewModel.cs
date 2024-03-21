using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Api.Contract;

public class AttributeViewModel : GuidViewModel
{
    public double? DefaultValue { get; init; }
    public Core.Domain.Attribute.AttributeType Type { get; init; }
    public int PointsCostPerLevel { get; init; }
    public double ValueIncreasePerLevel { get; init; }
    
    public Core.Domain.Attribute.AttributeType? DependOnAttributeType { get; init; }
    public BookReference BookReference { get; init; } = null!;
}