using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;
public class CreateCharacterCommand : IRequest<CommandResponse<Guid?>>
{
    public Core.Domain.CharacterAggregate.Character? Character { get; set; }
}

public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, CommandResponse<Guid?>>
{
    private readonly ILogger<CreateCharacterCommandHandler> _logger;
    private readonly CharacterFactory _factory;
    private readonly ICharacterRepository _repository;

    public CreateCharacterCommandHandler(ILogger<CreateCharacterCommandHandler> logger, CharacterFactory factory, ICharacterRepository repository)
    {
        _logger = logger;
        _factory = factory;
        _repository = repository;
    }

    public async Task<CommandResponse<Guid?>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating Character");

        var character = await _factory.CreateAsync(cancellationToken);

        if (request.Character != null)
        {
            request.Character.Id = character.Id;
            request.Character.UserId = character.UserId;
            request.Character.Version = character.Version;
            request.Character.CreatedBy = character.CreatedBy;
            request.Character.CreatedOn = character.CreatedOn;
            request.Character.ModifiedBy = character.ModifiedBy;
            request.Character.ModifiedOn = character.ModifiedOn;
            character = request.Character;
        }

        _repository.Add(character);
        await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        var result = new CommandResponse<Guid?>
        {
            Value = character.Id
        };
        result.AddInformation(StatusCodes.Status201Created);
        return result;
    }
}