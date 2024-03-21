using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Skill;

namespace SagaGuide.Api.Queries.Skill;

public class GetSkillQuery : IRequest<SkillViewModel>
{
    public Guid Id { get; set; }

    public GetSkillQuery(Guid id)
    {
        Id = id;
    }

}

public class GetSkillQueryHandler : IRequestHandler<GetSkillQuery, SkillViewModel>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetSkillQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<SkillViewModel> Handle(GetSkillQuery request, CancellationToken cancellationToken)
    {
        var skill = await _context.GetSkills()
            .SingleAsync(x => request.Id.Equals(x.Id), cancellationToken: cancellationToken);
        return skill.Adapt<SkillViewModel>();
    }
}