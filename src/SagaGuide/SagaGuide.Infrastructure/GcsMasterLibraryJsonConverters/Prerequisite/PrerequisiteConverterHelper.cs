using SagaGuide.Core.Domain.Prerequisite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;

public static class PrerequisiteConverterHelper
{ 
    public static TraitPrerequisite DeserializeTraitPrerequisite(JObject jsonObject, JsonSerializer serializer)
    { 
        return new TraitPrerequisite
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
            NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<StringCriteria>(serializer)!,
            LevelCriteria =  jsonObject.GetValue("level", StringComparison.OrdinalIgnoreCase)?.ToObject<IntegerCriteria?>(serializer),
            NotesCriteria =  jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
        };
    }
    
    public static SkillPrerequisite DeserializeSkillPrerequisite(JObject jsonObject, JsonSerializer serializer)
    {
        return new SkillPrerequisite
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
            NameCriteria = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<StringCriteria>(serializer)!,
            LevelCriteria =  jsonObject.GetValue("level", StringComparison.OrdinalIgnoreCase)?.ToObject<IntegerCriteria?>(serializer),
            SpecializationCriteria =  jsonObject.GetValue("specialization", StringComparison.OrdinalIgnoreCase)?.ToObject<StringCriteria?>(serializer),
        };
    }
    
    public static AttributePrerequisite DeserializeAttributePrerequisite(JObject jsonObject, JsonSerializer serializer)
    {
        return new AttributePrerequisite
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
            QualifierCriteria =  jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<IntegerCriteria>(serializer)!,
            Required =  GcsCommonPropertiesParsers.ParseAttributeType(jsonObject.GetValue("which", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer))!.Value,
            CombinedWith = GcsCommonPropertiesParsers.ParseAttributeType(jsonObject.GetValue("combined_with", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer)),
        };
    }
    
    public static PrerequisiteGroup DeserializePrerequisiteGroup(JObject jsonObject, JsonSerializer serializer)
    {
        return new PrerequisiteGroup
        {
            WhenTechLevel = jsonObject.GetValue("when_tl", StringComparison.OrdinalIgnoreCase)?.ToObject<IntegerCriteria>(serializer),
            ShouldAllBeSatisfied = jsonObject.GetValue("all", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer),
            Prerequisites = jsonObject.GetValue("prereqs", StringComparison.OrdinalIgnoreCase)!.ToObject<List<IPrerequisite>>(serializer)!,
        };
    }
    
    public static SpellPrerequisite DeserializeSpellPrerequisite(JObject jsonObject, JsonSerializer serializer)
    {
        return new SpellPrerequisite
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
            QualifierCriteria = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<StringCriteria>(serializer)!,
            QuantityCriteria =  jsonObject.GetValue("quantity", StringComparison.OrdinalIgnoreCase)?.ToObject<IntegerCriteria?>(serializer),
            ComparisonType =  ParseSpellPrerequisiteComparisonType(jsonObject.GetValue("sub_type", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!),
        };
    }
    
    public static ContainedWeightPrerequisite DeserializeContainedWeightPrerequisite(JObject jsonObject, JsonSerializer serializer)
    {
        var result = new ContainedWeightPrerequisite()
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
        };
        var weight = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase);
        var converter = new DoubleCriteriaConverter();
        result.QualifierCriteria = converter.ReadWeight(weight.CreateReader(), serializer);
        return result;
    }
    
    public static ContainedQuantityPrerequisite DeserializeContainedQuantityPrerequisite(JObject jsonObject, JsonSerializer serializer)
    {
        return new ContainedQuantityPrerequisite()
        {
            ShouldBe =  jsonObject.GetValue("has", StringComparison.OrdinalIgnoreCase)!.ToObject<bool>(serializer)!,
            QualifierCriteria = jsonObject.GetValue("qualifier", StringComparison.OrdinalIgnoreCase)!.ToObject<IntegerCriteria>(serializer)!,
        };
    }
    
    private static SpellPrerequisite.ComparisonTypeEnum ParseSpellPrerequisiteComparisonType(string comparison) =>
        comparison switch
        {
            "any" => SpellPrerequisite.ComparisonTypeEnum.Any,
            "name" => SpellPrerequisite.ComparisonTypeEnum.Name,
            "tag" => SpellPrerequisite.ComparisonTypeEnum.Tag,
            "college" => SpellPrerequisite.ComparisonTypeEnum.College,
            "college_count" => SpellPrerequisite.ComparisonTypeEnum.CollegeCount,
            _ => throw new ArgumentOutOfRangeException($"unknown or empty comparison type token in \"{comparison}\""),
        };
    
}