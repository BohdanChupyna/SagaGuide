using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Trait;

namespace SagaGuide.Api.Queries.Trait;

public class GetPaginatedTraitsQuery : GetPaginatedBaseQuery<TraitViewModel>
{
}

public class GetPaginatedFeaturesQueryHandler : IRequestHandler<GetPaginatedTraitsQuery, PaginatedItemsViewModel<TraitViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetPaginatedFeaturesQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public Task<PaginatedItemsViewModel<TraitViewModel>> Handle(GetPaginatedTraitsQuery request, CancellationToken cancellationToken)
    {
        var result = new PaginatedItemsViewModel<TraitViewModel>(
            request.PageIndex,
            request.PageSize,
            _context.GetTraits().LongCount(),
            _context.GetTraits()
                .OrderBy( s => s.Name)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ToArray()
                .Adapt<IEnumerable<TraitViewModel>>());
        return Task.FromResult(result);
    }
}