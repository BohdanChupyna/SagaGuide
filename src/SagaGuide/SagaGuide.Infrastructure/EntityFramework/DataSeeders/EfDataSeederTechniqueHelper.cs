using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public static class EfDataSeederTechniqueHelper
{
    public static List<Technique> GetTechniques()
    {
        var techniques = new List<Technique>
        {
            new Technique
            {
                Id = TechniqueIds.NeckSnapId,
                Name = "Neck Snap",
                Tags = new List<string>
                {
                    "Combat",
                    "Melee Combat",
                    "Technique", 
                    "Weapon"
                },
                BookReferences = new List<BookReference>
                {
                    new BookReference
                    {
                        PageNumber = 232,
                        SourceBook = BookReference.SourceBookEnum.BasicSet
                    },
                    new BookReference
                    {
                        PageNumber = 77,
                        SourceBook = BookReference.SourceBookEnum.MartialArts
                    }
                },
                DifficultyLevel = Skill.DifficultyLevelEnum.Hard,
                PointsCost = 2,
                Default = new SkillDefault
                {
                    AttributeType = Attribute.AttributeType.Strength,
                    Modifier = -4
                },
                TechniqueLimitModifier = 3,
                Prerequisites = null
            },
            new Technique
            {
                Id = Guid.Parse("8105c2fa-ce51-41e7-8be7-ff194166a226"),
                Name = "Retain Weapon",
                Tags = new List<string>
                {
                    "Combat",
                    "Melee Combat",
                    "Technique",
                    "Weapon"
                },
                BookReferences = new List<BookReference>
                {
                    new BookReference
                    {
                        PageNumber = 232,
                        SourceBook = BookReference.SourceBookEnum.BasicSet
                    },
                    new BookReference
                    {
                        PageNumber = 78,
                        SourceBook = BookReference.SourceBookEnum.MartialArts
                    }
                },
                DifficultyLevel = Skill.DifficultyLevelEnum.Hard,
                PointsCost = 2,
                Default = new SkillDefault
                {
                    Name = "@Melee Weapon Skill@"
                },
                TechniqueLimitModifier = 5,
                Prerequisites = null
            },
            new Technique
            {
                Id = Guid.Parse("9eb76547-3426-46e2-94a9-2744ddd870a0"),
                Name = "Retain Weapon (@Ranged Weapon@)",
                Tags = new List<string>
                {
                    "Combat",
                    "Melee Combat",
                    "Technique",
                    "Weapon"
                },
                BookReferences = new List<BookReference>
                {
                    new BookReference
                    {
                        PageNumber = 232,
                        SourceBook = BookReference.SourceBookEnum.BasicSet
                    },
                    new BookReference
                    {
                        PageNumber = 78,
                        SourceBook = BookReference.SourceBookEnum.MartialArts
                    }
                },
                DifficultyLevel = Skill.DifficultyLevelEnum.Hard,
                PointsCost = 2,
                Default = new SkillDefault
                {
                    AttributeType = Attribute.AttributeType.Dexterity
                },
                TechniqueLimitModifier = 5,
                Prerequisites = null
            },
            new Technique
            {
                Id = TechniqueIds.OffHandWeaponTrainingId,
                Name = "Off-Hand Weapon Training",
                Tags = new List<string>
                {
                    "Combat",
                    "Ranged Combat",
                    "Technique",
                    "Weapon"
                },
                BookReferences = new List<BookReference>
                {
                    new BookReference
                    {
                        PageNumber = 232,
                        SourceBook = BookReference.SourceBookEnum.BasicSet
                    }
                },
                DifficultyLevel = Skill.DifficultyLevelEnum.Hard,
                PointsCost = 2,
                Default = new SkillDefault
                {
                    Name = "Guns",
                    Specialization = "Pistol",
                    Modifier = -4,
                },
                TechniqueLimitModifier = 0,
                Prerequisites = null
            }
        };

        return techniques;
    }
}
