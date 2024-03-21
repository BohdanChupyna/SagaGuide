using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Character;

namespace SagaGuide.Api.Queries.Character;

public class GetCharactersQuery : IRequest<IEnumerable<CharacterViewModel>>
{
    public IEnumerable<Guid> SkillIds { get; set; }

    public GetCharactersQuery(IEnumerable<Guid> skillIds)
    {
        SkillIds = skillIds;
    }

}

public class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, IEnumerable<CharacterViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetCharactersQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CharacterViewModel>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.CharacterAggregate.Character> skills;
        if (!request.SkillIds.Any())
        {
            skills = await _context.GetCharacters().ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            skills = await _context.GetCharacters().Where(x => request.SkillIds.Contains(x.Id)).ToListAsync(cancellationToken);
        }
        
        return skills.Adapt<IEnumerable<CharacterViewModel>>();
    }
}