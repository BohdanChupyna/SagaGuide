using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using Microsoft.Extensions.Logging;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;
using SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

namespace SagaGuide.Infrastructure.EntityFramework.GcsMasterLibrarySeeder;

public class GcsMasterLibrarySeeder
{
    private readonly GurpsDbContext _dbContext;
    private readonly ILogger<GcsMasterLibrarySeeder> _logger;

    public GcsMasterLibrarySeeder(GurpsDbContext dbContext, ILogger<GcsMasterLibrarySeeder> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task Seed(string gcsMasterLibraryPath)
    {
        var filePaths = GetFilePaths(gcsMasterLibraryPath);
        await SeedSkillsAndTechniquesAsync(filePaths);
        await SeedTraitsAsync(filePaths);
        await SeedEquipmentAsync(filePaths);
    }
    
    private async Task SeedSkillsAndTechniquesAsync(IEnumerable<string> filePaths)
    {
        _logger.LogInformation("Starting to seed Gcs skills");

        if (_dbContext.Set<Skill>().Any() || _dbContext.Set<Technique>().Any())
        {
            _logger.LogInformation("DB already contains skills/techniques, stop seeding Gcs skills/techniques");
            return;
        }

        var skillPaths = GetFilePathsForExtension(filePaths, GcsMasterLibraryFileExtensions.SkillsAndTechniques);
        var skills = new List<Skill>();
        var techniques = new List<Technique>();
        
        foreach (var filePath in skillPaths)
        {
            SkillFileReader.Read(filePath, out var skillsRes, out var techniqueRes);
            skills.AddRange(skillsRes);
            techniques.AddRange(techniqueRes);
        }
        _logger.LogInformation($"Found {skills.Count} Gcs skills");
        _logger.LogInformation($"Found {techniques.Count} Gcs techniques");
        
        var uniqueSkills = new SkillEqualityCompare().UnifyDateTimeAndDistinct(skills).ToList();
        var uniqueTechniques = new TechniqueEqualityCompare().UnifyDateTimeAndDistinct(techniques).ToList();
        
        _logger.LogInformation($"Going to add {uniqueSkills.Count} unique Gcs skills");
        _logger.LogInformation($"Going to add {uniqueTechniques.Count} unique Gcs techniques");
        
        _dbContext.AddRange(uniqueSkills);
        _dbContext.AddRange(techniques);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
        _logger.LogInformation("Finished to seed Gcs skills and techniques");
    }
    
    private async Task SeedTraitsAsync(IEnumerable<string> filePaths)
    {
        _logger.LogInformation("Starting to seed Gcs traits");

        if (_dbContext.Set<Trait>().Any() )
        {
            _logger.LogInformation("DB already contains traits, stop seeding Gcs traits");
            return;
        }

        var traitsPaths = GetFilePathsForExtension(filePaths, GcsMasterLibraryFileExtensions.Traits);
        var traits = new List<Trait>();

        foreach (var filePath in traitsPaths)
        {
            TraitFileReader.Read(filePath, out var skillsRes);
            traits.AddRange(skillsRes);
        }
        _logger.LogInformation($"Found {traits.Count} Gcs traits");

        var uniqueTraits = new TraitEqualityCompare().UnifyDateTimeAndDistinct(traits).ToList();

        _logger.LogInformation($"Going to add {uniqueTraits.Count} unique Gcs traits");

        _dbContext.AddRange(uniqueTraits);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
        _logger.LogInformation("Finished to seed Gcs traits");
    }
    
    private async Task SeedEquipmentAsync(IEnumerable<string> filePaths)
    {
        _logger.LogInformation("Starting to seed Gcs equipments");

        if (_dbContext.Set<Equipment>().Any() )
        {
            _logger.LogInformation("DB already contains equipments, stop seeding Gcs equipments");
            return;
        }

        var equipmentPaths = GetFilePathsForExtension(filePaths, GcsMasterLibraryFileExtensions.Equipment);
        EquipmentFileReader.RemoveUnsupportedEquipmentFiles(ref equipmentPaths);
        var equipments = new List<Equipment>();

        foreach (var filePath in equipmentPaths)
        {
            equipments.AddRange(EquipmentFileReader.Read(filePath));
        }
        _logger.LogInformation($"Found {equipments.Count} Gcs equipments");

        var uniqueEquipments = new EquipmentEqualityCompare().UnifyDateTimeAndDistinct(equipments).ToList();

        _logger.LogInformation($"Going to add {uniqueEquipments.Count} unique Gcs traits");
        
        _dbContext.AddRange(uniqueEquipments);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
        _logger.LogInformation("Finished to seed Gcs equipments");
    }
    
    private static List<string> GetFilePathsForExtension(IEnumerable<string> filePaths, string targetExtension)
    {
        return filePaths.Where(path => path.EndsWith(targetExtension)).ToList();
        
    }
    
    private static List<string> GetFilePaths(string folderPath)
    {
        var filePaths = new List<string>();
        
        foreach (var filePath in Directory.GetFiles(folderPath))
        {
            filePaths.Add(filePath);
        }
        
        foreach (var subdirectoryPath in Directory.GetDirectories(folderPath))
        {
            filePaths.AddRange(GetFilePaths(subdirectoryPath));
        }
        
        return filePaths;
    }
}