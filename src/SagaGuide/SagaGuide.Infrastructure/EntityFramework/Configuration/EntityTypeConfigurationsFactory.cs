using SagaGuide.Infrastructure.EntityFramework.Configuration.CharacterAggregate;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration;

public static class EntityTypeConfigurationsFactory
{
    public static IEnumerable<EntityTypeConfigurationDependency> Create(GurpsDbContext dbContext) =>
        new List<EntityTypeConfigurationDependency>
        {
            new CharacterEntityTypeConfiguration(dbContext),
        };
}