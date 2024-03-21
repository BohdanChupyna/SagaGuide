using FluentValidation;
using SagaGuide.Core.Domain.CharacterAggregate;

namespace SagaGuide.Core.Validators.Character.CharacterAggregate;

public class CharacterBasicAttributeValidator : AbstractValidator<CharacterAttribute>
{
    public CharacterBasicAttributeValidator()
    {
        RuleFor(a => a).Must(a => a.SpentPoints % a.Attribute.PointsCostPerLevel == 0);
        RuleFor(a => a.Attribute).NotNull();
    }
}