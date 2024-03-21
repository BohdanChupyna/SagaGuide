using SagaGuide.Core.Domain.CharacterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaGuide.Infrastructure.EntityFramework.Configuration.CharacterAggregate;

public class CharacterEntityTypeConfiguration : EntityTypeConfigurationDependency<Character>
{
    private readonly GurpsDbContext _dbContext;
    public CharacterEntityTypeConfiguration(GurpsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Character");
        builder.HasKey(character => character.Id);
        builder.Property(character => character.UserId).IsRequired();
        builder.Property(character => character.Name).IsRequired();
        builder.HasQueryFilter(character => character.UserId == _dbContext.UserAccessor.GetCurrentUserId());
    }
}