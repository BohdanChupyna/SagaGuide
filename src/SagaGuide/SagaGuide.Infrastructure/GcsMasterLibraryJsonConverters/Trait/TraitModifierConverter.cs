using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.TraitAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;

public class TraitModifierConverter : JsonConverter<TraitModifier>
{
    public override void WriteJson(JsonWriter writer, TraitModifier? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override TraitModifier ReadJson(JsonReader reader, Type objectType, TraitModifier? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var modifier = new TraitModifier();
       
        modifier.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        modifier.Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        modifier.LocalNotes = jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        modifier.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        modifier.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>());
        modifier.Features = jsonObject.GetValue("features", StringComparison.OrdinalIgnoreCase)?.ToObject<List<IFeature>?>(serializer) ?? new List<IFeature>();
        
        modifier.PointsCost = jsonObject.GetValue("cost", StringComparison.OrdinalIgnoreCase)?.ToObject<double?>(serializer) ?? 0;
        modifier.CanLevel = jsonObject.GetValue("levels", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer).HasValue ?? false;

        modifier.CostAffectType = DeserializeAffectType(jsonObject.GetValue("affects", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer));
        modifier.CostType = DeserializeCostType(jsonObject.GetValue("cost_type", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer));

        return modifier;
    }

    private static TraitModifier.CostAffectTypeEnum DeserializeAffectType(string? affect)
    {
        return affect switch
        {
            null => TraitModifier.CostAffectTypeEnum.Total,
            "total" => TraitModifier.CostAffectTypeEnum.Total,
            "base_only" => TraitModifier.CostAffectTypeEnum.BaseOnly,
            "levels_only" => TraitModifier.CostAffectTypeEnum.LevelsOnly,
            _ => throw new ArgumentOutOfRangeException($"unknown CostAffectTypeEnum token in {affect}")
        };
    }

    private static TraitModifier.CostTypeEnum DeserializeCostType(string? affect)
    {
        return affect switch
        {
            "percentage" => TraitModifier.CostTypeEnum.Percentage,
            null => TraitModifier.CostTypeEnum.Points,
            "points" => TraitModifier.CostTypeEnum.Points,
            "multiplier" => TraitModifier.CostTypeEnum.Multiplier,
            _ => throw new ArgumentOutOfRangeException($"unknown CostAffectTypeEnum token in {affect}")
        };
    }
}
