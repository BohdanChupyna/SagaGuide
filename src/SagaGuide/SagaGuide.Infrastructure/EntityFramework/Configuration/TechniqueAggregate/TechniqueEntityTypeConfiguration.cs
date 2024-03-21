using SagaGuide.Core.Domain.TechniqueAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration.TechniqueAggregate;

public class TechniqueEntityTypeConfiguration : IEntityTypeConfiguration<Technique>
{
    public void Configure(EntityTypeBuilder<Technique> builder)
    {
        builder.ToTable("Technique");
        builder.HasKey(b => b.Id);
        builder.HasIndex(s => s.Name);
    }
}