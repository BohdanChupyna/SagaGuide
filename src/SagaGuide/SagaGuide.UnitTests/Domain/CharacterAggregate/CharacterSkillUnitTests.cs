// using SagaGuide.Core.Domain;
// using SagaGuide.Core.Domain.CharacterAggregate;
// using SagaGuide.Core.Domain.SkillAggregate;
// using FluentAssertions;
// using System.Collections.Generic;
// using Xunit;
//
// namespace SagaGuide.UnitTests.Domain.CharacterAggregate
// {
//     public class CharacterSkillUnitTests
//     {
//         [Theory]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 1, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 2, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 3, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 4, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 6, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 8, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 12, 14)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 16, 15)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 1, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 2, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 3, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 4, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 6, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 8, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 12, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 16, 14)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 1, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 2, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 3, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 4, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 6, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 8, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 12, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 16, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 1, 7)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 2, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 3, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 4, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 6, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 8, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 12, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 16, 12)]
//         public void SkillLevel_ForBasicAttributeOfTen_ComputesCorrectly(Skill.DifficultyLevelEnum difficultyLevel, int spentPoints, int expectedValue)
//         {
//             var characterSkill = CreateCharacterSkillAttributeBased(difficultyLevel, 10, spentPoints);
//
//             characterSkill.SkillLevel.Should().Be(expectedValue);
//         }
//
//         [Theory]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 1, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 2, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 3, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 4, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 6, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 8, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 12, 14)]
//         [InlineData(Skill.DifficultyLevelEnum.Easy, 16, 15)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 1, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 2, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 3, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 4, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 6, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 8, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 12, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.Average, 16, 14)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 1, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 2, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 3, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 4, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 6, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 8, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 12, 12)]
//         [InlineData(Skill.DifficultyLevelEnum.Hard, 16, 13)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 1, 7)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 2, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 3, 8)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 4, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 6, 9)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 8, 10)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 12, 11)]
//         [InlineData(Skill.DifficultyLevelEnum.VeryHard, 16, 12)]
//         public void SkillLevel_ForSecondaryCharacteristicsOfTen_ComputesCorrectly(Skill.DifficultyLevelEnum difficultyLevel, int spentPoints, int expectedValue)
//         {
//             var characterSkill = CreateCharacterSkillSecondaryCharacteristicBased(difficultyLevel, 10, spentPoints);
//
//             characterSkill.SkillLevel.Should().Be(expectedValue);
//         }
//
//         private CharacterSkill CreateCharacterSkillAttributeBased(Skill.DifficultyLevelEnum difficultyLevel, int attributeValue, int spentPoints)
//         {
//             var basicAttribute = new Attribute { Type = Attribute.AttributeType.Strength, DefaultValue = attributeValue, PointsCostPerLevel = 1 };
//
//             var attribute = new CharacterAttribute();
//             attribute.Attribute = basicAttribute;
//
//             var character = new Character();
//             character.Attributes = new List<CharacterAttribute> { attribute };
//
//             var skill = new Skill();
//             skill.DifficultyLevel = difficultyLevel;
//             skill.AttributeType = basicAttribute.Type;
//
//             var characterSkill = new CharacterSkill();
//             characterSkill.Skill = skill;
//             characterSkill.Character = character;
//             characterSkill.SpentPoints = spentPoints;
//
//             return characterSkill;
//         }
//
//         private CharacterSkill CreateCharacterSkillSecondaryCharacteristicBased(Skill.DifficultyLevelEnum difficultyLevel, int attributeValue, int spentPoints)
//         {
//             var characterSkill = CreateCharacterSkillAttributeBased(difficultyLevel, attributeValue, spentPoints);
//
//             var attribute = new Attribute()
//             {
//                 DependOnAttributeType = Attribute.AttributeType.Strength,
//                 PointsCostPerLevel = 1,
//                 Type = Attribute.AttributeType.Damage,
//             };
//
//             var characterAttribute = new CharacterAttribute
//             {
//                 Attribute = attribute,
//                 Character = characterSkill.Character
//             };
//
//             var skill = new Skill
//             {
//                 DifficultyLevel = difficultyLevel,
//                 AttributeType = attribute.Type
//             };
//
//             characterSkill.Skill = skill;
//             characterSkill.Character.Attributes = new List<CharacterAttribute> { characterAttribute };
//
//             return characterSkill;
//         }
//     }
// }
