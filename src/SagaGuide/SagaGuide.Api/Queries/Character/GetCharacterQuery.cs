using SagaGuide.Api.Contract.Skill;
using SagaGuide.Api.Queries.Skill;
using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Character;

namespace SagaGuide.Api.Queries.Character;

public class GetCharacterQuery : IRequest<CharacterViewModel?>
{
    public Guid Id { get; set; }

    public GetCharacterQuery(Guid id)
    {
        Id = id;
    }

}

public class GetSkillQueryHandler : IRequestHandler<GetCharacterQuery, CharacterViewModel?>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetSkillQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<CharacterViewModel?> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
    {
        var character = await _context.GetCharacters()
            .SingleOrDefaultAsync(x => request.Id.Equals(x.Id), cancellationToken: cancellationToken);
        return character?.Adapt<CharacterViewModel>();
    }
}