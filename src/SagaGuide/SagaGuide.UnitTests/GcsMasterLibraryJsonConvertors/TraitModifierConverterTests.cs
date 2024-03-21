using System.Linq;
using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class TraitModifierConverterTests
{
    [Fact]
    public void TraitModifier_AllPropertiesAreDeserialized()
    {
        const string json = @"
            {
                ""id"": ""d47eeba8-96e5-4fcd-8455-1c125ca554a5"",
                ""type"": ""modifier"",
                ""name"": ""DX penalty if not wearing custom-made clothing and armor"",
                ""reference"": ""B139"",
                ""notes"": ""Clothing and armor costs 10% more than usual"",
                ""features"": [
                    {
                        ""type"": ""attribute_bonus"",
                        ""attribute"": ""dx"",
                        ""amount"": -1
                    }
                ],
                ""cost"": 20,
                ""cost_type"": ""percentage"",
                ""affects"": ""base_only"",
                ""tags"": [
				    ""Mental"",
				    ""Quirk""
			    ],
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var modifier = JsonConverterWrapper.Deserialize<TraitModifier>(json, jsonSettings)!;

        modifier.Id.Should().Be("d47eeba8-96e5-4fcd-8455-1c125ca554a5");
        modifier.Name.Should().Be("DX penalty if not wearing custom-made clothing and armor");
        modifier.BookReferences.Single().SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        modifier.BookReferences.Single().PageNumber.Should().Be(139);
        modifier.LocalNotes.Should().Be("Clothing and armor costs 10% more than usual");
        modifier.CostType.Should().Be(TraitModifier.CostTypeEnum.Percentage);
        modifier.PointsCost.Should().Be(20);
        modifier.CostAffectType.Should().Be(TraitModifier.CostAffectTypeEnum.BaseOnly);

        modifier.Tags.Count.Should().Be(2);
        modifier.Tags[0].Should().Be("Mental");
        modifier.Tags[1].Should().Be("Quirk");
        
        var feature = modifier.Features.Single() as AttributeBonusFeature;
        feature.Should().NotBeNull();
        feature!.AttributeType.Should().Be(Attribute.AttributeType.Dexterity);
        feature.Amount.Should().Be(-1);
    }
    
    [Fact]
    public void TraitModifier_WithoutOptionalProperties_DefaultValuesAreSet()
    {
        const string json = @"
            {
                ""id"": ""d47eeba8-96e5-4fcd-8455-1c125ca554a5"",
                ""type"": ""modifier"",
                ""name"": ""DX penalty if not wearing custom-made clothing and armor"",
              
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var modifier = JsonConverterWrapper.Deserialize<TraitModifier>(json, jsonSettings)!;

        modifier.Id.Should().Be("d47eeba8-96e5-4fcd-8455-1c125ca554a5");
        modifier.Name.Should().Be("DX penalty if not wearing custom-made clothing and armor");
        modifier.BookReferences.Count.Should().Be(0);
        modifier.LocalNotes.Should().BeNull();
        modifier.CostType.Should().Be(TraitModifier.CostTypeEnum.Points);
        modifier.PointsCost.Should().Be(0);
        modifier.CostAffectType.Should().Be(TraitModifier.CostAffectTypeEnum.Total);
        modifier.Features.Count.Should().Be(0);
        modifier.Tags.Count.Should().Be(0);
        modifier.CanLevel.Should().BeFalse();
    }
}