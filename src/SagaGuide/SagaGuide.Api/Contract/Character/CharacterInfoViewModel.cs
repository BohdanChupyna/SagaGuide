namespace SagaGuide.Api.Contract.Character;

public class CharacterInfoViewModel: AuditableViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Player { get; set; } = string.Empty;
    public string Campaign { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}