using System.Collections;
using Newtonsoft.Json;
using SagaGuide.Infrastructure.JsonConverters;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;

public static class EquipmentFileReader
{
    public static IList<Core.Domain.EquipmentAggregate.Equipment> Read(string fileName)
    {
        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        
        var json = File.ReadAllText(fileName);

        var equipmentList = JsonConvert.DeserializeObject<EquipmentList>(json, jsonSettings.SerializerSettings);

        return equipmentList?.Rows ?? Enumerable.Empty<Core.Domain.EquipmentAggregate.Equipment>().ToList();
    }

    public static void RemoveUnsupportedEquipmentFiles(ref List<string> filePaths)
    {
        filePaths.Remove(filePaths.Find(p => p.Contains("Low Tech - JBB Homebrew.eqp"))!);
    }
    
    private class EquipmentList
    {
        public int Version { get; set; }
        public IList<Core.Domain.EquipmentAggregate.Equipment> Rows { get; set; } = null!;
    }
}
