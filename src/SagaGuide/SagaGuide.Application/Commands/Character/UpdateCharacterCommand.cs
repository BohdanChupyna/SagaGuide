using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.EnvironmentAbstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;

public class UpdateCharacterCommand : IRequest<CommandResponse<Core.Domain.CharacterAggregate.Character?>>
{
    public UpdateCharacterCommand(Core.Domain.CharacterAggregate.Character character) => Character = character;

    public Core.Domain.CharacterAggregate.Character Character { get; init; }
}

public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, CommandResponse<Core.Domain.CharacterAggregate.Character?>>
{
    private readonly ILogger<UpdateCharacterCommandHandler> _logger;
    private readonly ICharacterRepository _repository;
    private readonly IUserAccessor _userAccessor;

    public UpdateCharacterCommandHandler(ILogger<UpdateCharacterCommandHandler> logger, ICharacterRepository repository, IUserAccessor userAccessor)
    {
        _logger = logger;
        _repository = repository;
        _userAccessor = userAccessor;
    }

    public async Task<CommandResponse<Core.Domain.CharacterAggregate.Character?>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating Character with id {request.Character.Id}");

        var response = new CommandResponse<Core.Domain.CharacterAggregate.Character?>();

        var character = await _repository.GetAsNoTrackingAsync(request.Character.Id, cancellationToken);

        if (character == null)
        {
            response.AddError(StatusCodes.Status404NotFound, $"Character with id {request.Character.Id} does not exist.");
            return response;
        }

        try
        {
            _repository.Update(request.Character);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException e)
        {
            response.AddError(StatusCodes.Status400BadRequest, $"Character with id {request.Character.Id} is outdated or deleted.");
            return response;
        }
        catch (Exception e)
        {
            response.AddError(StatusCodes.Status500InternalServerError, e.Message);
            return response;
        }
        
        // Should get character from DB to get updated ModifiedOn property
        response.Value = await _repository.GetAsync(request.Character.Id, cancellationToken);
        response.AddInformation(StatusCodes.Status200OK);
        return response;
    }
}