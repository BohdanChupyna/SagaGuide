using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Core.Domain;

public class Attribute : GuidEntity
{
    public enum AttributeType
    {
        Strength,
        Dexterity,
        Intelligence,
        Health,
        DexterityAndHealth,
        Damage,
        BasicLift,
        HitPoints,
        Will,
        Perception,
        FatiguePoints,
        BasicSpeed,
        BasicMove,
        Parry,
        Block,
        Dodge,
        Hearing,
        TasteSmell,
        Vision,
        Touch,
        FrightCheck,
        SizeModifier,
        Quintessence,
        QuintessencePoints,
        TmThreshold,
        TmPowerTally,
        TmRechargeRate,
        TmExcessPowerTally,
        TmCalamityRollBonus,
    }

    public double? DefaultValue { get; init; }
    public AttributeType Type { get; init; }
    public int PointsCostPerLevel { get; init; }
    public double ValueIncreasePerLevel { get; init; }
    
    public AttributeType? DependOnAttributeType { get; init; }
    public BookReference BookReference { get; init; } = null!;
}