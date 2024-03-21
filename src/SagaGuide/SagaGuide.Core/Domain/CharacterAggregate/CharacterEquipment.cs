using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.EquipmentAggregate;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterEquipment : GuidEntity
{
    public Equipment Equipment { get; set; } = null!;
    public List<EquipmentModifier> SelectedModifiers = new ();

    public double Quantity { get; set; } = 1;
    public bool IsEquipped { get; set; }
    public int? LeftUses { get; set; }
}