using SagaGuide.Core.Domain.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Feature;

public class FeatureConverter : Newtonsoft.Json.JsonConverter
{

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        // Load the JSON object from the reader
        var jsonObject = JObject.Load(reader);

        
        var type = jsonObject.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer);
        FeatureBase feature = type switch
        {
            "attribute_bonus"  => FeatureConverterHelper.DeserializeAttributeBonusFeature(jsonObject, serializer),
            "conditional_modifier"  => FeatureConverterHelper.DeserializeConditionalModifierBonusFeature(jsonObject, serializer),
            "dr_bonus" => FeatureConverterHelper.DeserializeDamageReductionBonusFeature(jsonObject, serializer),
            "reaction_bonus" => FeatureConverterHelper.DeserializeReactionBonusFeature(jsonObject, serializer),
            "skill_bonus" => FeatureConverterHelper.DeserializeSkillBonusFeature(jsonObject, serializer),
            "skill_point_bonus" => FeatureConverterHelper.DeserializeSkillPointBonusFeature(jsonObject, serializer),
            "spell_bonus" => FeatureConverterHelper.DeserializeSpellBonusFeature(jsonObject, serializer),
            "spell_point_bonus" => FeatureConverterHelper.DeserializeSpellPointBonusFeature(jsonObject, serializer),
            "weapon_bonus" => FeatureConverterHelper.DeserializeWeaponBonusFeature(jsonObject, serializer, WeaponBonusFeature.WeaponBonusTypeEnum.Damage),
            "weapon_dr_divisor_bonus" => FeatureConverterHelper.DeserializeWeaponBonusFeature(jsonObject, serializer, WeaponBonusFeature.WeaponBonusTypeEnum.DamageReductionDivisor),
            "cost_reduction" => FeatureConverterHelper.DeserializeAttributeCostReductionFeature(jsonObject, serializer),
            "contained_weight_reduction" => FeatureConverterHelper.DeserializeContainedWeightReductionFeature(jsonObject, serializer),
            _ => throw new ArgumentOutOfRangeException($"unknown or empty prerequisite type token in \"{type}\"")
        };

        if (feature.FeatureType != IFeature.FeatureTypeEnum.ContainedWeightReduction && feature.FeatureType != IFeature.FeatureTypeEnum.AttributeCostReduction)
        {
            feature.Amount = jsonObject.GetValue("amount", StringComparison.OrdinalIgnoreCase)!.ToObject<int>(serializer);
        }
        
        feature.IsScalingWithLevel = jsonObject.GetValue("per_level", StringComparison.OrdinalIgnoreCase)?.ToObject<bool?>(serializer) ?? false;
        
        return feature;
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(IFeature).IsAssignableFrom(objectType);
    }
}