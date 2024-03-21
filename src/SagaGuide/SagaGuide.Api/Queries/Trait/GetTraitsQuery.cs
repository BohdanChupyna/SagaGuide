using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Trait;

namespace SagaGuide.Api.Queries.Trait;

public class GetTraitsQuery : IRequest<IEnumerable<TraitViewModel>>
{
    public IEnumerable<Guid> SkillIds { get; set; }

    public GetTraitsQuery(IEnumerable<Guid> skillIds)
    {
        SkillIds = skillIds;
    }

}

public class GetFeaturesQueryHandler : IRequestHandler<GetTraitsQuery, IEnumerable<TraitViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetFeaturesQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TraitViewModel>> Handle(GetTraitsQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.TraitAggregate.Trait> features;
        if (!request.SkillIds.Any())
        {
            features = await _context.GetTraits().OrderBy( x => x.Name).ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            features = await _context.GetTraits().Where(x => request.SkillIds.Contains(x.Id)).OrderBy( x => x.Name).ToListAsync(cancellationToken);
        }
        
        return features.Adapt<IEnumerable<TraitViewModel>>();
    }
}