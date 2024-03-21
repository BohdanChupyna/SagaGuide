using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterSkill : GuidEntity
{
    public Skill Skill { get; set; } = null!;
    public int SpentPoints { get; set; }
    public string? OptionalSpecialty { get; set; }
    public SkillDefault? DefaultedFrom { get; set; } = null!;
    // public int SkillLevel => ComputeSkillLevel();
    //
    // private int ComputeSkillLevel()
    // {
    //     // see page 170 GURPS 4th ed Basic set
    //
    //     var attributeValue = GetSkillAttributeValue();
    //     var penalty = SkillDifficultyPenalty();
    //
    //     if (SpentPoints < 4)
    //     {
    //         return attributeValue + penalty + SpentPoints / 2;
    //     }
    //
    //     return attributeValue + penalty + 1 + SpentPoints / 4;
    // }
    //
    // private int GetSkillAttributeValue()
    // {
    //     
    //         var attribute = Skill.AttributeType;
    //         return (int)Math.Floor(Character.Attributes.Single(x => x.Attribute.Type == Skill.AttributeType).Value);
    // }
    //
    // private int SkillDifficultyPenalty()
    // {
    //     return Skill.DifficultyLevel switch
    //     {
    //         Skill.DifficultyLevelEnum.Easy => 0,
    //         Skill.DifficultyLevelEnum.Average => -1,
    //         Skill.DifficultyLevelEnum.Hard => -2,
    //         Skill.DifficultyLevelEnum.VeryHard => -3,
    //         _ => throw new Exception($"Unknown Skill Difficulty level {Skill.DifficultyLevel}")
    //     };
    // }
}