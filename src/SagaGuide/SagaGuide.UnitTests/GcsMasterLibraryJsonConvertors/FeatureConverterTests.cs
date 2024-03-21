using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class FeatureConverterTests
{
    [Fact]
    public void AttributeBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""attribute_bonus"",
                ""limitation"": ""striking_only"",
                ""attribute"": ""st"",
                ""amount"": 1,
                ""per_level"": true
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(AttributeBonusFeature));

        var feature = (AttributeBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.AttributeBonus);
        feature.AttributeType.Should().Be(Attribute.AttributeType.Strength);
        feature.Amount.Should().Be(1);
        feature.BonusLimitation.Should().Be(AttributeBonusFeature.BonusLimitationEnum.StrikingOnly);
        feature.IsScalingWithLevel.Should().BeTrue();
    }
    
    [Fact]
    public void ConditionalModifierBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""conditional_modifier"",
				""situation"": ""to Vision rolls to spot items within 1 yd"",
				""amount"": -6
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(ConditionalModifierBonusFeature));

        var feature = (ConditionalModifierBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.ConditionalModifierBonus);
        feature.Situation.Should().Be("to Vision rolls to spot items within 1 yd");
        feature.Amount.Should().Be(-6);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void DamageReductionBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""dr_bonus"",
			    ""location"": ""eye"",
			    ""amount"": 1,
			    ""per_level"": true
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(DamageReductionBonusFeature));

        var feature = (DamageReductionBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.DamageReductionBonus);
        feature.Location.Should().Be("eye");
        feature.Amount.Should().Be(1);
        feature.IsScalingWithLevel.Should().BeTrue();
    }
    
    [Fact]
    public void ReactionBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""reaction_bonus"",
                ""situation"": ""from others"",
                ""amount"": 4,
                ""per_level"": false
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(ReactionBonusFeature));

        var feature = (ReactionBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.ReactionBonus);
        feature.Situation.Should().Be("from others");
        feature.Amount.Should().Be(4);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void SkillBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""skill_bonus"",
                ""selection_type"": ""skills_with_name"",
                ""name"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""navigation""
                },
                ""specialization"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""hyperspace""
                },
                ""amount"": 2
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(SkillBonusFeature));

        var feature = (SkillBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.SkillBonus);
        feature.SkillSelectionType.Should().Be(SkillBonusFeature.SkillSelectionTypeEnum.SkillsWithName);
        feature.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.NameCriteria.Qualifier.Should().Be("navigation");
        feature.SpecializationCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.SpecializationCriteria!.Qualifier.Should().Be("hyperspace");
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(2);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void SkillBonusFeature_ThisWeapon_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""skill_bonus"",
                ""selection_type"": ""this_weapon"",
                ""name"": {
                    ""compare"": ""is""
                },
                ""amount"": -1,
                ""per_level"": true
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(SkillBonusFeature));

        var feature = (SkillBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.SkillBonus);
        feature.SkillSelectionType.Should().Be(SkillBonusFeature.SkillSelectionTypeEnum.ThisWeapon);
        feature.NameCriteria.Should().BeNull();
        feature.SpecializationCriteria.Should().BeNull();
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(-1);
        feature.IsScalingWithLevel.Should().BeTrue();
    }
    
    [Fact]
    public void SkillPointBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""skill_point_bonus"",
                ""name"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""navigation""
                },
                ""specialization"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""hyperspace""
                },
                ""amount"": 2
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(SkillPointBonusFeature));

        var feature = (SkillPointBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.SkillPointBonus);
        feature.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.NameCriteria.Qualifier.Should().Be("navigation");
        feature.SpecializationCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.SpecializationCriteria!.Qualifier.Should().Be("hyperspace");
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(2);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void SpellBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""spell_bonus"",
                ""match"": ""college_name"",
                ""name"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""unholy""
                },
                ""amount"": 1
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(SpellBonusFeature));

        var feature = (SpellBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.SpellBonus);
        feature.NameCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.NameCriteria!.Qualifier.Should().Be("unholy");
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(1);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void SpellPointBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""spell_point_bonus"",
                ""match"": ""college_name"",
                ""name"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""unholy""
                },
                ""amount"": 1
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(SpellPointBonusFeature));

        var feature = (SpellPointBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.SpellPointBonus);
        feature.NameCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.NameCriteria!.Qualifier.Should().Be("unholy");
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(1);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void WeaponBonusFeature_ThisWeapon_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""weapon_bonus"",
                ""selection_type"": ""this_weapon"",
                ""name"": {
	                ""compare"": ""is""
                },
                ""level"": {
	                ""compare"": ""at_least""
                },
                ""amount"": -1,
                ""per_level"": true
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(WeaponBonusFeature));

        var feature = (WeaponBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.WeaponBonus);
        feature.WeaponSelectionType.Should().Be(WeaponBonusFeature.WeaponSelectionTypeEnum.ThisWeapon);
        feature.NameCriteria.Should().BeNull();
        feature.RelativeLevelCriteria.Should().BeNull();
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(-1);
        feature.IsScalingWithLevel.Should().BeTrue();
    }
    
    [Fact]
    public void WeaponBonusFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""weapon_bonus"",
                ""selection_type"": ""weapons_with_required_skill"",
                ""name"": {
                    ""compare"": ""is"",
                    ""qualifier"": ""Flail""
                },
                ""level"": {
                    ""compare"": ""at_least"",
                    ""qualifier"": 1
                },
                ""amount"": 1,
                ""per_level"": true
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(WeaponBonusFeature));

        var feature = (WeaponBonusFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.WeaponBonus);
        feature.WeaponSelectionType.Should().Be(WeaponBonusFeature.WeaponSelectionTypeEnum.WithRequiredSkill);
        feature.NameCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        feature.NameCriteria!.Qualifier.Should().Be("Flail");
        feature.RelativeLevelCriteria!.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        feature.RelativeLevelCriteria!.Qualifier.Should().Be(1);
        feature.TagsCriteria.Should().BeNull();
        feature.Amount.Should().Be(1);
        feature.IsScalingWithLevel.Should().BeTrue();
    }
    
    [Fact]
    public void AttributeCostReductionFeature_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""cost_reduction"",
                ""attribute"": ""st"",
                ""percentage"": 40
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(AttributeCostReductionFeature));

        var feature = (AttributeCostReductionFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.AttributeCostReduction);
        feature.AttributeType.Should().Be(Attribute.AttributeType.Strength);
        feature.Amount.Should().Be(40);
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void ContainedWeightReductionFeature_InLb_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""contained_weight_reduction"",
                ""reduction"": ""6 lb""
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(ContainedWeightReductionFeature));

        var feature = (ContainedWeightReductionFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.ContainedWeightReduction);
        feature.Amount.Should().Be(6);
        feature.IsInPercent.Should().BeFalse();
        feature.IsScalingWithLevel.Should().BeFalse();
    }
    
    [Fact]
    public void ContainedWeightReductionFeature_InPercent_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""type"": ""contained_weight_reduction"",
                ""reduction"": ""80%""
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var iFeature = JsonConverterWrapper.Deserialize<IFeature>(json, jsonSettings);
        iFeature.Should().BeAssignableTo(typeof(ContainedWeightReductionFeature));

        var feature = (ContainedWeightReductionFeature)iFeature!;
        feature.FeatureType.Should().Be(IFeature.FeatureTypeEnum.ContainedWeightReduction);
        feature.Amount.Should().Be(80);
        feature.IsInPercent.Should().BeTrue();
        feature.IsScalingWithLevel.Should().BeFalse();
    }
}

