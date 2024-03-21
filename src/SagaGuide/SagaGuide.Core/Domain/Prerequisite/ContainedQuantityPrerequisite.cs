namespace SagaGuide.Core.Domain.Prerequisite;

public class ContainedQuantityPrerequisite : PrerequisiteBase
{
    public IntegerCriteria QualifierCriteria { get; set; } = null!;
    public override IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.ContainedQuantity;
}