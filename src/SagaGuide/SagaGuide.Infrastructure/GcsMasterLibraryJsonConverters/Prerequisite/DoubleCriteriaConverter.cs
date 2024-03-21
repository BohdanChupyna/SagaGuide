using System.Globalization;
using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;


public class DoubleCriteriaConverter : JsonConverter<DoubleCriteria>
{
    public override void WriteJson(JsonWriter writer, DoubleCriteria? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override DoubleCriteria? ReadJson(JsonReader reader, Type objectType, DoubleCriteria? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        return new DoubleCriteria
        {
            Qualifier = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<double>(serializer),
            Comparison = ParseComparisonType( jsonObject.GetValue("compare", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!)
        };
    }

    public DoubleCriteria? ReadWeight(JsonReader reader, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var result = new DoubleCriteria
        {
            Comparison = ParseComparisonType( jsonObject.GetValue("compare", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!)
        };

        var weight = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!.Replace(" lb", "").Trim();
        result.Qualifier = double.Parse(weight, CultureInfo.InvariantCulture);
        return result;
    }

    private static DoubleCriteria.ComparisonType ParseComparisonType(string comparison) =>
        comparison switch
        {
            "" => DoubleCriteria.ComparisonType.Any,
            "is" => DoubleCriteria.ComparisonType.Equal,
            "is_not" => DoubleCriteria.ComparisonType.NotEqual,
            "at_least" => DoubleCriteria.ComparisonType.AtLeast,
            "at_most" => DoubleCriteria.ComparisonType.AtMost,
            _ => throw new ArgumentOutOfRangeException($"unknown or empty comparison type token in \"{comparison}\""),
        };
}