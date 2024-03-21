namespace SagaGuide.Core.Domain.Prerequisite;

public class ContainedWeightPrerequisite : PrerequisiteBase
{
    public DoubleCriteria QualifierCriteria { get; set; } = null!;
    public override IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.ContainedWeight;
}