namespace SagaGuide.Core.Domain.Common;

public class BookReference
{
    public BookReference()
    {
    }

    public BookReference(int pageNumber)
    {
        PageNumber = pageNumber;
        SourceBook = SourceBookEnum.BasicSet;
    }

    public enum SourceBookEnum
    {
        BasicSet,
        BasicSetCampaigns,
        MartialArts,
        Action,
        AfterTheEnd,
        Banestorm,
        BioTech,
        DelversToGrow,
        Dragons,
        DungeonFantasy,
        DungeonFantasyRpg,
        DungeonFantasyRpgMonsters,
        DungeonFantasyRpgExploits,
        Fantasy,
        FantasyFolk,
        Aliens,
        Gulliver,
        GunFu,
        HighTech,
        HistoricalFolk,
        Horror,
        InfiniteWorlds,
        LandsOutOfTime,
        Lite,
        LowTech,
        LowTechCompanion,
        LowTechInstantArmor,
        MagicDeathSpells,
        MagicPlantSpells,
        MagicTheLeastOfSpells,
        Magic,
        MonsterHunter,
        Mysteries,
        Myth,
        PowerUps,
        Powers,
        Psionics,
        PyramidMagazine,
        ReignOfSteelWillToLive,
        SocialEngineering,
        Space,
        StarWars,
        SteamPunk,
        Supers,
        TacticalShooting,
        TalesOfSolarPatrol,
        TemplateToolkit,
        ThaumatologyRitualPathMagic,
        TranshumanSpace,
        Traveler,
        TravelerInterstellarWars,
        Gtft,
        MartialArtsTechnicalGrappling,
        MartialArtsFairbairnCloseCombatSystems,
        PowersEnhancedSenses,
        ThaumatologyChineseElementalPowers,
        ThaumatologyMagicalStyles,
        ThaumatologySorcery,
        ThaumatologyThresholdMagic,
        UltraTech,
        UltraTechWeaponTables,
        VorkosiganSagaRpg,
    }
    public int PageNumber { get; set; }
    public int? MagazineNumber { get; set; }
    public SourceBookEnum SourceBook { get; set; } = SourceBookEnum.BasicSet;
}