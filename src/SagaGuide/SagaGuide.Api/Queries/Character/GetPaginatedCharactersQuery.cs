using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Character;

namespace SagaGuide.Api.Queries.Character;

public class GetPaginatedCharactersQuery : GetPaginatedBaseQuery<CharacterViewModel>
{
}

public class GetPaginatedCharactersQueryHandler : IRequestHandler<GetPaginatedCharactersQuery, PaginatedItemsViewModel<CharacterViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetPaginatedCharactersQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public Task<PaginatedItemsViewModel<CharacterViewModel>> Handle(GetPaginatedCharactersQuery request, CancellationToken cancellationToken)
    {
        var result = new PaginatedItemsViewModel<CharacterViewModel>(
            request.PageIndex,
            request.PageSize,
            _context.GetCharacters().LongCount(),
            _context.GetCharacters()
                .OrderBy( s => s.Name)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ToArray()
                .Adapt<IEnumerable<CharacterViewModel>>());
        return Task.FromResult(result);
    }
}