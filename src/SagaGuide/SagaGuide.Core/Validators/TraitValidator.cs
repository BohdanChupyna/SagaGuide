using FluentValidation;
using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Core.Validators;

public class TraitValidator : AbstractValidator<Trait>
{
    public TraitValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        
        RuleFor(t => t.PointsCostPerLevel).NotEqual(0).When(t => t.CanLevel);
        //RuleFor(t => t.BasePointsCost).NotEqual(0).When(t => !t.CanLevel); or there should be other modifiers
   
    }
}