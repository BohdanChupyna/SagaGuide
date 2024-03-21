using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.Common;
using System.Reflection;
using SagaGuide.Core.Definitions.CharacterAggregate;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SagaGuide.Infrastructure.EntityFramework.Configuration;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public class EfDataSeeder : IDataSeeder
{
    public static readonly Guid TestCharacterFrodoId = Guid.Parse("4b704106-7bea-467d-aa73-725196018a75");
    public static readonly Guid TestCharacterGorlumId = Guid.Parse("4f39bc98-4797-4e9b-8bf6-f496183ca0d0");
    public static readonly Guid TestCharacterGandalfId = Guid.Parse("7ec0ad09-2c14-44b4-bd8c-b980f1ed6195");
    private readonly GurpsDbContext _dbContext;
    private readonly CharacterFactory _characterFactory;
    private readonly IConfiguration  _configuration;
    private readonly GcsMasterLibrarySeeder.GcsMasterLibrarySeeder _gcsSeeder;
    private readonly ILogger<EfDataSeeder> _logger;
    private readonly IWebHostEnvironment environment;


    public EfDataSeeder(GurpsDbContext dbContext, CharacterFactory characterFactory, IConfiguration  configuration, GcsMasterLibrarySeeder.GcsMasterLibrarySeeder gcsSeeder, ILogger<EfDataSeeder> logger, IWebHostEnvironment environment)
    {
        _dbContext = dbContext;
        _characterFactory = characterFactory;
        _configuration = configuration;
        _gcsSeeder = gcsSeeder;
        _logger = logger;
        this.environment = environment;
    }

    async Task IDataSeeder.SeedAsync()
    {
        var mySqlDbConfig = _configuration.GetSection(MyDbConfig.ConfigSection).Get<MyDbConfig>();
        _logger.LogInformation($"Path to GCS Library: {mySqlDbConfig?.GcsMasterLibraryPath}");
        if (!string.IsNullOrEmpty(mySqlDbConfig?.GcsMasterLibraryPath))
        {
            _logger.LogInformation("Starting to seed from GCS Library");
            await SeedAttributesAsync();
            await _gcsSeeder.Seed(mySqlDbConfig.GcsMasterLibraryPath);
            _logger.LogInformation("Finished to seed from GCS Library");
            return;
        }

        if (!environment.IsProduction())
        {
            _logger.LogInformation("Starting to seed testing library");
            await SeedAttributesAsync();
            await SeedTraits();
            await SeedSkillsAsync_Impl();
            await SeedTechniquesAsync();
            await SeedEquipmentAsync();
            await SeedMeleeWeapons();
            await SeedCharacter();
        }

        if (environment.IsProduction())
        {
            _logger.LogInformation("GCS Library is not found");
        }
    }

    async void IDataSeeder.Seed()
    {
        await ((IDataSeeder)this).SeedAsync();
    }
    
    async Task IDataSeeder.SeedSkillsAsync()
    {
        await SeedTraits();
        await SeedSkillsAsync_Impl();
    }
    
    async void IDataSeeder.SeedSkills()
    {
        await ((IDataSeeder)this).SeedSkillsAsync();
    }
    
    private async Task SeedMeleeWeapons()
    {
    }


    private async Task SeedCharacter()
    {
        var areCharactersAdded = _dbContext.Set<Character>().Any();
        if (areCharactersAdded)
            return;
        
        var character = await _characterFactory.CreateAsync(CancellationToken.None);
        character.UserId = UserIds.UserA;
        character.Id = TestCharacterFrodoId;
        character.Name = "Frodo";
        character.Player = "Mike";
        character.Campaign = "MiddleEarth";
        character.Title = "Sneaky hobbit";
        character.Handedness = "right";
        character.Gender = "make";
        character.Race = "hobbit";
        character.Religion = "none";
        character.Age = 33;
        character.Height = 150;
        character.Weight = 40;
        character.TechLevel = 7;
        character.Size = 2;
        
        character.Skills.AddRange(_dbContext.Set<Skill>().Where(s => s.Id == SkillIds.Acting || s.Id == SkillIds.Judo)
            .Select(s => new CharacterSkill
        {
            Skill = s,
            SpentPoints = 8,
            DefaultedFrom = s.Defaults.FirstOrDefault()
        }));
        
        
        character.Traits.AddRange(_dbContext.Set<Trait>().Take(2).Select(f => new CharacterTrait
        {
            Trait = f,
            Level = 1
        }));
        _dbContext.Add(character);
        
        character = await _characterFactory.CreateAsync(CancellationToken.None);
        character.UserId = UserIds.UserA;
        character.Id = TestCharacterGorlumId;
        character.Name = "Gorlum";
        character.Player = "Pite";
        character.Campaign = "MiddleEarth";
        character.Title = "Fisher";
        character.Handedness = "right";
        character.Gender = "make";
        character.Race = "hobbit";
        character.Religion = "none";
        character.Age = 50;
        character.Height = 140;
        character.Weight = 35;
        character.TechLevel = 7;
        character.Size = 2;
        
        
        character.Skills.AddRange(_dbContext.Set<Skill>().Where(s => s.Id == SkillIds.Swimming || s.Id == SkillIds.Aquabatics)
            .Select(s => new CharacterSkill
            {
                Skill = s,
                SpentPoints = 4,
                DefaultedFrom = s.Defaults.FirstOrDefault()
            }));
        
        
        character.Traits.AddRange(_dbContext.Set<Trait>().Skip(2).Take(3).Select(f => new CharacterTrait
        {
            Trait = f,
            Level = 1
        }));
        _dbContext.Add(character);
        
        var userBCharacter = await _characterFactory.CreateAsync(CancellationToken.None);
        userBCharacter.UserId = UserIds.UserB;
        userBCharacter.Id = TestCharacterGandalfId;
        userBCharacter.Name = "Gandalf";
        userBCharacter.Player = "Saruman";
        userBCharacter.Campaign = "MiddleEarth";
        userBCharacter.Title = "The Wizard";
        userBCharacter.Handedness = "right";
        userBCharacter.Gender = "male";
        userBCharacter.Race = "human";
        userBCharacter.Religion = "none";
        userBCharacter.Age = 211;
        userBCharacter.Height = 180;
        userBCharacter.Weight = 70;
        userBCharacter.TechLevel = 7;
        userBCharacter.Size = 2;
        
        
        userBCharacter.Skills.AddRange(_dbContext.Set<Skill>().Where(s => s.Id == SkillIds.Swimming || s.Id == SkillIds.Aquabatics)
            .Select(s => new CharacterSkill
            {
                Skill = s,
                SpentPoints = 4,
                DefaultedFrom = s.Defaults.FirstOrDefault()
            }));
        
        
        userBCharacter.Traits.AddRange(_dbContext.Set<Trait>().Skip(2).Take(3).Select(f => new CharacterTrait
        {
            Trait = f,
            Level = 1
        }));
        _dbContext.Add(userBCharacter);
        
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
    }

    private async Task SeedAttributesAsync()
    {
        var areBasicAttributesAdded = _dbContext.Set<Attribute>().Any();
        if (areBasicAttributesAdded)
            return;

        var basicAttributes = new List<Attribute>
        {
            new()
            {
                Id = Guid.Parse("053a5747-aad1-402b-9a5b-e8a8b770fa8f"),
                DefaultValue = 10,
                Type = Attribute.AttributeType.Strength,
                PointsCostPerLevel = 10,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 14
                }
            },
            new()
            {
                Id = Guid.Parse("5d38d3a9-e454-41b6-9387-e89ad557ca14"),
                DefaultValue = 10,
                Type = Attribute.AttributeType.Dexterity,
                ValueIncreasePerLevel = 1,
                PointsCostPerLevel = 20,
                BookReference = new BookReference
                {
                    PageNumber = 15
                }
            },
            new()
            {
                Id = Guid.Parse("7e87f715-c559-484d-93da-75ff68b0dbee"),
                DefaultValue = 10,
                Type = Attribute.AttributeType.Intelligence,
                ValueIncreasePerLevel = 1,
                PointsCostPerLevel = 20,
                BookReference = new BookReference
                {
                    PageNumber = 15
                }
            },
            new()
            {
                Id = Guid.Parse("0f6b9935-b6ee-4329-9c65-d5172461fe1f"),
                DefaultValue = 10,
                Type = Attribute.AttributeType.Health,
                ValueIncreasePerLevel = 1,
                PointsCostPerLevel = 10,
                BookReference = new BookReference
                {
                    PageNumber = 15
                }
            },
            new()
            {
                Id = Guid.Parse("2d3328b4-b808-4960-b42e-46c0840b36c6"),
                DependOnAttributeType = Attribute.AttributeType.Strength,
                PointsCostPerLevel = 2,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 16
                },
                Type = Attribute.AttributeType.HitPoints
            },
            new()
            {
                Id = Guid.Parse("f32ee13d-89c2-45cd-9d05-16344d4490fd"),
                DependOnAttributeType = Attribute.AttributeType.Intelligence,
                PointsCostPerLevel = 5,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 16
                },
                Type = Attribute.AttributeType.Will
            },
            new()
            {
                Id = Guid.Parse("2a208147-6ab9-4776-abac-a3f8b11ee724"),
                DependOnAttributeType = Attribute.AttributeType.Intelligence,
                PointsCostPerLevel = 5,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 16
                },
                Type = Attribute.AttributeType.Perception
            },
            new()
            {
                Id = Guid.Parse("7bb8d32c-ccdd-4527-a967-1f90a9e72d5c"),
                DependOnAttributeType = Attribute.AttributeType.Health,
                PointsCostPerLevel = 3,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 16
                },
                Type = Attribute.AttributeType.FatiguePoints
            },
            new()
            {
                Id = Guid.Parse("22dc26b6-690a-4e02-8b7e-7c491bfdd1e3"),
                DependOnAttributeType = Attribute.AttributeType.DexterityAndHealth,
                PointsCostPerLevel = 5,
                ValueIncreasePerLevel = 0.25,
                BookReference = new BookReference
                {
                    PageNumber = 17
                },
                Type = Attribute.AttributeType.BasicSpeed
            },
            new()
            {
                Id = Guid.Parse("04422688-c74c-4e0a-a4d1-be2f27f3c16d"),
                DependOnAttributeType = Attribute.AttributeType.DexterityAndHealth,
                PointsCostPerLevel = 5,
                ValueIncreasePerLevel = 1,
                BookReference = new BookReference
                {
                    PageNumber = 17
                },
                Type = Attribute.AttributeType.BasicMove
            },
        };

        _dbContext.AddRange(basicAttributes);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
    }

    private async Task<List<Trait>> SeedTraits()
    {
        var areCharactersAdded = _dbContext.Set<Trait>().Any();
        if (areCharactersAdded)
            return new List<Trait>();
        
        var allTraits = EfDataSeederTraitHelper.GetAllTraits();
        
        _dbContext.AddRange(allTraits);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
        return allTraits;
    }

    private async Task SeedSkillsAsync_Impl()
    {
        var isAlreadyAdded = _dbContext.Set<Skill>().Any();
        if (isAlreadyAdded)
            return;

        var skills = EfDataSeederSkillHelper.GetSkills();
        
        _dbContext.AddRange(skills);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
    }

    private async Task SeedTechniquesAsync()
    {
        var isAlreadyAdded = _dbContext.Set<Technique>().Any();
        if (isAlreadyAdded)
            return;

        var techniques = EfDataSeederTechniqueHelper.GetTechniques();
        
        _dbContext.AddRange(techniques);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
    }
    
    private async Task SeedEquipmentAsync()
    {
        var isAlreadyAdded = _dbContext.Set<Equipment>().Any();
        if (isAlreadyAdded)
            return;

        var equipments = EfDataSeederEquipmentHelper.GetAllEquipment();
        
        _dbContext.AddRange(equipments);
        var cancellationTokenSource = new CancellationTokenSource();
        await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);
    }
    
    
    private static async Task<string> ReadEmbeddedFile(string resourceName)
    {
        var assembly = Assembly.GetAssembly(typeof(Character));
        var resourcePath = assembly!.GetManifestResourceNames().First(name => name.EndsWith(resourceName));
        await using var stream = assembly.GetManifestResourceStream(resourcePath);
        using var reader = new StreamReader(stream!);
        return await reader.ReadToEndAsync();
    }
}