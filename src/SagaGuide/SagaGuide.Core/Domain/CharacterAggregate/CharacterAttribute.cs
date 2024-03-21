using SagaGuide.Core.Domain.Common;

namespace SagaGuide.Core.Domain.CharacterAggregate;

public class CharacterAttribute : GuidEntity
{
    public Attribute Attribute { get; set; } = null!;
    
    // ToDo: Update implementation in case of need it's value on back-end side
    // public double Value
    // {
    //     get
    //     {
    //         // true for basic attributes: like dexterity.
    //         if(Attribute.DependOnAttributeType == null && Attribute.DefaultValue != null)
    //             return Attribute.DefaultValue.Value + CalculateValueProvidedBySpentPoint();
    //         
    //         double attributeValue;
    //         if (Attribute.Type is Attribute.AttributeType.BasicMove or Attribute.AttributeType.BasicSpeed)
    //         {
    //             var dexterity = Character.Attributes.Single(a => a.Attribute.Type == Attribute.AttributeType.Dexterity);
    //             var health = Character.Attributes.Single(a => a.Attribute.Type == Attribute.AttributeType.Health);
    //
    //             attributeValue = (dexterity.Value + health.Value) / 4.0;
    //             
    //             if (Attribute.Type == Attribute.AttributeType.BasicMove)
    //                 attributeValue = Math.Floor(attributeValue);
    //         }
    //         else
    //         {
    //             attributeValue = Character.Attributes.Single(a => a.Attribute.Type == Attribute.DependOnAttributeType).Value;
    //         }
    //
    //         return attributeValue + CalculateValueProvidedBySpentPoint();
    //     }
    // }

    public int SpentPoints { get; set; }
    
    private double CalculateValueProvidedBySpentPoint() => Attribute.ValueIncreasePerLevel * SpentPoints / Attribute.PointsCostPerLevel;
}