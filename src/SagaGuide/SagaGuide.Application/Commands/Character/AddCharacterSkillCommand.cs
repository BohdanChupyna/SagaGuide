using SagaGuide.Core;
using SagaGuide.Core.Definitions.CharacterAggregate;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.SkillAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;
public class AddCharacterSkillCommand : IRequest<CommandResponse<Guid?>>
{
    public AddCharacterSkillCommand(AddCharacterSkillDefinition definition) => Definition = definition;

    public AddCharacterSkillDefinition Definition { get; init; }
}

public class AddCharacterSkillCommandHandler : IRequestHandler<AddCharacterSkillCommand, CommandResponse<Guid?>>
{
    private readonly ILogger<AddCharacterSkillCommandHandler> _logger;
    private readonly ICharacterRepository _repository;
    private readonly ISkillRepository _skillRepository;

    public AddCharacterSkillCommandHandler(ILogger<AddCharacterSkillCommandHandler> logger, ICharacterRepository repository, ISkillRepository skillRepository)
    {
        _logger = logger;
        _repository = repository;
        _skillRepository = skillRepository;
    }

    public async Task<CommandResponse<Guid?>> Handle(AddCharacterSkillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding skill to Character");
        var result = new CommandResponse<Guid?>();
        
        var character = await _repository.GetAsync(request.Definition.CharacterId, cancellationToken);
        if (character == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.CharacterNotFound);
            return result;
        }

        var skill = await _skillRepository.GetSkill(request.Definition.SkillId, cancellationToken);
        if (skill == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.SkillNotFound);
            return result;
        }
        
        result = character.AddSkill(skill, request.Definition);

        if (!result.IsSuccess) 
            return result;
        
        _repository.Update(character);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return result;
    }
}