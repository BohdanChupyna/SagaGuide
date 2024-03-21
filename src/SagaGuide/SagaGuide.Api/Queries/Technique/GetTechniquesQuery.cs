using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Technique;

namespace SagaGuide.Api.Queries.Technique;

public class GetTechniquesQuery : IRequest<IEnumerable<TechniqueViewModel>>
{
    public IEnumerable<Guid> TechniquesIds { get; set; }

    public GetTechniquesQuery(IEnumerable<Guid> techniquesIds)
    {
        TechniquesIds = techniquesIds;
    }

}

public class GetTechniquesQueryHandler : IRequestHandler<GetTechniquesQuery, IEnumerable<TechniqueViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetTechniquesQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TechniqueViewModel>> Handle(GetTechniquesQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.TechniqueAggregate.Technique> techniques;
        if (!request.TechniquesIds.Any())
        {
            techniques = await _context.GetTechniques().OrderBy( s => s.Name).ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            techniques = await _context.GetTechniques().Where(x => request.TechniquesIds.Contains(x.Id)).OrderBy( s => s.Name).ToListAsync(cancellationToken);
        }
        
        return techniques.Adapt<IEnumerable<TechniqueViewModel>>();
    }
}