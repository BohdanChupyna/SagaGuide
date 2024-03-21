using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;

public class IntegerCriteriaConverter : JsonConverter<IntegerCriteria>
{
    public override void WriteJson(JsonWriter writer, IntegerCriteria? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override IntegerCriteria? ReadJson(JsonReader reader, Type objectType, IntegerCriteria? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var result = new IntegerCriteria
        {
            Comparison = ParseComparisonType( jsonObject.GetValue("compare", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!)
        };
        
        var qualifier = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)?.ToObject<int>(serializer);
        result.Qualifier = qualifier == null && result.Comparison == IntegerCriteria.ComparisonType.AtLeast ? 0 : qualifier!.Value;

        return result;
    }


    private static IntegerCriteria.ComparisonType ParseComparisonType(string comparison) =>
        comparison switch
        {
            "" => IntegerCriteria.ComparisonType.Any,
            "is" => IntegerCriteria.ComparisonType.Equal,
            "is_not" => IntegerCriteria.ComparisonType.NotEqual,
            "at_least" => IntegerCriteria.ComparisonType.AtLeast,
            "at_most" => IntegerCriteria.ComparisonType.AtMost,
            _ => throw new ArgumentOutOfRangeException($"unknown or empty comparison type token in \"{comparison}\""),
        };
}