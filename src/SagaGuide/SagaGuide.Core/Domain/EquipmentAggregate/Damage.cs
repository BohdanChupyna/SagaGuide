namespace SagaGuide.Core.Domain.EquipmentAggregate;

public class Damage
{
    public string DamageType { get; set; }  = null!;
    public string? AttackType { get; set; }
    public string? BaseDamage { get; set; }
    public double? ArmorDivisor { get; set; }
    public string? Fragmentation { get; set; }
    public double? FragmentationArmorDivisor { get; set; }
    public string? FragmentationAttackType { get; set; }
    public int? ModifierPerDie { get; set; }
}