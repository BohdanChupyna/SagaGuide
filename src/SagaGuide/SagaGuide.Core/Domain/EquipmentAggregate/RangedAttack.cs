namespace SagaGuide.Core.Domain.EquipmentAggregate;

public class RangedAttack : Attack
{
    public string? Accuracy { get; set; }
    public string? Range { get; set; }
    public string? RateOfFire { get; set; }
    public string? Shots { get; set; }
    public string? Bulk { get; set; }
    public string? Recoil { get; set; }
}
