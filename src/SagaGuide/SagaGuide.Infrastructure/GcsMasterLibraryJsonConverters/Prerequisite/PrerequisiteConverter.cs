using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;

public class PrerequisiteConverter : Newtonsoft.Json.JsonConverter
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
        return type switch
        {
            "prereq_list" => PrerequisiteConverterHelper.DeserializePrerequisiteGroup(jsonObject, serializer),
            "trait_prereq"  => PrerequisiteConverterHelper.DeserializeTraitPrerequisite(jsonObject, serializer),
            "attribute_prereq"  => PrerequisiteConverterHelper.DeserializeAttributePrerequisite(jsonObject, serializer),
            "skill_prereq"  => PrerequisiteConverterHelper.DeserializeSkillPrerequisite(jsonObject, serializer),
            "spell_prereq"  => PrerequisiteConverterHelper.DeserializeSpellPrerequisite(jsonObject, serializer),
            "contained_weight_prereq" => PrerequisiteConverterHelper.DeserializeContainedWeightPrerequisite(jsonObject, serializer),
            "contained_quantity_prereq" => PrerequisiteConverterHelper.DeserializeContainedQuantityPrerequisite(jsonObject, serializer),
            _ => throw new ArgumentOutOfRangeException($"unknown or empty prerequisite type token in \"{type}\"")
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(IPrerequisite).IsAssignableFrom(objectType);
    }
}