using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class IntegerCriteriaConverterTests
{
    [Fact]
    public void NumericCriteria_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""compare"": ""at_least"",
			    ""qualifier"": 10
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var stringCriteria = JsonConverterWrapper.Deserialize<IntegerCriteria>(json, jsonSettings)!;
        stringCriteria.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        stringCriteria.Qualifier.Should().Be(10);
    }
}