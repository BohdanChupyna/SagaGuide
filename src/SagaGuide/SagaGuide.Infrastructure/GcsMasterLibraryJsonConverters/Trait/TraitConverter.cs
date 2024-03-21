using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Core.Validators;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;

public class TraitConverter : JsonConverter<Core.Domain.TraitAggregate.Trait>
{
    public override void WriteJson(JsonWriter writer, Core.Domain.TraitAggregate.Trait? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Core.Domain.TraitAggregate.Trait ReadJson(JsonReader reader, Type objectType, Core.Domain.TraitAggregate.Trait? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var trait = new Core.Domain.TraitAggregate.Trait();
        trait.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        trait.Name = jsonObject.GetValue("name", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        trait.LocalNotes = jsonObject.GetValue("notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        trait.Tags = jsonObject.GetValue("tags", StringComparison.OrdinalIgnoreCase)?.ToObject<List<string>>(serializer) ?? new List<string>();
        trait.BookReferences = GcsCommonPropertiesParsers.ParseBookReferences(jsonObject.GetValue("reference", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>());
        trait.Prerequisites = jsonObject.GetValue("prereqs", StringComparison.OrdinalIgnoreCase)?.ToObject<PrerequisiteGroup?>(serializer);
        
        //If CanLevel is null it means false;
        trait.CanLevel = jsonObject.GetValue("can_level", StringComparison.OrdinalIgnoreCase)?.ToObject<bool?>(serializer) ?? false;
        
        trait.PointsCostPerLevel = jsonObject.GetValue("points_per_level", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer) ?? 0;
        trait.BasePointsCost = jsonObject.GetValue("base_points", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer) ?? 0;
        trait.SelfControlRoll = jsonObject.GetValue("cr", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer) ?? 0;
        trait.SelfControlRollAdjustment = DeserializeSelfControlRollAdjustment(jsonObject.GetValue("cr_adj", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer));

        //fix GCS error when trait can't level up and there is PointsCostPerLevel instead of BasePointsCost
        if (trait is { CanLevel: false, PointsCostPerLevel: not 0, BasePointsCost: 0})
        {
            trait.BasePointsCost = trait.PointsCostPerLevel;
            trait.PointsCostPerLevel = 0;
        }
        
        //If RoundCostDown is null it means false;
        trait.RoundCostDown = jsonObject.GetValue("round_down", StringComparison.OrdinalIgnoreCase)?.ToObject<bool?>(serializer) ?? false;
        
        trait.Features = jsonObject.GetValue("features", StringComparison.OrdinalIgnoreCase)?.ToObject<List<IFeature>>(serializer) ?? new List<IFeature>();
        DeserializeModifiers(jsonObject, serializer, out var modifiers, out var modifierGroups);
        trait.Modifiers = modifiers;
        trait.ModifierGroups = modifierGroups;
        //trait.Modifiers = jsonObject.GetValue("modifiers", StringComparison.OrdinalIgnoreCase)?.ToObject<List<TraitModifier>>(serializer) ?? new List<TraitModifier>();
        
        // VTTNotes         string             `json:"vtt_notes,omitempty"` - rarely used, so not supported
        // Ancestry         string             `json:"ancestry,omitempty"` - rarely used, so not supported
        // UserDesc         string             `json:"userdesc,omitempty"` - rarely used, so not supported
        // Disabled         bool               `json:"disabled,omitempty"` - all values for Traits are null, so no sense to parse it.
        // Levels           fxp.Int            `json:"levels,omitempty"`   - CharacterTrait property
        
        // Weapons          []*Weapon          `json:"weapons,omitempty"`          // Non-container only
        // Study            []*Study           `json:"study,omitempty"`            // Non-container only
        // TemplatePicker   *TemplatePicker    `json:"template_picker,omitempty"`  // Container only
        // ContainerType    ContainerType      `json:"container_type,omitempty"`     // Container only
        // StudyHoursNeeded StudyHoursNeeded   `json:"study_hours_needed,omitempty"` // Non-container only
       
        
        //new TraitValidator().ValidateAndThrow(trait);
        return trait;
    }
    
    private static Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum DeserializeSelfControlRollAdjustment(string? adjustment)
    {
        return adjustment switch
        {
            null => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.None,
            "none" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.None,
            "action_penalty" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.ActionPenalty,
            "reaction_penalty" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.ReactionPenalty,
            "fright_check_penalty" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.FrightCheckPenalty,
            "fright_check_bonus" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.FrightCheckBonus,
            "minor_cost_of_living_increase" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.MinorCostOfLivingIncrease,
            "major_cost_of_living_increase" => Core.Domain.TraitAggregate.Trait.SelfControlRollAdjustmentEnum.MajorCostOfLivingIncrease,
            _ => throw new ArgumentOutOfRangeException($"unknown SelfControlRollAdjustmentEnum token in {adjustment}")
        };
    }

    private static void DeserializeModifiers(JObject jsonObject, JsonSerializer serializer, out List<TraitModifier> modifiers, out List<TraitModifierGroup> groups)
    {
        modifiers = new List<TraitModifier>();
        groups = new List<TraitModifierGroup>();
        
        var list = jsonObject.GetValue("modifiers", StringComparison.OrdinalIgnoreCase);
        
        if(list == null)
            return;
        
        foreach (var child in list.Children())
        {
            if (child.Type == JTokenType.Object)
            {
                var obj = (JObject)child;
                var type = obj.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToString();
                switch (type)
                {
                    case "modifier":
                    {
                        modifiers.Add(child.ToObject<TraitModifier>(serializer)!);
                        break;
                    }
                    case "modifier_container":
                    {
                        groups.Add(child.ToObject<TraitModifierGroup>(serializer)!);
                        break;
                    }
                }
            }
        }
    }
}
