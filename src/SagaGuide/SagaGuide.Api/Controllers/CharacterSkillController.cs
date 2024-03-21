// using System.Net.Mime;
// using SagaGuide.Api.Constants;
// using SagaGuide.Api.Contract.Character;
// using SagaGuide.Application.Commands.Character;
// using SagaGuide.Core.Definitions.CharacterAggregate;
// using Mapster;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
//
// namespace SagaGuide.Api.Controllers;
//
// [ApiController]
// [Route(RouteTemplates.CharacterApi + "/{characterId:guid}/skills")]
// [Consumes(MediaTypeNames.Application.Json)]
// [Produces(MediaTypeNames.Application.Json)]
// public class CharacterSkillController : SagaGuideControllerBase
// {
//     private readonly IMediator _mediator;
//
//     public CharacterSkillController(IMediator mediator) => _mediator = mediator;
//     
//     /// <summary>
//     ///     This operation fetches a single character
//     /// </summary>
//     /// <param name="characterId"></param>
//     /// /// <param name="viewModel"></param>
//     /// <param name="cancellationToken"></param>
//     /// <returns></returns>
//     [HttpPost]
//     //[Authorize(Authorizations.ReadGurpsRules)]
//     [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status201Created)]
//     [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> PostSkill([FromRoute] Guid characterId, [FromBody] AddCharacterSkillViewModel viewModel, CancellationToken cancellationToken)
//     {
//         var definition = viewModel.Adapt<AddCharacterSkillDefinition>();
//         definition.CharacterId = characterId;
//         var query = new AddCharacterSkillCommand(definition);
//         
//         var result = await _mediator.Send(query, cancellationToken);
//         
//         return ActionResultForCommandResponse(result);
//     }
//     
//     /// <summary>
//     ///     This operation deletes a single character
//     /// </summary>
//     /// <param name="characterId"></param>
//     /// /// <param name="characterSkillId"></param>
//     /// <param name="cancellationToken"></param>
//     /// <returns></returns>
//     [HttpDelete( "{characterSkillId:guid}")]
//     //[Authorize(Authorizations.ReadGurpsRules)]
//     [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
//     public async Task<IActionResult> DeleteSkill([FromRoute] Guid characterId,[FromRoute] Guid characterSkillId, CancellationToken cancellationToken)
//     {
//         var query = new DeleteCharacterSkillCommand(characterId, characterSkillId);
//         var result = await _mediator.Send(query, cancellationToken);
//         return ActionResultForCommandResponse(result);
//     }
// }