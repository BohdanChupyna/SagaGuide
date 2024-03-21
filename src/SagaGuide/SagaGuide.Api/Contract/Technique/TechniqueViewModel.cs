using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Api.Contract.Skill;

namespace SagaGuide.Api.Contract.Technique;

public class TechniqueViewModel : AuditableViewModel
{
    public string Name { get; set; } = null!;
    public List<string> Tags { get; set; } = new ();
    public List<BookReference> BookReferences { get; set; } = new ();
    public SkillViewModel.DifficultyLevelEnumViewModel DifficultyLevel { get; set; }
    public int PointsCost { get; set; }
    public SkillDefault Default { get; set; }  = null!;
    public int? TechniqueLimitModifier { get; set; }
    public PrerequisiteGroup? Prerequisites { get; set; }
}