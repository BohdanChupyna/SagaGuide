using SagaGuide.Core;
using SagaGuide.Core.Definitions.CharacterAggregate;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;
public class AddCharacterFeatureCommand : IRequest<CommandResponse<Guid?>>
{
    public AddCharacterFeatureCommand(AddCharacterFeatureDefinition definition) => Definition = definition;

    public AddCharacterFeatureDefinition Definition { get; init; }
}

public class AddCharacterFeatureCommandHandler : IRequestHandler<AddCharacterFeatureCommand, CommandResponse<Guid?>>
{
    private readonly ILogger<AddCharacterFeatureCommandHandler> _logger;
    private readonly ICharacterRepository _characterRepository;
    private readonly ITraitRepository _traitRepository;

    public AddCharacterFeatureCommandHandler(ILogger<AddCharacterFeatureCommandHandler> logger, ICharacterRepository repository, ITraitRepository traitRepository)
    {
        _logger = logger;
        _characterRepository = repository;
        _traitRepository = traitRepository;
    }

    public async Task<CommandResponse<Guid?>> Handle(AddCharacterFeatureCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Adding feature to Character");
        var result = new CommandResponse<Guid?>();

        var character = await _characterRepository.GetAsync(request.Definition.CharacterId, cancellationToken);
        if (character == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.CharacterNotFound);
            return result;
        }

        var feature = await _traitRepository.GetFeatureAsync(request.Definition.FeatureId, cancellationToken);
        if (feature == null)
        {
            result.AddError(StatusCodes.Status404NotFound, ErrorMessages.FeatureNotFound);
            return result;
        }

        result = character.AddTrait(feature, request.Definition);

        if (!result.IsSuccess)
            return result;

        _characterRepository.Update(character);
        await _characterRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return result;
    }
}