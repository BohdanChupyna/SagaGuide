using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using Mapster;
using Newtonsoft.Json;

namespace SagaGuide.Api.Mapping;

public static class MyMapsterConfiguration
{
    public static void ConfigureMapster()
    {
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        
        TypeAdapterConfig<IPrerequisite, IPrerequisite>.NewConfig()
            .MapWith(src => DeepCopy(src));
        
        TypeAdapterConfig<IFeature, IFeature>.NewConfig()
            .MapWith(src => DeepCopy(src));
        
        TypeAdapterConfig<Attack, Attack>.NewConfig()
            .MapWith(src => DeepCopy(src));
        
    }
    
    private static T DeepCopy<T>(T src)
    {
        var jsonSettings = JsonSettingsWrapper.Create();
        jsonSettings.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
        var json = JsonConverterWrapper.Serialize(src, jsonSettings);
        return JsonConverterWrapper.Deserialize<T>(json, jsonSettings)!;
    }
    
}