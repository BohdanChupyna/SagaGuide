using System.Collections.Generic;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.Infrastructure.JsonConverters;

public class PrerequisiteGroupDefaultJsonConverterTests
{
    [Fact]
    public void Deserialize_ReadCorrectly()
    {
        const string json = @"{
    ""shouldAllBeSatisfied"": false,
    ""prerequisites"": [
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.SkillPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""swimming""
        },
        ""prerequisiteType"": ""Skill"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""amphibious""
        },
        ""prerequisiteType"": ""Trait"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""aquatic""
        },
        ""prerequisiteType"": ""Trait"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.PrerequisiteGroup, SagaGuide.Core"",
        ""shouldAllBeSatisfied"": true,
        ""prerequisites"": [
        {
            ""$type"": ""SagaGuide.Core.Domain.Prerequisite.SkillPrerequisite, SagaGuide.Core"",
            ""nameCriteria"": {
                ""comparison"": ""Contains"",
                ""qualifier"": ""flying""
            },
            ""prerequisiteType"": ""Skill"",
            ""shouldBe"": true
        },
        {
            ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
            ""nameCriteria"": {
                ""comparison"": ""EndsWith"",
                ""qualifier"": ""cat""
            },
            ""prerequisiteType"": ""Trait"",
            ""shouldBe"": true
        }
        ],
        ""prerequisiteType"": ""Group""
    }
    ],
    ""prerequisiteType"": ""Group""
}";

        var jsonSettings = JsonSettingsWrapper.Create();
        var group = JsonConverterWrapper.Deserialize<PrerequisiteGroup>(json, jsonSettings)!;
        group.ShouldAllBeSatisfied.Should().BeFalse();
        group.PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Group);
        group.Prerequisites.Count.Should().Be(4);

        group.Prerequisites[0].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Skill);
        var skill = (SkillPrerequisite)group.Prerequisites[0];
        skill.ShouldBe.Should().BeTrue();
        skill.NameCriteria.Qualifier.Should().Be("swimming");
        skill.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        
        group.Prerequisites[1].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Trait);
        var trait = (TraitPrerequisite)group.Prerequisites[1];
        trait.ShouldBe.Should().BeTrue();
        trait.NameCriteria.Qualifier.Should().Be("amphibious");
        trait.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        
        group.Prerequisites[2].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Trait);
        trait = (TraitPrerequisite)group.Prerequisites[2];
        trait.ShouldBe.Should().BeTrue();
        trait.NameCriteria.Qualifier.Should().Be("aquatic");
        trait.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        
        group.Prerequisites[3].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Group);
        var innerGroup = (PrerequisiteGroup)group.Prerequisites[3];
        innerGroup.ShouldAllBeSatisfied.Should().BeTrue();
        
        skill = (SkillPrerequisite)innerGroup.Prerequisites[0];
        skill.ShouldBe.Should().BeTrue();
        skill.NameCriteria.Qualifier.Should().Be("flying");
        skill.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Contains);
        
        trait = (TraitPrerequisite)innerGroup.Prerequisites[1];
        trait.ShouldBe.Should().BeTrue();
        trait.NameCriteria.Qualifier.Should().Be("cat");
        trait.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.EndsWith);
    }
    
    [Fact]
    public void Serialize_WriteCorrectly()
    {
        IPrerequisite prerequisites = new PrerequisiteGroup
        {
            ShouldAllBeSatisfied = false,
            Prerequisites = new List<IPrerequisite>
            {
                new SkillPrerequisite
                {
                    ShouldBe = true,
                    NameCriteria = new StringCriteria
                    {
                        Comparison = StringCriteria.ComparisonType.Is,
                        Qualifier = "swimming"
                    }
                },
                new TraitPrerequisite
                {
                    ShouldBe = true,
                    NameCriteria = new StringCriteria
                    {
                        Comparison = StringCriteria.ComparisonType.Is,
                        Qualifier = "amphibious"
                    },
                },
                new TraitPrerequisite
                {
                    ShouldBe = true,
                    NameCriteria = new StringCriteria
                    {
                        Comparison = StringCriteria.ComparisonType.Is,
                        Qualifier = "aquatic"
                    },
                },
                new PrerequisiteGroup()
                {
                    ShouldAllBeSatisfied = false,
                    Prerequisites = new List<IPrerequisite>()
                    {
                        new SkillPrerequisite
                        {
                            ShouldBe = true,
                            NameCriteria = new StringCriteria
                            {
                                Comparison = StringCriteria.ComparisonType.Contains,
                                Qualifier = "flying"
                            }
                        },
                        new TraitPrerequisite
                        {
                            ShouldBe = true,
                            NameCriteria = new StringCriteria
                            {
                                Comparison = StringCriteria.ComparisonType.EndsWith,
                                Qualifier = "cat"
                            },
                        },
                    }
                }
            }
        };
        
        var jsonSettings = JsonSettingsWrapper.Create();
        
        var json = JsonConverterWrapper.Serialize(prerequisites, jsonSettings);

        var expectedJson = @"{
    ""shouldAllBeSatisfied"": false,
    ""prerequisites"": [
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.SkillPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""swimming""
        },
        ""prerequisiteType"": ""Skill"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""amphibious""
        },
        ""prerequisiteType"": ""Trait"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
        ""nameCriteria"": {
            ""comparison"": ""Is"",
            ""qualifier"": ""aquatic""
        },
        ""prerequisiteType"": ""Trait"",
        ""shouldBe"": true
    },
    {
        ""$type"": ""SagaGuide.Core.Domain.Prerequisite.PrerequisiteGroup, SagaGuide.Core"",
        ""shouldAllBeSatisfied"": false,
        ""prerequisites"": [
        {
            ""$type"": ""SagaGuide.Core.Domain.Prerequisite.SkillPrerequisite, SagaGuide.Core"",
            ""nameCriteria"": {
                ""comparison"": ""Contains"",
                ""qualifier"": ""flying""
            },
            ""prerequisiteType"": ""Skill"",
            ""shouldBe"": true
        },
        {
            ""$type"": ""SagaGuide.Core.Domain.Prerequisite.TraitPrerequisite, SagaGuide.Core"",
            ""nameCriteria"": {
                ""comparison"": ""EndsWith"",
                ""qualifier"": ""cat""
            },
            ""prerequisiteType"": ""Trait"",
            ""shouldBe"": true
        }
        ],
        ""prerequisiteType"": ""Group""
    }
    ],
    ""prerequisiteType"": ""Group""
}";

        json = json.Replace(" ", "");
        expectedJson = expectedJson.Replace(" ", "");
        json.Length.Should().Be(expectedJson.Length);
        json.Should().Be(expectedJson);
    }
}