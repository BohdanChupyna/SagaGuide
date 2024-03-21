using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.TraitAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;

public class TraitModifierGroupConverter : JsonConverter<TraitModifierGroup>
{
    public override void WriteJson(JsonWriter writer, TraitModifierGroup? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override TraitModifierGroup ReadJson(JsonReader reader, Type objectType, TraitModifierGroup? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        return new TraitModifierGroup
        {
            Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!),
            Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!,
            Modifiers = jsonObject.GetValue("children", StringComparison.OrdinalIgnoreCase)?.ToObject<List<TraitModifier>>(serializer) ?? new List<TraitModifier>(),
            BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>()),
        };
    }
}