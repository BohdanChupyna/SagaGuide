using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;

public class DeleteCharacterFeatureCommand : IRequest<CommandResponse<bool>>
{
    public DeleteCharacterFeatureCommand(Guid characterId, Guid characterFeatureId)
    {
        CharacterId = characterId;
        CharacterFeatureId = characterFeatureId;
    }

    public Guid CharacterId { get; init; }
    public Guid CharacterFeatureId { get; init; }
}

public class DeleteCharacterFeatureCommandHandler : IRequestHandler<DeleteCharacterFeatureCommand, CommandResponse<bool>>
{
    private readonly ILogger<DeleteCharacterFeatureCommandHandler> _logger;
    private readonly ICharacterRepository _repository;

    public DeleteCharacterFeatureCommandHandler(ILogger<DeleteCharacterFeatureCommandHandler> logger, ICharacterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<CommandResponse<bool>> Handle(DeleteCharacterFeatureCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting character feature {request.CharacterFeatureId} from Character {request.CharacterId}");
        var result = new CommandResponse<bool>();

        var character = await _repository.GetAsync(request.CharacterId, cancellationToken);
        if (character == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.CharacterNotFound);
            return result;
        }

        result = character.DeleteTrait(request.CharacterFeatureId);

        if (!result.IsSuccess)
            return result;

        _repository.Update(character);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return result;
    }
}