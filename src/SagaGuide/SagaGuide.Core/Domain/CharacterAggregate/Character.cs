using FluentValidation;
using Microsoft.AspNetCore.Http;
using SagaGuide.Core.Definitions.CharacterAggregate;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Core.Validators.Character;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class Character : AuditableEntity
{
    public Guid UserId { get; set; } 
    public string Name { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Handedness { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;
    public double Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int TechLevel { get; set; }
    public double Size { get; set; }
    public int HpLose { get; set; }
    public int FpLose { get; set; }
    public int TotalPoints { get; set; }
    public List<CharacterAttribute> Attributes { get; set; } = new();
    public List<CharacterSkill> Skills { get; set; } = new();
    public List<CharacterTechnique> Techniques { get; set; } = new();
    public List<CharacterTrait> Traits { get; set; } = new();
    public List<CharacterEquipment> Equipments { get; set; } = new();
    
    public CommandResponse<Guid?> AddSkill(Skill skill, AddCharacterSkillDefinition definition)
    {
        new AddCharacterSkillDefinitionValidator().ValidateAndThrow(definition);
        
        var isPrerequisitesSatisfied = new CommandResponse<Guid?>();
        // foreach (var group in skill.Prerequisites)
        // {
        //     // var checkResult = group.Prerequisites.Select(CheckPrerequisite).ToList();
        //     //     
        //     // if (group.GroupType == SkillPrerequisitesGroup.PrerequisitesGroup.And)
        //     // {
        //     //     isPrerequisitesSatisfied.Messages.AddRange(checkResult.Where(message => !message.IsOk));
        //     //     continue;
        //     // }
        //     //
        //     // if (group.GroupType == SkillPrerequisitesGroup.PrerequisitesGroup.Or && !checkResult.Any(message => message.IsOk))
        //     // {
        //     //     isPrerequisitesSatisfied.Messages.AddRange(checkResult);
        //     // }
        // }

   
        if (!isPrerequisitesSatisfied.IsSuccess) 
            return isPrerequisitesSatisfied;
        
        var characterSkill = new CharacterSkill
        {
            Skill = skill,
            SpentPoints = definition.SpentPoints,
            OptionalSpecialty = definition.OptionalSpecialty
        };
        Skills.Add(characterSkill);
        var result = new CommandResponse<Guid?>
        {
            Value = characterSkill.Id
        };
        result.AddInformation(StatusCodes.Status201Created);
        return result;
    }

    public CommandResponse<bool> DeleteSkill(Guid characterSkillId)
    {
        var result = new CommandResponse<bool>();
        var skill = Skills.Find(x => x.Id == characterSkillId);

        if (skill == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.SkillNotFound);
            return result;
        }

        Skills.Remove(skill);
        result.Value = true;
        result.AddInformation(StatusCodes.Status204NoContent);
        return result;
    }

    public CommandResponse<Guid?> AddTrait(Trait trait, AddCharacterFeatureDefinition definition)
    {
        new AddCharacterTraitDefinitionValidator().ValidateAndThrow(definition);

        var characterFeature = new CharacterTrait
        {
            Trait = trait,
            OptionalSpecialty = definition.OptionalSpecialty,
            Level = 1
        };
        Traits.Add(characterFeature);
        var result = new CommandResponse<Guid?>
        {
            Value = characterFeature.Id
        };
        result.AddInformation(StatusCodes.Status201Created);
        return result;
    }

    public CommandResponse<bool> DeleteTrait(Guid characterFeatureId)
    {
        var result = new CommandResponse<bool>();
        var feature = Traits.Find(x => x.Id == characterFeatureId);

        if (feature == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.FeatureNotFound);
            return result;
        }

        Traits.Remove(feature);
        result.Value = true;
        result.AddInformation(StatusCodes.Status204NoContent);
        return result;
    }

    // private CommandResponseMessage CheckPrerequisite(SkillPrerequisite prerequisite)
    // {
    //     if (prerequisite.RequiredSkill != null)
    //     {
    //         var characterSkill = Skills.Find(x => x.SkillId == prerequisite.RequiredSkill.Id);
    //         if (characterSkill == null)
    //         {
    //             return CommandResponseMessage.Error(StatusCodes.Status404NotFound,
    //                 ErrorMessages.GetFromTemplate(ErrorMessages.PrerequisiteSkillNotFound,
    //                     prerequisite.RequiredSkill.Name,
    //                     prerequisite.RequiredSkill.Id));
    //         }
    //
    //         if (characterSkill.SkillLevel < prerequisite.MinimalSkillLevel)
    //         {
    //             return CommandResponseMessage.Error(StatusCodes.Status404NotFound,
    //                 ErrorMessages.GetFromTemplate(ErrorMessages.PrerequisiteSkillLevelIsTooLow,
    //                     prerequisite.RequiredSkill.Name,
    //                     prerequisite.MinimalSkillLevel,
    //                     characterSkill.SkillLevel,
    //                     prerequisite.RequiredSkill.Id));
    //         }
    //     }
    //
    //     if (prerequisite.RequiredFeature == null) 
    //         return CommandResponseMessage.Ok();
    //     
    //     var characterFeature = Traits.Find(x => x.FeatureId == prerequisite.RequiredSkillId);
    //     if (characterFeature == null)
    //     {
    //         return CommandResponseMessage.Error(StatusCodes.Status404NotFound,
    //             ErrorMessages.GetFromTemplate(ErrorMessages.PrerequisiteFeatureNotFound,
    //                 prerequisite.RequiredFeature.Name,
    //                 prerequisite.RequiredFeature.Id));
    //     }
    //
    //     if (prerequisite.RequiredFeatureModifier != null &&
    //         characterFeature.SelectedFeatureModifiers.Find(x => x.Id == prerequisite.RequiredFeatureModifier.Id) ==
    //         null)
    //     {
    //         return CommandResponseMessage.Error(StatusCodes.Status404NotFound,
    //             ErrorMessages.GetFromTemplate(ErrorMessages.PrerequisiteFeatureModifierNotFound,
    //                 prerequisite.RequiredFeatureModifier.Id,
    //                 prerequisite.RequiredFeature.Name,
    //                 prerequisite.RequiredFeature.Id));
    //     }
    //     
    //     return CommandResponseMessage.Ok();
    // }
}