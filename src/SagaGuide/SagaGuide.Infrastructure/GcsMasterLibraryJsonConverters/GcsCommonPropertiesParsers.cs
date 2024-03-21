using System.Text.RegularExpressions;
using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.Common;
using Newtonsoft.Json;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters;

public static class GcsCommonPropertiesParsers
{
    public static Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum ParseDifficultyLevel(string difficulty)
    {
        return difficulty.ToLower() switch
        {
            "e" => Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum.Easy,
            "a" => Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum.Average,
            "h" => Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum.Hard,
            "vh" => Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum.VeryHard,
            "w" => Core.Domain.SkillAggregate.Skill.DifficultyLevelEnum.Wildcard,
            _ => throw new ArgumentOutOfRangeException($"unknown difficulty token in {difficulty}")
        };
    }
    
    public static Attribute.AttributeType? ParseAttributeType(string? type)
    {
        if (type == null)
            return null;
        
        return type switch
        {
            "iq" => Attribute.AttributeType.Intelligence,
            "dx" => Attribute.AttributeType.Dexterity,
            "ht" => Attribute.AttributeType.Health,
            "st" => Attribute.AttributeType.Strength,
            "per" => Attribute.AttributeType.Perception,
            "will" => Attribute.AttributeType.Will,
            "parry" => Attribute.AttributeType.Parry,
            "block" => Attribute.AttributeType.Block, 
            "dodge" => Attribute.AttributeType.Dodge,
            "hearing" => Attribute.AttributeType.Hearing,
            "taste_smell" => Attribute.AttributeType.TasteSmell,
            "vision" => Attribute.AttributeType.Vision,
            "touch" => Attribute.AttributeType.Touch,
            "fright_check" => Attribute.AttributeType.FrightCheck,
            "basic_move" => Attribute.AttributeType.BasicMove,
            "basic_speed" => Attribute.AttributeType.BasicSpeed,
            "sm" => Attribute.AttributeType.SizeModifier,
            "fp" => Attribute.AttributeType.FatiguePoints,
            "hp" => Attribute.AttributeType.HitPoints,
            "qn" => Attribute.AttributeType.Quintessence,
            "qp" => Attribute.AttributeType.QuintessencePoints,
            "tm_threshold" => Attribute.AttributeType.TmThreshold,
            "tm_pt" => Attribute.AttributeType.TmPowerTally,
            "tm_rr" => Attribute.AttributeType.TmRechargeRate,
            "tm_excess" => Attribute.AttributeType.TmExcessPowerTally,
            "tm_calamity_bonus" => Attribute.AttributeType.TmCalamityRollBonus,
            _ => throw new ArgumentOutOfRangeException($"unknown attribute token in {type}")
        };
    }
    
    public static List<BookReference> ParseBookReferences(string? inputJsonString)
    {
        var bookReferences = new List<BookReference>();

        if (inputJsonString == null)
            return bookReferences;
        
        const string bookGroup = "BookGroup";
        const string pageGroup = "PageGroup";
        const string magazineGroup = "MagazineGroup";
        const string regexPattern = $"(?<{bookGroup}>[a-z]+)((?<{magazineGroup}>\\d+):)?(?<{pageGroup}>\\d+)";
        var regex = new Regex(regexPattern);
        var matches = regex.Matches(inputJsonString.ToLower());

        if (matches.Count == 0)
            throw new JsonReaderException($"unknown or empty BookReference token in \"{inputJsonString}\"");
        
        foreach (Match match in matches)
        {
            bookReferences.Add(new BookReference
                {
                    PageNumber = int.Parse(match.Groups[pageGroup].Value),
                    MagazineNumber = match.Groups[magazineGroup].Success ? int.Parse(match.Groups[magazineGroup].Value) : null,
                    SourceBook = match.Groups[bookGroup].Value switch
                    {
                        "b" => BookReference.SourceBookEnum.BasicSet,
                        "ma" => BookReference.SourceBookEnum.MartialArts,
                        "act" => BookReference.SourceBookEnum.Action,
                        "ate" => BookReference.SourceBookEnum.AfterTheEnd,
                        "bs" => BookReference.SourceBookEnum.Banestorm,
                        "bt" => BookReference.SourceBookEnum.BioTech,
                        "dtg" => BookReference.SourceBookEnum.DelversToGrow,
                        "dr" => BookReference.SourceBookEnum.Dragons,
                        "df" => BookReference.SourceBookEnum.DungeonFantasy,
                        "dfa" => BookReference.SourceBookEnum.DungeonFantasyRpg,
                        "f" => BookReference.SourceBookEnum.Fantasy,
                        "ff" => BookReference.SourceBookEnum.FantasyFolk,
                        "a" => BookReference.SourceBookEnum.Aliens,
                        "gul" => BookReference.SourceBookEnum.Gulliver,
                        "gf" => BookReference.SourceBookEnum.GunFu,
                        "ht" => BookReference.SourceBookEnum.HighTech,
                        "hf" => BookReference.SourceBookEnum.HistoricalFolk,
                        "h" => BookReference.SourceBookEnum.Horror,
                        "iw" => BookReference.SourceBookEnum.InfiniteWorlds,
                        "loot" => BookReference.SourceBookEnum.LandsOutOfTime,
                        "lite" => BookReference.SourceBookEnum.Lite,
                        "lt" => BookReference.SourceBookEnum.LowTech,
                        "ltc" => BookReference.SourceBookEnum.LowTechCompanion,
                        "ltia" => BookReference.SourceBookEnum.LowTechInstantArmor,
                        "mpd" => BookReference.SourceBookEnum.MagicDeathSpells,
                        "mps" => BookReference.SourceBookEnum.MagicPlantSpells,
                        "mtlos" => BookReference.SourceBookEnum.MagicTheLeastOfSpells,
                        "m" => BookReference.SourceBookEnum.Magic,
                        "mh" => BookReference.SourceBookEnum.MonsterHunter,
                        "myst" => BookReference.SourceBookEnum.Mysteries,
                        "myth" => BookReference.SourceBookEnum.Myth,
                        "pu" => BookReference.SourceBookEnum.PowerUps,
                        "p" => BookReference.SourceBookEnum.Powers,
                        "psi" => BookReference.SourceBookEnum.Psionics,
                        "py" => BookReference.SourceBookEnum.PyramidMagazine,
                        "rswl" => BookReference.SourceBookEnum.ReignOfSteelWillToLive,
                        "se" =>BookReference.SourceBookEnum.SocialEngineering,
                        "s" => BookReference.SourceBookEnum.Space,
                        "swc" => BookReference.SourceBookEnum.StarWars,
                        "st" => BookReference.SourceBookEnum.SteamPunk,
                        "su" => BookReference.SourceBookEnum.Supers,
                        "ts" => BookReference.SourceBookEnum.TacticalShooting,
                        "tsp" => BookReference.SourceBookEnum.TalesOfSolarPatrol,
                        "tt" => BookReference.SourceBookEnum.TemplateToolkit,
                        "trpm" => BookReference.SourceBookEnum.ThaumatologyRitualPathMagic,
                        "db" => BookReference.SourceBookEnum.TranshumanSpace,
                        "ta" => BookReference.SourceBookEnum.Traveler,
                        "tiw" => BookReference.SourceBookEnum.TravelerInterstellarWars,
                        "gtft" => BookReference.SourceBookEnum.Gtft,
                        "matg" => BookReference.SourceBookEnum.MartialArtsTechnicalGrappling,
                        "bx" => BookReference.SourceBookEnum.BasicSetCampaigns,
                        "dfm" => BookReference.SourceBookEnum.DungeonFantasyRpgMonsters,
                        "mafccs" => BookReference.SourceBookEnum.MartialArtsFairbairnCloseCombatSystems,
                        "pes" => BookReference.SourceBookEnum.PowersEnhancedSenses,
                        "tcep" => BookReference.SourceBookEnum.ThaumatologyChineseElementalPowers,
                        "tms" => BookReference.SourceBookEnum.ThaumatologyMagicalStyles,
                        "tsor" => BookReference.SourceBookEnum.ThaumatologySorcery,
                        "th" => BookReference.SourceBookEnum.ThaumatologyThresholdMagic,
                        "ut" => BookReference.SourceBookEnum.UltraTech,
                        "utwt" => BookReference.SourceBookEnum.UltraTechWeaponTables,
                        "vor" => BookReference.SourceBookEnum.VorkosiganSagaRpg,
                        "dfx" => BookReference.SourceBookEnum.DungeonFantasyRpgExploits,
                        _ => throw new ArgumentOutOfRangeException($"unknown or empty book name token in \"{match.Groups["BookGroup"].Value}\""),
                    }
                }
            );
        }


        return bookReferences;
    }
}