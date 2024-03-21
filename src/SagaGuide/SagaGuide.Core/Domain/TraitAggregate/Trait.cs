using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;

namespace SagaGuide.Core.Domain.TraitAggregate;

public class Trait : AuditableEntity
{
    public enum SelfControlRollAdjustmentEnum
    {
        None,
        ActionPenalty,
        ReactionPenalty,
        FrightCheckPenalty,
        FrightCheckBonus,
        MinorCostOfLivingIncrease,
        MajorCostOfLivingIncrease,
    }
    
    public enum TagsEnum
    {
        Advantage,
        Disadvantage,
        Mental,
        Physical,
        Social,
        Exotic,
        Supernatural,
    }
    
    public string Name { get; set; } = null!;
    public string? LocalNotes { get; set; }
  
    public List<string> Tags { get; set; } = new();
    public PrerequisiteGroup? Prerequisites { get; set; }
    public List<BookReference> BookReferences { get; set; } = new();
    
    public int PointsCostPerLevel { get; set; }
    public int BasePointsCost { get; set; }
    
    public bool CanLevel { get; set; }
    public bool RoundCostDown { get; set; }

    public List<IFeature> Features { get; set; } = new ();
    public List<TraitModifier> Modifiers { get; set; } = new ();
    public List<TraitModifierGroup> ModifierGroups { get; set; } = new ();

   public int SelfControlRoll { get; set; }
   public SelfControlRollAdjustmentEnum SelfControlRollAdjustment { get; set; }


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
  
}