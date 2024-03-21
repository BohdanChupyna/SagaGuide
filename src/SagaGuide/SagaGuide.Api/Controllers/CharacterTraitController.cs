// using SagaGuide.Api.Constants;
// using SagaGuide.Api.Contract.Character;
// using SagaGuide.Application.Commands.Character;
// using SagaGuide.Core.Definitions.CharacterAggregate;
// using Mapster;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using System.Net.Mime;
//
// namespace SagaGuide.Api.Controllers
// {
//     [ApiController]
//     [Route(RouteTemplates.CharacterApi + "/{characterId:guid}/traits")]
//     [Consumes(MediaTypeNames.Application.Json)]
//     [Produces(MediaTypeNames.Application.Json)]
//     public class CharacterTraitController : SagaGuideControllerBase
//     {
//         private readonly IMediator _mediator;
//
//         public CharacterTraitController(IMediator mediator) => _mediator = mediator;
//
//         /// <summary>
//         ///     This operation adds feature to character
//         /// </summary>
//         /// <param name="characterId"></param>
//         /// /// <param name="viewModel"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         [HttpPost]
//         //[Authorize(Authorizations.ReadGurpsRules)]
//         [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status201Created)]
//         [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status404NotFound)]
//         public async Task<IActionResult> PostFeature([FromRoute] Guid characterId, [FromBody] AddCharacterTraitViewModel viewModel, CancellationToken cancellationToken)
//         {
//             var definition = viewModel.Adapt<AddCharacterFeatureDefinition>();
//             definition.CharacterId = characterId;
//             var query = new AddCharacterFeatureCommand(definition);
//
//             var result = await _mediator.Send(query, cancellationToken);
//
//             return ActionResultForCommandResponse(result);
//         }
//
//         /// <summary>
//         ///     This operation deletes feature from character
//         /// </summary>
//         /// <param name="characterId"></param>
//         /// /// <param name="characterFeatureId"></param>
//         /// <param name="cancellationToken"></param>
//         /// <returns></returns>
//         [HttpDelete("{characterFeatureId:guid}")]
//         //[Authorize(Authorizations.ReadGurpsRules)]
//         [ProducesResponseType(typeof(CharacterViewModel), StatusCodes.Status200OK)]
//         public async Task<IActionResult> DeleteFeature([FromRoute] Guid characterId, [FromRoute] Guid characterFeatureId, CancellationToken cancellationToken)
//         {
//             var query = new DeleteCharacterFeatureCommand(characterId, characterFeatureId);
//             var result = await _mediator.Send(query, cancellationToken);
//             return ActionResultForCommandResponse(result);
//         }
//     }
// }
