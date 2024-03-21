using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Api.Contract.Skill;

public class SkillViewModel : AuditableViewModel
{
    public enum DifficultyLevelEnumViewModel
    {
        Easy,
        Average,
        Hard,
        VeryHard,
        Wildcard
    }
    public string Name { get; set; } = null!;
    public List<string> Tags { get; set; } = new();

    //if specialization contains value in @ it means that user should provide specialization 
    public string? Specialization { get; set; }
    public int? TechLevel { get; set; }
    public DifficultyLevelEnumViewModel DifficultyLevel { get; set; }
    public int PointsCost { get; set; }
    public int? EncumbrancePenaltyMultiplier { get; set; }
    public List<SkillDefault> Defaults { get; set; } = new();
    public Core.Domain.Attribute.AttributeType AttributeType { get; set; }
    public PrerequisiteGroup? Prerequisites { get; set; }
    public List<BookReference> BookReferences { get; set; } = new();
    public string? LocalNotes { get; set; }
}