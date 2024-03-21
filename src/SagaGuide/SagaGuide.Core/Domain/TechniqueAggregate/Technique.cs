using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Core.Domain.TechniqueAggregate;

public class Technique : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new ();
    public List<BookReference> BookReferences { get; set; } = new ();
    public Skill.DifficultyLevelEnum DifficultyLevel { get; set; } = Skill.DifficultyLevelEnum.Easy;
    public int PointsCost { get; set; }
    public SkillDefault Default { get; set; }  = null!;
    public int? TechniqueLimitModifier { get; set; }
    public PrerequisiteGroup? Prerequisites { get; set; }
}