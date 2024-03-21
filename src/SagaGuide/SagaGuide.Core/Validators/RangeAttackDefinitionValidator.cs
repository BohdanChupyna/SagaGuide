using FluentValidation;
using SagaGuide.Core.Definitions;

namespace SagaGuide.Core.Validators;

public class RangeAttackDefinitionValidator : AbstractValidator<RangeAttackDefinition>
{
    public RangeAttackDefinitionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(512);
    }
}