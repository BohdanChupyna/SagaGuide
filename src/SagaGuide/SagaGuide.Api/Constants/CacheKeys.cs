namespace SagaGuide.Api.Constants;

public static class CacheKeys
{
    public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(365);
    public static readonly TimeSpan AbsoluteExpiration = TimeSpan.FromDays(365);
    
    public const string GetAllEquipments = "getAllEquipments";
    public const string GetAllSkills = "getAllSkills";
    public const string GetAllTechniques = "getAllTechniques";
    public const string GetAllTraits = "getAllTraits";
    
}