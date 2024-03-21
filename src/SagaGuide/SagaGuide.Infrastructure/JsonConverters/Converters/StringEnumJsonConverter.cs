using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SagaGuide.Infrastructure.JsonConverters.Converters;

public class StringEnumJsonConverter : StringEnumConverter
{
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        try
        {
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }
        catch (JsonException)
        {
            // Should return any negative value to make our enum checks work
            return -100;
        }
    }
}