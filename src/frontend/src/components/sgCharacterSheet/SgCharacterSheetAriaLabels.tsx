import {adaptNameForAriaLabel} from "../ariaLabelUtils";
import {ISkill} from "../../domain/interfaces/skill/ISkill";
import {ICharacterEquipment, ICharacterTechnique, ICharacterTrait} from "../../domain/interfaces/character/ICharacter";
import {
    getCharacterTechniqueNameWithOptionalSpeciality,
    getCharacterTraitNameWithOptionalSpeciality
} from "../../domain/interfaces/character/characterDomainLogicHelper";
import {IActiveDefenceData} from "../../domain/interfaces/character/characterActiveDefenceHelper";
import {AttributeTypeEnum} from "../../domain/interfaces/attribute/IAttribute";

export class SgCharacterSheetAriaLabels
{
    // static readonly SkillsTabPanelButton: string = "rulebook-tabpanel-skills";
    static readonly CharacterName: string = "character-name";
    static readonly CharacterTotalPoints: string = "character-total-points";
    static readonly CharacterTotalRemainingPoints: string = "character-total-remaining-points";
    static readonly CharacterTotalAttributesPoints: string = "character-total-attributes-points";
    static readonly CharacterTotalSkillsPoints: string = "character-total-skills-points";
    static readonly CharacterTotalTechniquesPoints: string = "character-total-techniques-points";
    static readonly CharacterTotalAdvantagesPoints: string = "character-total-advantages-points";
    static readonly CharacterTotalDisadvantagesPoints: string = "character-total-disadvantages-points";

    static readonly CharacterSkillsRemoveDialogOkButton: string = "character-skills-remove-dialog-ok-button";
    
    static getSkillRowLabel(skill: ISkill): string
    {
        return `${adaptNameForAriaLabel(skill.name)}-character-skill-row`;
    }

    static getSkillRemoveButtonLabel(skill: ISkill): string
    {
        return `${adaptNameForAriaLabel(skill.name)}-character-skill-remove-button`;
    }
    
    static getTechniqueRowLabel(technique: ICharacterTechnique): string
    {
        return SgCharacterSheetAriaLabels.getTechniqueRowLabelWithProvidedName(getCharacterTechniqueNameWithOptionalSpeciality(technique));
    }
    
    static getTechniqueRowLabelWithProvidedName(name: string): string
    {
        return `${adaptNameForAriaLabel(name)}-character-technique-row`;
    }
    
    static getTraitRowLabel(trait: ICharacterTrait): string
    {
        return SgCharacterSheetAriaLabels.getTraitRowLabelWithProvidedName(getCharacterTraitNameWithOptionalSpeciality(trait));
    }
    
    static getTraitRowLabelWithProvidedName(name: string): string
    {
        return `${adaptNameForAriaLabel(name)}-character-trait-row`;
    }

    static getTraitNameLabel(trait: ICharacterTrait): string
    {
        return SgCharacterSheetAriaLabels.getTraitNameLabelWithProvidedName(getCharacterTraitNameWithOptionalSpeciality(trait));
    }

    static getTraitNameLabelWithProvidedName(name: string): string
    {
        return `${adaptNameForAriaLabel(name)}-character-trait-name`;
    }

    static getTraitCostLabel(trait: ICharacterTrait): string
    {
        return SgCharacterSheetAriaLabels.getTraitCostLabelWithProvidedName(getCharacterTraitNameWithOptionalSpeciality(trait));
    }

    static getTraitCostLabelWithProvidedName(name: string): string
    {
        return `${adaptNameForAriaLabel(name)}-character-trait-cost`;
    }

    static getEquipmentRowLabel(characterEquipment: ICharacterEquipment): string
    {
        return SgCharacterSheetAriaLabels.getEquipmentRowLabelWithProvidedName(characterEquipment.equipment.name);
    }

    static getEquipmentRowLabelWithProvidedName(name: string): string
    {
        return `${adaptNameForAriaLabel(name)}-character-equipment-row`;
    }

    static getEquipmentIsEquippedCheckBoxLabel(characterEquipment: ICharacterEquipment): string
    {
        return `${adaptNameForAriaLabel(characterEquipment.equipment.name)}-character-equipment-is-equipped-check-box`;
    }
    
    static getActiveDefenceDataLabels(defence: IActiveDefenceData): IActiveDefenceDataAriaLabels
    {
        return {
            defenceName: `character-${adaptNameForAriaLabel(defence.defenceName)}-defence`,
            defenceProviderName: `character-${adaptNameForAriaLabel(defence.defenceName)}-defence-provider`,
            value: `character-${adaptNameForAriaLabel(defence.defenceName)}-defence-value`
        };
    }
}

export interface IActiveDefenceDataAriaLabels
{
    defenceName: string,
    defenceProviderName: string,
    value: string,
}
    