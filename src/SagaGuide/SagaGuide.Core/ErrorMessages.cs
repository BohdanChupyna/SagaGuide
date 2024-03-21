namespace SagaGuide.Core;

public static class ErrorMessages
{
    public const string CharacterNotFound = "Character not found";

    public const string SkillNotFound = "Skill not found";
    public const string PrerequisiteSkillNotFound = "Prerequisite '{0}' skill with ID {1} not found";
    public const string PrerequisiteSkillLevelIsTooLow = "Prerequisite '{0}' skill level is too low. Required level is {1}, but found {2}. Skill ID {3}";

    public const string PrerequisiteFeatureNotFound = "Prerequisite '{0}' feature {1} not found";
    public const string PrerequisiteFeatureModifierNotFound = "Prerequisite feature modifier {0} for feature {1} ID {2} not found";

    public const string FeatureNotFound = "Feature not found";

    public static string GetFromTemplate(string errorMessageTemplate, params object[] args)
    {
        return string.Format(errorMessageTemplate, args);
    }
}