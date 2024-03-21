using SagaGuide.Core.Domain.CharacterAggregate;
using Mapster;
using MediatR;
using SagaGuide.Api.Contract.Character;

namespace SagaGuide.Api.Queries.Character;

public class GetUnregisteredCharacterQuery : IRequest<CharacterViewModel>
{
}

public class GetUnregisteredCharacterQueryHandler : IRequestHandler<GetUnregisteredCharacterQuery, CharacterViewModel>
{
    private readonly ILogger<GetUnregisteredCharacterQueryHandler> _logger;
    private readonly CharacterFactory _factory;

    public GetUnregisteredCharacterQueryHandler(ILogger<GetUnregisteredCharacterQueryHandler> logger, CharacterFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    public async Task<CharacterViewModel> Handle(GetUnregisteredCharacterQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating unregistered character");

        var character = await _factory.CreateAsync(cancellationToken);
        character.Name = "Unregistered Character";

        return character.Adapt<CharacterViewModel>();
    }
}