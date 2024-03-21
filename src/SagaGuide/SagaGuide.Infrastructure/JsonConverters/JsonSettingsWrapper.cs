using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Feature;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Prerequisite;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;
using SagaGuide.Infrastructure.JsonConverters.Converters;

namespace SagaGuide.Infrastructure.JsonConverters;

public class JsonSettingsWrapper
{
    public JsonSettingsWrapper() => SerializerSettings = CreateNewtonsoftSettings();

    public JsonSerializerSettings SerializerSettings { get; private set; }

    public static JsonSettingsWrapper Create(JsonSerializerSettings settings)
    {
        var settingsWrapper = Create();
        settingsWrapper.SerializerSettings = CreateNewtonsoftSettings(settings);
        return settingsWrapper;
    }

    public static JsonSettingsWrapper Create(JsonSettingsWrapper? settings = null)
    {
        if (settings != null)
        {
            settings.SerializerSettings = CreateNewtonsoftSettings(settings.SerializerSettings);
            return settings;
        }

        settings = new JsonSettingsWrapper();
        return settings;
    }

    public static JsonSettingsWrapper CreateGcsMasterLibrarySettings()
    {
        var settings = new JsonSerializerSettings();
        // GL 8.05 - when value is null, the property does not appear in json, and vice versa
        settings.NullValueHandling = NullValueHandling.Ignore;
        // GL 8.01 - camel_case property names
        settings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false
            }
        };
        
        settings.Converters.Add(new TrimmingStringJsonConverter());
        
        settings.Converters.Add(new SkillConverter());
        settings.Converters.Add(new SkillDefaultConverter());
        settings.Converters.Add(new IntegerCriteriaConverter());
        settings.Converters.Add(new DoubleCriteriaConverter());
        settings.Converters.Add(new StringCriteriaConverter());
        settings.Converters.Add(new PrerequisiteConverter());
        settings.Converters.Add(new TechniqueConverter());
        
        settings.Converters.Add(new TraitConverter());
        settings.Converters.Add(new TraitModifierConverter());
        settings.Converters.Add(new TraitModifierGroupConverter());
        
        settings.Converters.Add(new FeatureConverter());
        
        settings.Converters.Add(new EquipmentConverter());
        settings.Converters.Add(new AttackConverter());
        settings.Converters.Add(new DamageConverter());
        settings.Converters.Add(new EquipmentModifierConverter());

        var settingsWrapper = new JsonSettingsWrapper
        {
            SerializerSettings = settings
        };
        return settingsWrapper;
    }
    
    private static JsonSerializerSettings CreateNewtonsoftSettings(JsonSerializerSettings? settings = null)
    {
        settings ??= new JsonSerializerSettings();

        // GL 8.05 - when value is null, the property does not appear in json, and vice versa
        settings.NullValueHandling = NullValueHandling.Ignore;
        // GL 8.01 - camel_case property names
        settings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false
            }
        };
        
        settings.TypeNameHandling = TypeNameHandling.Auto;
        
        // Automatically convert enums to strings and vice versa
        settings.Converters.Add(new StringEnumJsonConverter());
        settings.Converters.Add(new ProblemDetailsConverter());
        settings.Converters.Add(new TrimmingStringJsonConverter());
#if DEBUG
        // No GL, just developer comfort
        settings.Formatting = Formatting.Indented;
#endif
        return settings;
    }

    public static JsonSettingsWrapper CreateNewtonsoftSettingsForMediatorLoggingBehavior()
    {
        var settings = CreateNewtonsoftSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
        var settingsWrapper = new JsonSettingsWrapper
        {
            SerializerSettings = settings
        };
        return settingsWrapper;
    }
    
    public void AddConverter(Newtonsoft.Json.JsonConverter converter)
    {
        SerializerSettings.Converters.Add(converter);
    }

    public void SetWriteIndented(bool writeIndented)
    {
        SerializerSettings.Formatting = writeIndented ? Formatting.Indented : Formatting.None;
    }
}