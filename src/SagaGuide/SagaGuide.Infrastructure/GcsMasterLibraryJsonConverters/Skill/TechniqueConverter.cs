using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;

public class TechniqueConverter : JsonConverter<Technique>
{
    public override void WriteJson(JsonWriter writer, Technique? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Technique ReadJson(JsonReader reader, Type objectType, Technique? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        
        var technique = new Technique();
        technique.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        technique.Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        technique.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        technique.PointsCost = jsonObject.GetValue("points", StringComparison.OrdinalIgnoreCase)!.ToObject<int>(serializer);
        technique.TechniqueLimitModifier = jsonObject.GetValue("limit", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer);
        technique.Default = jsonObject.GetValue("default", StringComparison.OrdinalIgnoreCase)!.ToObject<SkillDefault>(serializer)!;
        technique.Prerequisites = jsonObject.GetValue("prereqs", StringComparison.OrdinalIgnoreCase)?.ToObject<PrerequisiteGroup?>(serializer);
        technique.DifficultyLevel = GcsCommonPropertiesParsers.ParseDifficultyLevel(jsonObject.GetValue("difficulty")!.ToObject<string>(serializer)!);
        technique.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>());

        return technique;
    }
}