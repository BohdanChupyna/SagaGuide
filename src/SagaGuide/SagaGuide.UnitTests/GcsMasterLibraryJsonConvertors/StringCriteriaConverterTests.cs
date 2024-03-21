using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class StringCriteriaConverterTests
{
    [Fact]
    public void StringCriteria_IsCorrectlyParsed()
    {
        const string json = @"
            {
                ""compare"": ""is"",
                ""qualifier"": ""hyperspace""
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var stringCriteria = JsonConverterWrapper.Deserialize<StringCriteria>(json, jsonSettings)!;
        stringCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        stringCriteria.Qualifier.Should().Be("hyperspace");
    }
}