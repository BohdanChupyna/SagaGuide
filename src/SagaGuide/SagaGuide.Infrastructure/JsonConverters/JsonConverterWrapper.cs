using Newtonsoft.Json;

namespace SagaGuide.Infrastructure.JsonConverters;

public static class JsonConverterWrapper
{
    public static TValue? Deserialize<TValue>(string json, JsonSettingsWrapper? settings = null)
    {
        var jsonSettings = settings ?? JsonSettingsWrapper.Create();
        return NewtonsoftDeserialize<TValue>(json, jsonSettings.SerializerSettings);
    }

    public static string Serialize<TValue>(TValue value, JsonSettingsWrapper? settings = null)
    {
        var jsonSettings = settings ?? JsonSettingsWrapper.Create();
        return NewtonsoftSerialize(value, jsonSettings.SerializerSettings);
    }

    private static TValue? NewtonsoftDeserialize<TValue>(string json, JsonSerializerSettings settings) =>
        JsonConvert.DeserializeObject<TValue>(json, settings);

    private static string NewtonsoftSerialize<TValue>(TValue value, JsonSerializerSettings settings) =>
        JsonConvert.SerializeObject(value, settings);
}