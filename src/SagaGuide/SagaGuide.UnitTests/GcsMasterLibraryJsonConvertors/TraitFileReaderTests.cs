using System.Collections.Generic;
using System.IO;
using System.Linq;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.EntityFramework.GcsMasterLibrarySeeder;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Trait;
using SagaGuide.Infrastructure.JsonConverters.EqualityComparers;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class TraitFileReaderTests
{
    [Fact]
    public void ParseAllGcsLibraryTraits_Success()
    {
        List<string> filePaths = TestUtils.GetFilePathsByExtension(TestUtils.GcsMasterLibraryFolderPath, GcsMasterLibraryFileExtensions.Traits);
    
        var traits = new List<Trait>();
        //var filePath = "D:\\development\\gcs\\gcs_master_library-master\\Library\\Basic Set\\Basic Set Skills.skl";
    
        foreach (var filePath in filePaths)
        {
            TraitFileReader.Read(filePath, out var res);
            traits.AddRange(res);
        }

        
        // var DisabledTrue = traits.Where(t => t.RoundCostDown.HasValue && t.RoundCostDown.Value).Select(t => t.Id)
        //     .ToList();
        //
        // var DisabledFalse = traits.Where(t => t.RoundCostDown.HasValue && !t.RoundCostDown.Value).Select(t => t.Id)
        //     .ToList();
        //
        // var DisabledNull = traits.Where(t => !t.RoundCostDown.HasValue).Select(t => t.Id)
        //     .ToList();
        
        var count = traits.Count;
        var uniqueTraits = new TraitEqualityCompare().UnifyDateTimeAndDistinct(traits).ToList().Count;
    }
}