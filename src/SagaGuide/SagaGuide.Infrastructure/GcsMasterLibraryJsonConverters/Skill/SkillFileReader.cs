using SagaGuide.Core.Domain.TechniqueAggregate;
using Newtonsoft.Json.Linq;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;

public static class SkillFileReader
{
    public static void Read(string filePath, out List<Core.Domain.SkillAggregate.Skill> skills, out List<Technique> techniques)
    {
        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
      
        var json = File.ReadAllText(filePath);

        skills = new List<Core.Domain.SkillAggregate.Skill>();
        techniques = new List<Technique>();

        var skillList = JObject.Parse(json);
        var rows = skillList.GetValue("rows")!;
        foreach (var child in rows.Children())
        {
            if (child.Type == JTokenType.Object)
            {
                var obj = (JObject)child;
                var type = obj.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToString();
                switch (type)
                {
                    case "skill":
                    {
                        var skill = JsonConverterWrapper.Deserialize<Core.Domain.SkillAggregate.Skill>(obj.ToString(), jsonSettings);
                        if(skill != null) 
                            skills.Add(skill);
                        break;
                    }
                    case "technique":
                        var technique = JsonConverterWrapper.Deserialize<Technique>(obj.ToString(), jsonSettings);
                        if(technique != null) 
                            techniques.Add(technique);
                        break;
                }
            }
        }
    }
}

