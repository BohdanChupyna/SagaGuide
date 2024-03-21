using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Skill;

namespace SagaGuide.Api.Queries.Skill;

public class GetSkillsQuery : IRequest<List<SkillViewModel>>
{
    public IEnumerable<Guid> SkillIds { get; set; }

    public GetSkillsQuery(IEnumerable<Guid> skillIds)
    {
        SkillIds = skillIds;
    }

}

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, List<SkillViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetSkillsQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<List<SkillViewModel>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.SkillAggregate.Skill> skills;
        if (!request.SkillIds.Any())
        {
            skills = await _context.GetSkills().OrderBy( s => s.Name).ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            skills = await _context.GetSkills().Where(x => request.SkillIds.Contains(x.Id)).OrderBy( s => s.Name).ToListAsync(cancellationToken);
        }
        
        return skills.Adapt<List<SkillViewModel>>();
    }
}