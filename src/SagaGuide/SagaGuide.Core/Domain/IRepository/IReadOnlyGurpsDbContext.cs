using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Core.Domain.IRepository;

public interface IReadOnlyGurpsDbContext
{
    public IQueryable<Attribute> GetAttributes();
    public IQueryable<Skill> GetSkills();
    public IQueryable<Technique> GetTechniques();
    public IQueryable<Trait> GetTraits();
    public IQueryable<Equipment> GetEquipments();
    public IQueryable<Character> GetCharacters();
    public IQueryable<Character> GetCharactersInfo();
}