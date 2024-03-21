using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.JsonConverters.Converters;

public class TrimmingStringJsonConverter : JsonConverter<string>
{
    public override string? ReadJson(JsonReader reader, Type objectType, string? existingValue, bool hasExistingValue,
        JsonSerializer serializer) => reader.Value?.ToString()?.Trim();

    public override void WriteJson(JsonWriter writer, string? value, JsonSerializer serializer) =>
        new JValue(value).WriteTo(writer);
}