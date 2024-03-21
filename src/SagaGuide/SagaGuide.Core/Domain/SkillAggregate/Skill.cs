using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.SkillAggregate;

public class Skill : AuditableEntity
{
    public enum DifficultyLevelEnum
    {
        Easy,
        Average,
        Hard,
        VeryHard,
        Wildcard
    }

    public string Name { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();

    //if specialization contains value in @ it means that user should provide specialization 
    public string? Specialization { get; set; }
    public int? TechLevel { get; set; }
    public DifficultyLevelEnum DifficultyLevel { get; set; } = DifficultyLevelEnum.Easy;
    public int PointsCost { get; set; }
    public int? EncumbrancePenaltyMultiplier { get; set; }
    public List<SkillDefault> Defaults { get; set; } = new();
    public Attribute.AttributeType AttributeType { get; set; }
    public PrerequisiteGroup? Prerequisites { get; set; }
    public List<BookReference> BookReferences { get; set; } = new();
    public string? LocalNotes { get; set; }
}