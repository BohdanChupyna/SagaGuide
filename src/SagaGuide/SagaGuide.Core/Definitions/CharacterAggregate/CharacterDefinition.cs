namespace SagaGuide.Core.Definitions.CharacterAggregate;

public class CharacterDefinition : AuditableDefinitionBase
{
    public string Name { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Handedness { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;
    public double Age { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public int TechLevel { get; set; }
    public double Size { get; set; }
    public int HpLose { get; set; }
    public int FpLose { get; set; }
}