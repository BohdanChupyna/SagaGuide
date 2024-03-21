using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Trait;

namespace SagaGuide.Api.Queries.Trait;

public class GetTraitQuery: IRequest<TraitViewModel>
{
    public Guid Id { get; set; }

    public GetTraitQuery(Guid id)
    {
        Id = id;
    }
}

public class GetTraitQueryHandler : IRequestHandler<GetTraitQuery, TraitViewModel>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetTraitQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<TraitViewModel> Handle(GetTraitQuery request, CancellationToken cancellationToken)
    {
        var feature = await _context.GetTraits()
            .SingleAsync(x => request.Id.Equals(x.Id), cancellationToken: cancellationToken);
        return feature.Adapt<TraitViewModel>();
    }
}