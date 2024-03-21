using System.Net.Mime;
using SagaGuide.Application.Commands.Character;
using SagaGuide.Core.Domain.CharacterAggregate;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Character;
using SagaGuide.Api.Queries.Character;

namespace SagaGuide.Api.Controllers;

[ApiController]
[Route(RouteTemplates.CharacterApi)]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class CharacterController : SagaGuideControllerBase
{
    private readonly IMediator _mediator;

    public CharacterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     This operation fetches a single character
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "unregistered/")]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUnregisteredCharacter(CancellationToken cancellationToken)
    {
        var query = new GetUnregisteredCharacterQuery();
        var createdCharacter = await _mediator.Send(query, cancellationToken);

        return Created(Request.GetDisplayUrl(), createdCharacter);
    }
    
    /// <summary>
    ///     This operation fetches a single character
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet( "{id}")]
    [Authorize(Authorizations.ReadCharacters)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCharacter([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCharacterQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result != null ? Ok(result) : NotFound();
    }
    
    /// <summary>
    /// This operation fetches a list of characters
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Authorizations.ReadCharacters)]
    [ProducesResponseType(typeof(IEnumerable<CharacterViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCharacters([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
        var query = new GetCharactersQuery(ids);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// This operation fetches a page of Characters
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <returns></returns>
    [HttpGet("page")]
    [Authorize(Authorizations.ReadCharacters)]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<CharacterViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPaginatedCharacters([FromQuery] int pageIndex, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var query = new GetPaginatedCharactersQuery()
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
        };

        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// This operation fetches a list of characters info
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="ids"></param>
    /// <returns></returns>
    [HttpGet("info")]
    [Authorize(Authorizations.ReadCharacters)]
    [ProducesResponseType(typeof(IEnumerable<CharacterInfoViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCharactersInfo([FromQuery] Guid[] ids, CancellationToken cancellationToken)
    {
        var query = new GetCharactersInfoQuery(ids);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    ///     This operation creates a single character
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Created character</returns>
    [HttpPost]
    [Authorize(Authorizations.WriteCharacter)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCharacter([FromBody] CharacterViewModel? viewModel, CancellationToken cancellationToken)
    {
        var command = new CreateCharacterCommand{Character = viewModel?.Adapt<Character>()};
        var createCharacterResponse = await _mediator.Send(command, cancellationToken);

        if (!createCharacterResponse.IsSuccess)
            return ActionResultForCommandResponse(createCharacterResponse);
        
        var query = new GetCharacterQuery(createCharacterResponse.Value!.Value);
        var result = await _mediator.Send(query, cancellationToken);
        return Created(Request.GetDisplayUrl(), result);
    }
    
    /// <summary>
    ///     This operation updates a single character
    /// </summary>
    /// <param name="viewModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>CharacterViewModel</returns>
    [HttpPut]
    [Authorize(Authorizations.WriteCharacter)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCharacter([FromBody] CharacterViewModel viewModel, CancellationToken cancellationToken)
    {
        var character = viewModel.Adapt<Character>();
        
        var command = new UpdateCharacterCommand(character);
        var response = await _mediator.Send(command, cancellationToken);

        return response.IsSuccess ? Ok(response.Value?.Adapt<CharacterViewModel>()) : ActionResultForCommandResponse(response);
    }
    
    /// <summary>
    ///     This operation deletes a single character
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete( "{id}")]
    [Authorize(Authorizations.WriteCharacter)]
    [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteCharacter([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCharacterCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return ActionResultForCommandResponse(result);
    }
}