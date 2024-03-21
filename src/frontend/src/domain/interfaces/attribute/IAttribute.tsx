import IGuid from "../IGuid";
import IBookReference from "../IBookReference";

export enum AttributeTypeEnum
{
    Strength = "Strength",
    Dexterity = "Dexterity",
    Intelligence = "Intelligence",
    Health = "Health",
    DexterityAndHealth = "DexterityAndHealth",
    Damage = "Damage",
    BasicLift = "BasicLift",
    HitPoints = "HitPoints",
    Will = "Will",
    Perception = "Perception",
    FatiguePoints = "FatiguePoints",
    BasicSpeed = "BasicSpeed",
    BasicMove = "BasicMove",
    Parry = "Parry",
    Block = "Block",
    Dodge = "Dodge",
    Hearing = "Hearing",
    TasteSmell = "TasteSmell",
    Vision = "Vision",
    Touch = "Touch",
    FrightCheck = "FrightCheck",
    SizeModifier = "SizeModifier",
    Quintessence = "Quintessence",
    QuintessencePoints = "QuintessencePoints",
    TmThreshold = "TmThreshold",
    TmPowerTally = "TmPowerTally",
    TmRechargeRate = "TmRechargeRate",
    TmExcessPowerTally = "TmExcessPowerTally",
    TmCalamityRollBonus = "TmCalamityRollBonus",
}

export interface IAttribute extends IGuid
{
    defaultValue: number|null,
    type: AttributeTypeEnum,
    pointsCostPerLevel: number,
    valueIncreasePerLevel: number,
    dependOnAttributeType: AttributeTypeEnum|null,
    bookReference: IBookReference,
}

export function getAttributeAbbreviation(attributeType: AttributeTypeEnum): string
{
    switch (attributeType) {
        case AttributeTypeEnum.Strength:
            return "ST";
        case AttributeTypeEnum.Dexterity:
            return "DX";
        case AttributeTypeEnum.Intelligence:
            return "IQ";
        case AttributeTypeEnum.Health:
            return "HT";
        case AttributeTypeEnum.DexterityAndHealth:
            return "DX&HT";
        case AttributeTypeEnum.Perception:
            return "Per";
        case AttributeTypeEnum.Will:
            return "Will";
        case AttributeTypeEnum.BasicSpeed:
            return "BS";
        case AttributeTypeEnum.BasicMove:
            return "BM";
        case AttributeTypeEnum.HitPoints:
            return "HP";
        case AttributeTypeEnum.FatiguePoints:
            return "FP";
        case AttributeTypeEnum.BasicLift:
            return "BL";
        case AttributeTypeEnum.Damage:
            return "Dmg";
    }

    throw new Error(`Wrong AttributeTypeEnum in "${attributeType}"`);
}