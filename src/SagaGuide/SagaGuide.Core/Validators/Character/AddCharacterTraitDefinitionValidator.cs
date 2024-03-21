using FluentValidation;
using SagaGuide.Core.Definitions.CharacterAggregate;

namespace SagaGuide.Core.Validators.Character
{
    public class AddCharacterTraitDefinitionValidator : AbstractValidator<AddCharacterFeatureDefinition>
    {
        public AddCharacterTraitDefinitionValidator()
        {
            RuleFor(x => x.CharacterId).NotEmpty();
            RuleFor(x => x.FeatureId).NotEmpty();
            RuleFor(x => x.SpentPoints).GreaterThanOrEqualTo(1);
        }
    }
}
