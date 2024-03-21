using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Skill;

namespace SagaGuide.Api.Queries.Skill;

public class GetPaginatedSkillsQuery : GetPaginatedBaseQuery<SkillViewModel>
{
}

public class GetPaginatedSkillsQueryHandler : IRequestHandler<GetPaginatedSkillsQuery, PaginatedItemsViewModel<SkillViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetPaginatedSkillsQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public Task<PaginatedItemsViewModel<SkillViewModel>> Handle(GetPaginatedSkillsQuery request, CancellationToken cancellationToken)
    {
        var result = new PaginatedItemsViewModel<SkillViewModel>(
            request.PageIndex,
            request.PageSize,
            _context.GetSkills().LongCount(),
            _context.GetSkills()
                .OrderBy( s => s.Name)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize)
                .ToArray()
                .Adapt<IEnumerable<SkillViewModel>>());
        return Task.FromResult(result);
    }
}