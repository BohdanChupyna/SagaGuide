using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.CharacterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration;

public class AttributeEntityTypeConfiguration : IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder)
    {
        builder.ToTable("Attribute");
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Type).IsUnique();
        builder.Property(b => b.PointsCostPerLevel).IsRequired();
    }
}