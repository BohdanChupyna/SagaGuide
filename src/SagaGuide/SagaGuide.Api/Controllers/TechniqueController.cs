using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract.Technique;
using SagaGuide.Api.Queries.Technique;

namespace SagaGuide.Api.Controllers;

[ApiController]
[Route(RouteTemplates.DbApi)]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class TechniqueController : SagaGuideControllerBase
{
    private readonly IMediator _mediator;
    private readonly MemoryCacheProvider _memoryCacheProvider;

    public TechniqueController(IMediator mediator, MemoryCacheProvider memoryCacheProvider)
    {
        _mediator = mediator;
        _memoryCacheProvider = memoryCacheProvider;
    }

    /// <summary>
    /// This operation fetches a list of techniques
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet( "techniques")]
    [ProducesResponseType(typeof(IEnumerable<TechniqueViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTechniques([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
        if (ids.Length == 0 && _memoryCacheProvider.Cache.TryGetValue(CacheKeys.GetAllTechniques, out var techniques))
        {
            return Ok(techniques as List<TechniqueViewModel>);
        }
        
        var query = new GetTechniquesQuery(ids);
        var result = (await _mediator.Send(query, cancellationToken)).ToList();
        
        if (ids.Length == 0)
        {
            _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllTechniques, result);
        }
        
        return Ok(result);
    }
}