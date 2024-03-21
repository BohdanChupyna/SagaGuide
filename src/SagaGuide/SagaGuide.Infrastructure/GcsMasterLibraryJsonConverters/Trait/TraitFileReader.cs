using SagaGuide.Core.Domain.TechniqueAggregate;
using Newtonsoft.Json.Linq;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;

public static class TraitFileReader
{
    public static void Read(string filePath, out List<Core.Domain.TraitAggregate.Trait> traits)
    {
        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
      
        var json = File.ReadAllText(filePath);

        traits = new List<Core.Domain.TraitAggregate.Trait>();

        JObject traitList = JObject.Parse(json);
        var rows = traitList.GetValue("rows")!;
        foreach (var child in rows.Children())
        {
            if (child.Type == JTokenType.Object)
            {
                var obj = (JObject)child;
                var type = obj.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToString();
                switch (type)
                {
                    case "trait":
                    {
                        var trait = JsonConverterWrapper.Deserialize<Core.Domain.TraitAggregate.Trait>(obj.ToString(), jsonSettings);
                        if(trait != null) 
                            traits.Add(trait);
                        break;
                    }
                }
            }
        }
    }
}