using SagaGuide.Core.Domain.SkillAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;

public class SkillDefaultConverter : JsonConverter<SkillDefault>
{
    public override void WriteJson(JsonWriter writer, SkillDefault? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    
    public override SkillDefault? ReadJson(JsonReader reader, Type objectType, SkillDefault? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        // Load the JSON object from the reader
        var jObject = JObject.Load(reader);
        
        var type = jObject.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToString();
        Core.Domain.Attribute.AttributeType? attributeType = null;
        if (type != "skill")
            attributeType = GcsCommonPropertiesParsers.ParseAttributeType(type)!.Value;

        return new SkillDefault
        {
            AttributeType = attributeType,
            Name = jObject.GetValue("name", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer)!,
            Specialization = jObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer),
            Modifier = jObject.GetValue("modifier", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer) ?? 0,
            Level = jObject.GetValue("level", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer),
            AdjustedLevel = jObject.GetValue("adjusted_level", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer),
            Points = jObject.GetValue("Points", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer)
        };
    }
}
