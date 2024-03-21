using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.Features;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;

public class EquipmentModifierConverter : JsonConverter<EquipmentModifier>
{
    public override void WriteJson(JsonWriter writer, EquipmentModifier? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override EquipmentModifier? ReadJson(JsonReader reader, Type objectType, EquipmentModifier? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var modifier = new EquipmentModifier();
        
        modifier.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        modifier.Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        modifier.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>());
        modifier.Notes = jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        modifier.TechLevel = jsonObject.GetValue("tech_level", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        modifier.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        modifier.Weight = jsonObject.GetValue("weight", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        modifier.Features = jsonObject.GetValue("features", StringComparison.OrdinalIgnoreCase)?.ToObject<List<IFeature>>(serializer) ?? new List<IFeature>();
        modifier.Cost = jsonObject.GetValue("cost", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        modifier.Weight = jsonObject.GetValue("weight", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        
        modifier.CostType = DeserializeCostType(jsonObject.GetValue("cost_type", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer));
        modifier.WeightType = DeserializeWeightType(jsonObject.GetValue("weight_type", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer));
        return modifier;
    }
    
    private static EquipmentModifier.CostTypeEnum DeserializeCostType(string? costType)
    {
        return costType switch
        {
            null => EquipmentModifier.CostTypeEnum.OriginalEquipmentModifier,
            "to_original_cost" => EquipmentModifier.CostTypeEnum.OriginalEquipmentModifier,
            "to_base_cost" => EquipmentModifier.CostTypeEnum.BaseEquipmentModifier,
            "to_final_base_cost" => EquipmentModifier.CostTypeEnum.FinalBaseEquipmentModifier,
            "to_final_cost" => EquipmentModifier.CostTypeEnum.FinalEquipmentModifier,
            _ => throw new ArgumentOutOfRangeException($"unknown EquipmentModifier.CostTypeEnum token in {costType}")
        };
    }
   
    private static EquipmentModifier.WeightTypeEnum DeserializeWeightType(string? weightType)
    {
        return weightType switch
        {
            null => EquipmentModifier.WeightTypeEnum.OriginalEquipmentModifier,
            "to_original_weight" => EquipmentModifier.WeightTypeEnum.OriginalEquipmentModifier,
            "to_base_weight" => EquipmentModifier.WeightTypeEnum.BaseEquipmentModifier,
            "to_final_base_weight" => EquipmentModifier.WeightTypeEnum.FinalBaseEquipmentModifier,
            "to_final_weight" => EquipmentModifier.WeightTypeEnum.FinalEquipmentModifier,
            _ => throw new ArgumentOutOfRangeException($"unknown EquipmentModifier.CostTypeEnum token in {weightType}")
        };
    }
}