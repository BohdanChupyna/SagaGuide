import {adaptNameForAriaLabel} from "../ariaLabelUtils";
import {ISkill} from "../../domain/interfaces/skill/ISkill";
import {ITechnique} from "../../domain/interfaces/technique/ITechnique";
import ITrait, {ITraitModifier} from "../../domain/interfaces/trait/ITrait";
import {IEquipment} from "../../domain/interfaces/equipment/IEquipment";

export class SgRuleBookTabAriaLabels
{
    static readonly SearchInput: string = "rulebook-tabpanel-search";
    
    static readonly SkillsTabPanelButton: string = "rulebook-tabpanel-skills";
    
    static readonly TechniquesTabPanelButton: string = "rulebook-tabpanel-techniques";
    static readonly TechniqueDialog: string = "optional-technique-specialization-dialog";
    static readonly TechniqueDialogOkButton: string = "optional-technique-specialization-dialog-ok-button";
    static readonly TechniqueDialogCancelButton: string = "optional-technique-specialization-dialog-cancel-button";
    static readonly TechniqueDialogNameInput: string = "technique-specialization-input";
    static readonly TechniqueDialogDefaultNameInput: string = "technique-default-specialization-input";
    
    static readonly TraitsTabPanelButton: string = "rulebook-tabpanel-traits";
    static readonly TraitDialog: string = "optional-technique-specialization-dialog";
    static readonly TraitDialogOkButton: string = "optional-technique-specialization-dialog-ok-button";
    static readonly TraitDialogCancelButton: string = "optional-technique-specialization-dialog-cancel-button";
    static readonly TraitDialogSpecializationInput: string = "technique-specialization-input";
    
    static readonly EquipmentsTabPanelButton: string = "rulebook-tabpanel-equipment";
    
    static getSkillPaperLabel(skill: ISkill): string
    {
        return `${adaptNameForAriaLabel(skill.name)}-skill-section`;
    }

    static getTechniquePaperLabel(technique: ITechnique): string
    {
        return `${adaptNameForAriaLabel(technique.name)}-technique-section`;
    }
    
    static getTraitPaperLabel(trait: ITrait): string
    {
        return `${adaptNameForAriaLabel(trait.name)}-trait-section`;
    }
    
    static getTraitModifierLabel(modifier: ITraitModifier)
    {
        return SgRuleBookTabAriaLabels.getTraitModifierLabelWithProvidedName(modifier.name, modifier.pointsCost);
    }

    static getTraitModifierLabelWithProvidedName(name: string, pointsCost: number)
    {
        return `${adaptNameForAriaLabel(name)}-${pointsCost}-trait-modifier`;
    }

    static getEquipmentPaperLabel(equipment: IEquipment): string
    {
        return `${adaptNameForAriaLabel(equipment.name)}-equipment-section`;
    }
}