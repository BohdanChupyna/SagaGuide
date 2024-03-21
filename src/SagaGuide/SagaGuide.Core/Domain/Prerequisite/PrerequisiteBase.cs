using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Core.Domain.Prerequisite;

public class PrerequisiteBase : IPrerequisite
{
    public bool ShouldBe { get; set; }
    public virtual IPrerequisite.PrerequisiteTypeEnum PrerequisiteType => throw new NotImplementedException();
}