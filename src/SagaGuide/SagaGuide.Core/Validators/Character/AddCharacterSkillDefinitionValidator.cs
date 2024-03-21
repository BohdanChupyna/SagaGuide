using FluentValidation;
using SagaGuide.Core.Definitions.CharacterAggregate;

namespace SagaGuide.Core.Validators.Character;

public class AddCharacterSkillDefinitionValidator: AbstractValidator<AddCharacterSkillDefinition>
{
    public AddCharacterSkillDefinitionValidator()
    {
        RuleFor(x => x.CharacterId).NotEmpty();
        RuleFor(x => x.SkillId).NotEmpty();
        RuleFor(x => x.SpentPoints).GreaterThanOrEqualTo(1);
    }
}