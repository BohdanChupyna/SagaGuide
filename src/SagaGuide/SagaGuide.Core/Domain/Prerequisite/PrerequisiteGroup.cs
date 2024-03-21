namespace SagaGuide.Core.Domain.Prerequisite;

public class PrerequisiteGroup: IPrerequisite
{
    public IntegerCriteria? WhenTechLevel { get; set; }
    public bool ShouldAllBeSatisfied { get; set; }
    public List<IPrerequisite> Prerequisites { get; set; } = new();
    public IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => IPrerequisite.PrerequisiteTypeEnum.Group;
}