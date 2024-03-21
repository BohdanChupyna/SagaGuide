using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.EnvironmentAbstractions;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Core.Domain.IRepository;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Infrastructure.EntityFramework.Configuration;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.EntityFramework;

public class GurpsDbContext : DbContext, IUnitOfWork, IReadOnlyGurpsDbContext
{
    private readonly IMediator _mediator;

    public GurpsDbContext(DbContextOptions options, IMediator mediator, IUserAccessor userAccessor) : base(options)
    {
        _mediator = mediator;
        UserAccessor = userAccessor;

        Transaction = new GurpsDbTransaction(this);
    }
    
    public IUserAccessor UserAccessor { get; }

    public GurpsDbTransaction Transaction { get; }

    public Guid? GetCurrentTransactionId() => Transaction.GetCurrentTransaction()?.TransactionId;

    public DbSet<Attribute> BasicAttributes { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<Technique> Techniques { get; set; } = null!;
    public DbSet<Trait> Traits { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    
    public DbSet<Equipment> Equipments { get; set; } = null!;
    
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        DoAuditableWork();

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<string>();
        
        AddListTypeJsonConversion<string>(configurationBuilder);
        
        AddTypeJsonConversion<BookReference>(configurationBuilder);
        AddTypeJsonConversion<IFeature>(configurationBuilder);
        AddTypeJsonConversion<IPrerequisite>(configurationBuilder);
        AddTypeJsonConversion<TraitModifierGroup>(configurationBuilder);
        AddTypeJsonConversion<TraitModifier>(configurationBuilder);
        AddTypeJsonConversion<SkillDefault>(configurationBuilder);
        AddTypeJsonConversion<PrerequisiteGroup>(configurationBuilder);
        AddTypeJsonConversion<SkillPrerequisite>(configurationBuilder);
        AddTypeJsonConversion<AttributePrerequisite>(configurationBuilder);
        AddTypeJsonConversion<ContainedWeightPrerequisite>(configurationBuilder);
        AddTypeJsonConversion<ContainedQuantityPrerequisite>(configurationBuilder);
        AddTypeJsonConversion<TraitPrerequisite>(configurationBuilder);
        AddTypeJsonConversion<IntegerCriteria>(configurationBuilder);
        AddTypeJsonConversion<StringCriteria>(configurationBuilder);
        AddTypeJsonConversion<DoubleCriteria>(configurationBuilder);
        
        AddTypeJsonConversion<Damage>(configurationBuilder);
        AddTypeJsonConversion<Attack>(configurationBuilder);
        AddTypeJsonConversion<MeleeAttack>(configurationBuilder);
        AddTypeJsonConversion<RangedAttack>(configurationBuilder);
        
        AddTypeJsonConversion<CharacterAttribute>(configurationBuilder);
        AddTypeJsonConversion<CharacterTraitModifier>(configurationBuilder);
        AddTypeJsonConversion<CharacterTrait>(configurationBuilder);
        AddTypeJsonConversion<CharacterSkill>(configurationBuilder);
        AddTypeJsonConversion<CharacterTechnique>(configurationBuilder);
        AddTypeJsonConversion<CharacterEquipment>(configurationBuilder);
    }

    private void AddTypeJsonConversion<T>(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<T>()
            .HaveConversion<DmsJsonConverter<T>, DmsJsonComparer<T>>();
        
        configurationBuilder
            .Properties<List<T>>()
            .HaveConversion<ListJsonConverter<T>, ListComparer<T>>();
        
        configurationBuilder
            .Properties<T?>()
            .HaveConversion<DmsJsonConverter<T?>, DmsJsonComparer<T?>>();
        
        configurationBuilder
            .Properties<List<T?>>()
            .HaveConversion<ListJsonConverter<T?>, ListComparer<T?>>();
    }

    private void AddListTypeJsonConversion<T>(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<List<T>>()
            .HaveConversion<ListJsonConverter<T>, ListComparer<T>>();
        
        configurationBuilder
            .Properties<List<T?>>()
            .HaveConversion<ListJsonConverter<T?>, ListComparer<T?>>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var configurations = EntityTypeConfigurationsFactory.Create(this);
        foreach (var entityTypeConfiguration in configurations) entityTypeConfiguration.Configure(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        //PostgreSql is case-sensetive by default
        // const string caseSensitiveCollation = "SQL_Latin1_General_CP1_CS_AS";
        // modelBuilder.UseCollation(caseSensitiveCollation);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties();
            var decimalProperties = properties
                .Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?));

            foreach (var property in decimalProperties)
                modelBuilder.Entity(entityType.Name).Property(property.Name).HasColumnType("decimal(18,4)");

            // https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-3.x/breaking-changes#detectchanges-honors-store-generated-key-values
            // important to set Property(e => e.Id).ValueGeneratedNever();  
            // otherwise new items in a list will not be Added but Modified (since our entity constructor sets Guid) and inserts will not be made correctly
            if (properties.Any(x => x.Name == nameof(Entity<Guid>.Id)))
                modelBuilder.Entity(entityType.Name).Property(nameof(Entity<Guid>.Id)).ValueGeneratedNever();
        }
    }

    private void DoAuditableWork()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e is { Entity: IAuditable, State: EntityState.Added or EntityState.Modified });

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((IAuditable)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                // ((IAuditable)entityEntry.Entity).CreatedBy = _userAccessor.GetCurrentUserId();
            }
            else
            {
                Entry((IAuditable)entityEntry.Entity).Property(p => p.CreatedOn).IsModified = false;
                Entry((IAuditable)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
            }

            ((IAuditable)entityEntry.Entity).ModifiedOn = DateTime.UtcNow;
            // ((IAuditable)entityEntry.Entity).ModifiedBy = _userAccessor.GetCurrentUserId();
        }
    }

    IQueryable<Attribute> IReadOnlyGurpsDbContext.GetAttributes() =>
        BasicAttributes.AsNoTrackingWithIdentityResolution();

    IQueryable<Skill> IReadOnlyGurpsDbContext.GetSkills() => Skills.AsNoTrackingWithIdentityResolution();
    IQueryable<Technique> IReadOnlyGurpsDbContext.GetTechniques() => Techniques.AsNoTrackingWithIdentityResolution();
    IQueryable<Trait> IReadOnlyGurpsDbContext.GetTraits() => Traits.AsNoTrackingWithIdentityResolution();
    IQueryable<Equipment> IReadOnlyGurpsDbContext.GetEquipments() => Equipments.AsNoTrackingWithIdentityResolution();
    IQueryable<Character> IReadOnlyGurpsDbContext.GetCharacters() => Characters.AsNoTrackingWithIdentityResolution();
    IQueryable<Character> IReadOnlyGurpsDbContext.GetCharactersInfo() => Characters.AsNoTrackingWithIdentityResolution().IgnoreAutoIncludes();
}