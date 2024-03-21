using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Equipment;

namespace SagaGuide.Api.Queries.Equipment;

public class GetEquipmentQuery : IRequest<EquipmentViewModel>
{
    public Guid Id { get; set; }

    public GetEquipmentQuery(Guid id)
    {
        Id = id;
    }

}

public class GetEquipmentQueryHandler : IRequestHandler<GetEquipmentQuery, EquipmentViewModel>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetEquipmentQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<EquipmentViewModel> Handle(GetEquipmentQuery request, CancellationToken cancellationToken)
    {
        var equipment = await _context.GetEquipments()
            .SingleAsync(x => request.Id.Equals(x.Id), cancellationToken: cancellationToken);
        return equipment.Adapt<EquipmentViewModel>();
    }
}