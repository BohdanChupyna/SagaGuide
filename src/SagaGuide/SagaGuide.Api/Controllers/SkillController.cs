using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Skill;
using SagaGuide.Api.Queries.Skill;

namespace SagaGuide.Api.Controllers;

[ApiController]
[Route(RouteTemplates.DbApi)]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]

public class SkillController : SagaGuideControllerBase
{
    private readonly IMediator _mediator;
    private readonly MemoryCacheProvider _memoryCacheProvider;
    
    public SkillController(IMediator mediator, MemoryCacheProvider memoryCacheProvider)
    {
        _mediator = mediator;
        _memoryCacheProvider = memoryCacheProvider;
    }

    /// <summary>
    ///     This operation fetches a single skill
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "skills/{id}")]
    [ProducesResponseType(typeof(SkillViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSkill(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSkillQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a list of skills
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet( "skills")]
    [ProducesResponseType(typeof(IEnumerable<SkillViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSkills([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
      
        if (ids.Length == 0 && _memoryCacheProvider.Cache.TryGetValue(CacheKeys.GetAllSkills, out var skills))
        {
            return Ok(skills as List<SkillViewModel>);
        }
       
        var query = new GetSkillsQuery(ids);
        var result = await _mediator.Send(query, cancellationToken);

        if (ids.Length == 0)
        {
            _memoryCacheProvider.UpdateCacheKey(CacheKeys.GetAllSkills, result.ToList());
        }
        
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a page of skills
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("skills/page")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<SkillViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaginatedSkills([FromQuery] int pageIndex, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetPaginatedSkillsQuery()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}