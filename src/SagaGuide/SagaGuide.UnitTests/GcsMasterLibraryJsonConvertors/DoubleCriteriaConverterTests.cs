using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class DoubleCriteriaConverterTests
{
    [Fact]
    public void NumericCriteria_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""compare"": ""at_least"",
			    ""qualifier"": 22.2
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var stringCriteria = JsonConverterWrapper.Deserialize<DoubleCriteria>(json, jsonSettings)!;
        stringCriteria.Comparison.Should().Be(DoubleCriteria.ComparisonType.AtLeast);
        stringCriteria.Qualifier.Should().Be(22.2);
    }
}