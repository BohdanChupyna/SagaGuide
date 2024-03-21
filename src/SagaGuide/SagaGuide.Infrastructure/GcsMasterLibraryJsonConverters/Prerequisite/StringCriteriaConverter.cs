using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;

public class StringCriteriaConverter : JsonConverter<StringCriteria>
{
    public override void WriteJson(JsonWriter writer, StringCriteria? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override StringCriteria? ReadJson(JsonReader reader, Type objectType, StringCriteria? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        return new StringCriteria
        {
            Qualifier = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!,
            Comparison = ParseComparisonType( jsonObject.GetValue("compare", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!)
        };
    }

    private static StringCriteria.ComparisonType ParseComparisonType(string comparison) =>
        comparison switch
        {
            "" => StringCriteria.ComparisonType.Any,
            "is" => StringCriteria.ComparisonType.Is,
            "is_not" => StringCriteria.ComparisonType.IsNot,
            "contains" => StringCriteria.ComparisonType.Contains,
            "does_not_contain" => StringCriteria.ComparisonType.DoesNotContain,
            "starts_with" => StringCriteria.ComparisonType.StartsWith,
            "does_not_start_with" => StringCriteria.ComparisonType.DoesNotStartWith,
            "ends_with" => StringCriteria.ComparisonType.EndsWith,
            "does_not_end_with" => StringCriteria.ComparisonType.DoesNotEndWith,
            _ => throw new ArgumentOutOfRangeException($"unknown or empty comparison type token in \"{comparison}\""),
        };
}