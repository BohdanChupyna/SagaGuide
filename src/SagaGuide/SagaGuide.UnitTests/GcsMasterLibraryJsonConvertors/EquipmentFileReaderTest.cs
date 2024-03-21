using System.Collections.Generic;
using System.Linq;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Infrastructure.EntityFramework.GcsMasterLibrarySeeder;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;
using SagaGuide.Infrastructure.JsonConverters.EqualityComparers;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class EquipmentFileReaderTest
{
    [Fact]
    public void ParseAllGcsLibraryEquipment_Success()
    {
        var filePaths = TestUtils.GetFilePathsByExtension(TestUtils.GcsMasterLibraryFolderPath, GcsMasterLibraryFileExtensions.Equipment);
        EquipmentFileReader.RemoveUnsupportedEquipmentFiles(ref filePaths);
        var equipment = new List<Equipment>();
    
        foreach (var filePath in filePaths)
        {
            equipment.AddRange(EquipmentFileReader.Read(filePath));
        }
        
        var weaponsCount = equipment.Count(e => e.Attacks.Count > 0);
        var equipmentCount = equipment.Count - weaponsCount;
        
        var result = new EquipmentEqualityCompare().UnifyDateTimeAndDistinct(equipment).ToList();
        var weaponsResultCount = result.Count(e => e.Attacks.Count > 0);
        var equipmentResultCount = result.Count - weaponsCount;
    }
}