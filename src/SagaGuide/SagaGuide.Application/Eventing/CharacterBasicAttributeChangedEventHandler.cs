//using SagaGuide.Core.Domain.CharacterAggregate;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace SagaGuide.Application.Eventing;

//public class CharacterBasicAttributeChangedEventHandler : INotificationHandler<CharacterBasicAttributeChangedEvent>
//{
//    private readonly ILogger<CharacterBasicAttributeChangedEventHandler> _logger;

//    public CharacterBasicAttributeChangedEventHandler(ILogger<CharacterBasicAttributeChangedEventHandler> logger)
//    {
//        _logger = logger;
//    }

//    public Task Handle(CharacterBasicAttributeChangedEvent notification, CancellationToken cancellationToken)
//    {
//        var character = notification.Attribute.Character;

//        _logger.LogInformation($"Handling CharacterBasicAttributeChangedEvent for characterId{character.Id}");

//        switch (notification.Attribute.BasicAttribute.Type)
//        {
//            case AttributeType.Strength:
//            case AttributeType.Intelligence:
//                UpdateLinearDependedCharacteristics(character, notification.Attribute);
//                break;
//            case AttributeType.Dexterity:
//            case AttributeType.Health:
//            case AttributeType.DexterityAndHealth:
//                UpdateLinearDependedCharacteristics(character, notification.Attribute);
//                UpdateBasicSpeedAndBasicMove(character);
//                break;
//            default:
//                throw new ArgumentOutOfRangeException();
//        }

//        return Task.CompletedTask;
//    }

//    private void UpdateLinearDependedCharacteristics(Character character, CharacterBasicAttribute attribute)
//    {
//        var characteristics = character.Characteristics.Where(c => c.Characteristic.DependOnAttributeType == attribute.BasicAttribute.Type);
//        foreach (var characteristic in characteristics)
//        {
//            _logger.LogInformation($"Updating {characteristic.Characteristic.Type} with old value {characteristic.Value}");
//            characteristic.Value = attribute.Value + CalculateValueProvidedBySpentPoint(characteristic);
//            _logger.LogInformation($"Characteristic {characteristic.Characteristic.Type} new value is {characteristic.Value}");
//        }
//    }

//    private void UpdateBasicSpeedAndBasicMove(Character character)
//    {
//        var dexterity = character.Attributes.Single(a => a.BasicAttribute.Type == AttributeType.Dexterity);
//        var health = character.Attributes.Single(a => a.BasicAttribute.Type == AttributeType.Health);

//        var basicSpeed = character.Characteristics.Single(c => c.Characteristic.Type == CharacteristicType.BasicSpeed);
//        _logger.LogInformation($"Updating {basicSpeed.Characteristic.Type} with old value {basicSpeed.Value}");
//        basicSpeed.Value = (dexterity.Value + health.Value) / 4.0 + CalculateValueProvidedBySpentPoint(basicSpeed);
//        _logger.LogInformation($"Characteristic {basicSpeed.Characteristic.Type} new value is {basicSpeed.Value}");

//        var basicMove = character.Characteristics.Single(c => c.Characteristic.Type == CharacteristicType.BasicMove);
//        _logger.LogInformation($"Updating {basicMove.Characteristic.Type} with old value {basicMove.Value}");
//        basicMove.Value = Math.Floor(basicSpeed.Value) + CalculateValueProvidedBySpentPoint(basicMove);
//        _logger.LogInformation($"Characteristic {basicMove.Characteristic.Type} new value is {basicMove.Value}");
//    }

//    private static double CalculateValueProvidedBySpentPoint(CharacterSecondaryCharacteristic  characteristic) => characteristic.Characteristic.ValueIncreasePerLevel * characteristic.SpentPoints / characteristic.Characteristic.PointsPerLevel;
//}