using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.IRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SagaGuide.Application.Commands.Character;
public class DeleteCharacterCommand : IRequest<CommandResponse<bool>>
{
    public DeleteCharacterCommand(Guid id) => Id = id;

    public Guid Id { get; init; }
}

public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, CommandResponse<bool>>
{
    private readonly ILogger<DeleteCharacterCommandHandler> _logger;
    private readonly IMediator _mediator;
    private readonly ICharacterRepository _repository;

    public DeleteCharacterCommandHandler(IMediator mediator, ILogger<DeleteCharacterCommandHandler> logger, ICharacterRepository repository)
    {
        _mediator = mediator;
        _logger = logger;
        _repository = repository;
    }

    public async Task<CommandResponse<bool>> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting Character with id {request.Id}");

        var response = new CommandResponse<bool>();
        
        var result = await _repository.DeleteAsync(request.Id, cancellationToken);
        if (result)
        {
            response.AddInformation(StatusCodes.Status204NoContent);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        else
        {
            response.AddError(StatusCodes.Status404NotFound, ErrorMessages.CharacterNotFound);
        }
        
        return response;
    }
}