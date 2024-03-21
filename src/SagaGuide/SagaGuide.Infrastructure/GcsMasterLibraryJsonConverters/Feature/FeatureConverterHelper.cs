using System.Text.RegularExpressions;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Feature;

public static class FeatureConverterHelper
{
    public static AttributeBonusFeature DeserializeAttributeBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        var feature = new AttributeBonusFeature
        {
            AttributeType = GcsCommonPropertiesParsers.ParseAttributeType(jsonObject.GetValue("attribute", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer))!.Value,
        };

        var limitationJson = jsonObject.GetValue("limitation", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);

        feature.BonusLimitation = limitationJson switch
        {
            null => AttributeBonusFeature.BonusLimitationEnum.None,
            "none" => AttributeBonusFeature.BonusLimitationEnum.None,
            "striking_only" => AttributeBonusFeature.BonusLimitationEnum.StrikingOnly,
            "lifting_only" => AttributeBonusFeature.BonusLimitationEnum.LiftingOnly,
            "throwing_only" => AttributeBonusFeature.BonusLimitationEnum.ThrowingOnly,
            _ => throw new ArgumentOutOfRangeException($"unknown limitation token in {limitationJson}")
        };

        return feature;
    }
    
    public static AttributeCostReductionFeature DeserializeAttributeCostReductionFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new AttributeCostReductionFeature
        {
            Amount = jsonObject.GetValue("percentage", StringComparison.OrdinalIgnoreCase)!.ToObject<int>(serializer),
            AttributeType = GcsCommonPropertiesParsers.ParseAttributeType(jsonObject.GetValue("attribute", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer))!.Value,
        };
    }
    
    public static ConditionalModifierBonusFeature DeserializeConditionalModifierBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new ConditionalModifierBonusFeature
        {
            Situation = jsonObject.GetValue("situation", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!,
        };
    }
    
    public static DamageReductionBonusFeature DeserializeDamageReductionBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new DamageReductionBonusFeature
        {
            Location = jsonObject.GetValue("location", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!,
            Specialization = jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer),
        };
    }
    
    public static ReactionBonusFeature DeserializeReactionBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new ReactionBonusFeature
        {
            Situation = jsonObject.GetValue("situation", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!,
        };
    }
    
    public static SkillBonusFeature DeserializeSkillBonusFeature(JObject jsonObject, JsonSerializer serializer)
    {
        var selectionJson = jsonObject.GetValue("selection_type", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer);
        
        var feature = new SkillBonusFeature
        {
            SkillSelectionType = jsonObject.GetValue("selection_type", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer) switch
            {
                "skills_with_name" => SkillBonusFeature.SkillSelectionTypeEnum.SkillsWithName,
                "this_weapon" => SkillBonusFeature.SkillSelectionTypeEnum.ThisWeapon,
                "weapons_with_name" => SkillBonusFeature.SkillSelectionTypeEnum.WeaponsWithName,
                _ => throw new ArgumentOutOfRangeException($"unknown spellMatch token in {selectionJson}")
            },
        };

        if (feature.SkillSelectionType == SkillBonusFeature.SkillSelectionTypeEnum.ThisWeapon)
            return feature;

        feature.NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);
        feature.SpecializationCriteria = jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);
        feature.TagsCriteria = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);

        return feature;
    }
    
    public static SkillPointBonusFeature DeserializeSkillPointBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new SkillPointBonusFeature
        {
            NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<StringCriteria>(serializer)!,
            SpecializationCriteria =  jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
            TagsCriteria =  jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
        };
    }
    
    public static SpellBonusFeature DeserializeSpellBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new SpellBonusFeature
        {
            NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
            TagsCriteria =  jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
            SpellMatchType = DeserializeSpellMatchTypeEnum(jsonObject.GetValue("match", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer)),
        };
    }

    public static SpellPointBonusFeature DeserializeSpellPointBonusFeature(JObject jsonObject, JsonSerializer serializer)
    { 
        return new SpellPointBonusFeature
        {
            NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
            TagsCriteria =  jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
            SpellMatchType = DeserializeSpellMatchTypeEnum(jsonObject.GetValue("match", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer)),
        };
    }
    
    private static SpellMatchTypeEnum DeserializeSpellMatchTypeEnum(string? spellMatchType)
    {
        return spellMatchType switch
        {
            "all_colleges" => SpellMatchTypeEnum.AllColleges,
            "college_name" => SpellMatchTypeEnum.CollegeName,
            "power_source_name" => SpellMatchTypeEnum.PowerSource,
            "spell_name" => SpellMatchTypeEnum.SpellName,
            _ => throw new ArgumentOutOfRangeException($"unknown spellMatch token in {spellMatchType}")
        };
    }
    
    public static WeaponBonusFeature DeserializeWeaponBonusFeature(JObject jsonObject, JsonSerializer serializer, WeaponBonusFeature.WeaponBonusTypeEnum weaponBonusType)
    { 
        var feature = new WeaponBonusFeature
        {
            WeaponSelectionType = DeserializeWeaponSelectionTypeEnum(jsonObject.GetValue("selection_type", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer)),
            WeaponBonusType = weaponBonusType,
        };

        if (feature.WeaponSelectionType == WeaponBonusFeature.WeaponSelectionTypeEnum.ThisWeapon)
            return feature;

        feature.NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);
        feature.SpecializationCriteria = jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);
        feature.TagsCriteria = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer);
        feature.RelativeLevelCriteria = jsonObject.GetValue("level", StringComparison.OrdinalIgnoreCase)?.ToObject<IntegerCriteria?>(serializer);

        return feature;
    }
    
    private static WeaponBonusFeature.WeaponSelectionTypeEnum DeserializeWeaponSelectionTypeEnum(string? weaponSelectionType)
    {
        return weaponSelectionType switch
        {
            "weapons_with_required_skill" => WeaponBonusFeature.WeaponSelectionTypeEnum.WithRequiredSkill,
            "this_weapon" => WeaponBonusFeature.WeaponSelectionTypeEnum.ThisWeapon,
            "weapons_with_name" => WeaponBonusFeature.WeaponSelectionTypeEnum.WithName,
            _ => throw new ArgumentOutOfRangeException($"unknown WeaponSelectionType token in {weaponSelectionType}")
        };
    }
    
    public static ContainedWeightReductionFeature DeserializeContainedWeightReductionFeature(JObject jsonObject, JsonSerializer serializer)
    {
        var reduction = jsonObject.GetValue("reduction", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!.ToLowerInvariant();
        var pattern = @"(?<reductionGroup>\d+)\s*(?<lbGroup>lb)?";
        var match = Regex.Match(reduction, pattern);

        if (!match.Success)
            throw new ArgumentOutOfRangeException($"unknown ContainedWeightReduction token in {reduction}");
        
        return new ContainedWeightReductionFeature
        {
            Amount = int.Parse(match.Groups["reductionGroup"].Value),
            IsInPercent = !match.Groups["lbGroup"].Success,
        };
    }
}