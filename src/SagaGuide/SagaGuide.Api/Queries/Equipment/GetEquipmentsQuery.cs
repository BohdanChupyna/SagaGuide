using SagaGuide.Core.Domain.IRepository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SagaGuide.Api.Contract.Equipment;

namespace SagaGuide.Api.Queries.Equipment;

public class GetEquipmentsQuery : IRequest<IEnumerable<EquipmentViewModel>>
{
    public IEnumerable<Guid> EquipmentIds { get; set; }

    public GetEquipmentsQuery(IEnumerable<Guid> equipmentIds)
    {
        EquipmentIds = equipmentIds;
    }

}

public class GetEquipmentsQueryHandler : IRequestHandler<GetEquipmentsQuery, IEnumerable<EquipmentViewModel>>
{
    private readonly IReadOnlyGurpsDbContext _context;

    public GetEquipmentsQueryHandler(IReadOnlyGurpsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipmentViewModel>> Handle(GetEquipmentsQuery request, CancellationToken cancellationToken)
    {
        List<Core.Domain.EquipmentAggregate.Equipment> equipments;
        if (!request.EquipmentIds.Any())
        {
            equipments = await _context.GetEquipments().OrderBy( s => s.Name).ToListAsync(cancellationToken: cancellationToken);
        }
        else
        {
            equipments = await _context.GetEquipments().Where(x => request.EquipmentIds.Contains(x.Id)).OrderBy( s => s.Name).ToListAsync(cancellationToken);
        }
        
        return equipments.Adapt<IEnumerable<EquipmentViewModel>>();
    }
}