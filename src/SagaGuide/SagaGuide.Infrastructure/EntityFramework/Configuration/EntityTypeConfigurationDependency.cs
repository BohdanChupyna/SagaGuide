using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration;

// Inspired by https://github.com/dotnet/efcore/issues/23103#issuecomment-720662870
public abstract class EntityTypeConfigurationDependency
{
    public abstract void Configure(ModelBuilder modelBuilder);
}

public abstract class EntityTypeConfigurationDependency<TEntity>
    : EntityTypeConfigurationDependency, IEntityTypeConfiguration<TEntity>
    where TEntity : class
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    public override void Configure(ModelBuilder modelBuilder)
        => Configure(modelBuilder.Entity<TEntity>());
}