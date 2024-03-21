using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Character;

namespace SagaGuide.Api.Queries.Character;

public class GetCharactersInfoQuery : IRequest<IEnumerable<CharacterInfoViewModel>>
{
    public IEnumerable<Guid> Ids { get; set; }

    public GetCharactersInfoQuery(IEnumerable<Guid> ids)
    {
        Ids = ids;
    }

}

public class GetCharactersInfoQueryHandler : IRequestHandler<GetCharactersInfoQuery, IEnumerable<CharacterInfoViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetCharactersInfoQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CharacterInfoViewModel>> Handle(GetCharactersInfoQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.CharacterAggregate.Character> characters;
        if (!request.Ids.Any())
        {
            characters = await _context.GetCharactersInfo().ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            characters = await _context.GetCharactersInfo().Where(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }
        
        return characters.Adapt<IEnumerable<CharacterInfoViewModel>>();
    }
}