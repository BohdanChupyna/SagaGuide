using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract.Equipment;
using SagaGuide.Api.Queries.Equipment;

namespace SagaGuide.Api.Controllers;

[ApiController]
[Route(RouteTemplates.DbApi)]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class EquipmentController : SagaGuideControllerBase
{
    private readonly IMediator _mediator;
    private readonly MemoryCacheProvider _memoryCacheProvider;

    public EquipmentController(IMediator mediator, MemoryCacheProvider memoryCacheProvider)
    {
        _mediator = mediator;
        _memoryCacheProvider = memoryCacheProvider;
    }

    /// <summary>
    ///     This operation fetches a single equipment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "equipments/{id}")]
    [ProducesResponseType(typeof(EquipmentViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEquipment(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEquipmentQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a list of equipments
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet( "equipments")]
    [ProducesResponseType(typeof(IEnumerable<EquipmentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEquipments([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
       
        if (ids.Length == 0 && _memoryCacheProvider.Cache.TryGetValue(CacheKeys.GetAllEquipments, out var equipment))
        {
            return Ok(equipment as List<EquipmentViewModel>);
        }
        
        var query = new GetEquipmentsQuery(ids);
        var result = (await _mediator.Send(query, cancellationToken)).ToList();
        
        if (ids.Length == 0)
        {
           _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllEquipments, result);
        }
        
        return Ok(result);
    }
}