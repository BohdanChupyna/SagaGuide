using SagaGuide.Core.Domain.EquipmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration.EquipmentAggregate;

public class EquipmentEntityTypeConfiguration: IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("Equipment");
        builder.HasKey(b => b.Id);
        builder.HasIndex(s => s.Name);
        builder.Property(e => e.Modifiers).HasConversion(new ListJsonConverter<EquipmentModifier>(), new ListComparer<EquipmentModifier>());
    }
}