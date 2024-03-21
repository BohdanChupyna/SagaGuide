using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;
public class DeleteCharacterSkillCommand : IRequest<CommandResponse<bool>>
{
    public DeleteCharacterSkillCommand(Guid characterId, Guid characterSkillId)
    {
        CharacterId = characterId;
        CharacterSkillId = characterSkillId;
    }

    public Guid CharacterId { get; init; }
    public Guid CharacterSkillId { get; init; }
}

public class DeleteCharacterSkillCommandHandler : IRequestHandler<DeleteCharacterSkillCommand, CommandResponse<bool>>
{
    private readonly ILogger<DeleteCharacterSkillCommandHandler> _logger;
    private readonly ICharacterRepository _repository;

    public DeleteCharacterSkillCommandHandler(ILogger<DeleteCharacterSkillCommandHandler> logger, ICharacterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<CommandResponse<bool>> Handle(DeleteCharacterSkillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting character skill {request.CharacterSkillId} from Character {request.CharacterId}");
        var result = new CommandResponse<bool>();
        
        var character = await _repository.GetAsync(request.CharacterId, cancellationToken);
        if (character == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.CharacterNotFound);
            return result;
        }

        result = character.DeleteSkill(request.CharacterSkillId);

        if (!result.IsSuccess) 
            return result;
        
        _repository.Update(character);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return result;
    }
}