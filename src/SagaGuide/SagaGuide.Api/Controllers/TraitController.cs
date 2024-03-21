using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Trait;
using SagaGuide.Api.Queries.Trait;

namespace SagaGuide.Api.Controllers;

[ApiController]
[Route(RouteTemplates.DbApi)]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TraitController : SagaGuideControllerBase
{
    private readonly IMediator _mediator;

    private readonly MemoryCacheProvider _memoryCacheProvider;

    public TraitController(IMediator mediator, MemoryCacheProvider memoryCacheProvider)
    {
        _mediator = mediator;
        _memoryCacheProvider = memoryCacheProvider;
    }

    /// <summary>
    ///     This operation fetches a single feature
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("traits/{id}")]
    [ProducesResponseType(typeof(TraitViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFeature(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTraitQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a list of features
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet("traits")]
    [ProducesResponseType(typeof(IEnumerable<TraitViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTraits([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
        if (ids.Length == 0 && _memoryCacheProvider.Cache.TryGetValue(CacheKeys.GetAllTraits, out var traits))
        {
            return Ok(traits as List<TraitViewModel>);
        }
        
        var query = new GetTraitsQuery(ids);
        var result = (await _mediator.Send(query, cancellationToken)).ToList();
        
        if (ids.Length == 0)
        {
            _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllTraits, result);
        }
        
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a page of features
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("traits/page")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<TraitViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaginatedFeatures([FromQuery] int pageIndex, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetPaginatedTraitsQuery()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}