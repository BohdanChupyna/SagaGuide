using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration.TraitAggregate;

public class TraitEntityTypeConfiguration : IEntityTypeConfiguration<Trait>
{
    public void Configure(EntityTypeBuilder<Trait> builder)
    {
        builder.ToTable("Trait");
        builder.HasKey(b => b.Id);
        builder.HasIndex(s => s.Name);
    }
}