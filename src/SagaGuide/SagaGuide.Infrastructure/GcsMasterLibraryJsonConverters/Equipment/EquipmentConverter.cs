using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;

public class EquipmentConverter : JsonConverter<Core.Domain.EquipmentAggregate.Equipment>
{
    public override void WriteJson(JsonWriter writer, Core.Domain.EquipmentAggregate.Equipment? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Core.Domain.EquipmentAggregate.Equipment? ReadJson(JsonReader reader, Type objectType, Core.Domain.EquipmentAggregate.Equipment? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var equipment = new Core.Domain.EquipmentAggregate.Equipment();
        
        equipment.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        equipment.Name = jsonObject.GetValue("description", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        equipment.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string>());
        equipment.Notes = jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        equipment.TechLevel = jsonObject.GetValue("tech_level", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        equipment.LegalityClass = jsonObject.GetValue("legality_class", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        equipment.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        equipment.RatedStrength = jsonObject.GetValue("rated_strength", StringComparison.OrdinalIgnoreCase)?.ToObject<int>(serializer);
        equipment.Cost = jsonObject.GetValue("value", StringComparison.OrdinalIgnoreCase)?.ToObject<double>(serializer);
        equipment.Weight = jsonObject.GetValue("weight", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        equipment.MaxUses = jsonObject.GetValue("max_uses", StringComparison.OrdinalIgnoreCase)?.ToObject<int>(serializer);
        equipment.Prerequisites = jsonObject.GetValue("prereqs", StringComparison.OrdinalIgnoreCase)?.ToObject<PrerequisiteGroup>(serializer);
        equipment.Features = jsonObject.GetValue("features", StringComparison.OrdinalIgnoreCase)?.ToObject<List<IFeature>>(serializer) ?? new List<IFeature>();
        equipment.Attacks = jsonObject.GetValue("weapons", StringComparison.OrdinalIgnoreCase)?.ToObject<List<Attack>>(serializer) ?? new List<Attack>();
        equipment.Modifiers = jsonObject.GetValue("modifiers", StringComparison.OrdinalIgnoreCase)?.ToObject<List<EquipmentModifier>>(serializer) ?? new List<EquipmentModifier>();
        equipment.IgnoreWeightForSkills = jsonObject.GetValue("ignore_weight_for_skills", StringComparison.OrdinalIgnoreCase)?.ToObject<bool>(serializer) ?? false;
        
        return equipment;
    }
}

// type EquipmentEditData struct {
// VTTNotes               string               `json:"vtt_notes,omitempty"` - not widely used in GSC

// Quantity               fxp.Int              `json:"quantity,omitempty"` - part of character equipment
// Uses                   int                  `json:"uses,omitempty"`  - part of character equipment
// Equipped               bool                 `json:"equipped,omitempty"` - part of character equipment
// }