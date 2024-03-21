using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;

public class SkillConverter : JsonConverter<Core.Domain.SkillAggregate.Skill>
{
    public override void WriteJson(JsonWriter writer, Core.Domain.SkillAggregate.Skill? value,
        JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Core.Domain.SkillAggregate.Skill ReadJson(JsonReader reader, Type objectType,
        Core.Domain.SkillAggregate.Skill? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var difficulty = jsonObject.GetValue("difficulty")!.ToString();
        ParseDifficulty(difficulty, out var difficultyLevel, out var attributeType);
        
        var skill = new Core.Domain.SkillAggregate.Skill();
        skill.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        skill.Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        skill.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        skill.Specialization = jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>();
        skill.TechLevel = jsonObject.GetValue("tech_level", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer);
        skill.PointsCost = jsonObject.GetValue("points", StringComparison.OrdinalIgnoreCase)?.ToObject<int>(serializer) ?? 0;
        skill.EncumbrancePenaltyMultiplier = jsonObject.GetValue("points", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer);
        skill.Defaults = jsonObject.GetValue("defaults", StringComparison.OrdinalIgnoreCase)?.ToObject<List<SkillDefault>>(serializer) ?? new List<SkillDefault>();
        skill.AttributeType = attributeType;
        skill.DifficultyLevel = difficultyLevel;
        skill.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>());
        skill.Prerequisites = jsonObject.GetValue("prereqs", StringComparison.OrdinalIgnoreCase)?.ToObject<PrerequisiteGroup?>(serializer);
        skill.LocalNotes = jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>();
        
        return skill;
    }

    private static void ParseDifficulty(string inputDifficulty,
        out Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum difficultyLevel,
        out Attribute.AttributeType attributeType)
    {
        var split = inputDifficulty.ToLower().Split("/");
        var attribute = split[0];
        var difficulty = split[1];

        attributeType = GcsCommonPropertiesParsers.ParseAttributeType(attribute)!.Value;
        difficultyLevel = GcsCommonPropertiesParsers.ParseDifficultyLevel(difficulty);
    }
}