namespace SagaGuide.Core.Domain.EquipmentAggregate;

public class MeleeAttack : Attack
{
    public string? Reach { get; set; }
    public string? Parry { get; set; }
    public string? Block { get; set; }
}
