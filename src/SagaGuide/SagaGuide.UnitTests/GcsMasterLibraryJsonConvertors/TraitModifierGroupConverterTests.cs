using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class TraitModifierGroupConverterTests
{
    [Fact]
    public void TraitModifierGroup_DeserializeCorectly()
    {
        const string json = @"
            {
				""id"": ""a3aae46a-4033-47e9-8212-84f46bf2a404"",
                ""type"": ""modifier_container"",
                ""open"": true,
                ""children"": [
                {
                    ""id"": ""90ea2d51-49e6-484c-9cb3-461df7b7bd54"",
                    ""type"": ""modifier"",
                    ""name"": ""Appears quite rarely (6-)"",
                    ""reference"": ""B36"",
                    ""cost"": 0.5,
                    ""cost_type"": ""multiplier"",
                    ""disabled"": true
                },
                {
                    ""id"": ""be41be9a-9e87-47cf-bff3-755e3635a670"",
                    ""type"": ""modifier"",
                    ""name"": ""Appears fairly often (9-)"",
                    ""reference"": ""B36"",
                    ""cost"": 1,
                    ""cost_type"": ""multiplier"",
                    ""disabled"": true
                },
                {
                    ""id"": ""558c0415-bdd8-4a47-bf06-09fd5e1d7e80"",
                    ""type"": ""modifier"",
                    ""name"": ""Appears quite often (12-)"",
                    ""reference"": ""B36"",
                    ""cost"": 2,
                    ""cost_type"": ""multiplier"",
                    ""disabled"": true
                },
                {
                    ""id"": ""1f0b6315-dff4-473e-bdef-19346d578523"",
                    ""type"": ""modifier"",
                    ""name"": ""Appears almost all the time (15-)"",
                    ""reference"": ""B36"",
                    ""cost"": 3,
                    ""cost_type"": ""multiplier"",
                    ""disabled"": true
                },
                {
                    ""id"": ""d2c39c4d-c98d-4266-8b8c-dcd6383e3a0a"",
                    ""type"": ""modifier"",
                    ""name"": ""Appears constantly"",
                    ""reference"": ""B36"",
                    ""cost"": 4,
                    ""cost_type"": ""multiplier"",
                    ""disabled"": true
                }
                ],
                ""name"": ""Frequency of Appearance"",
                ""reference"": ""B37""
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var group = JsonConverterWrapper.Deserialize<TraitModifierGroup>(json, jsonSettings)!;

        group.Id.Should().Be("a3aae46a-4033-47e9-8212-84f46bf2a404");
        group.Name.Should().Be("Frequency of Appearance");
        group.BookReferences.Count.Should().Be(1);
        group.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        group.BookReferences[0].PageNumber.Should().Be(37);

        group.Modifiers.Count.Should().Be(5);

        group.Modifiers[0].Id.Should().Be("90ea2d51-49e6-484c-9cb3-461df7b7bd54");
        group.Modifiers[0].Name.Should().Be("Appears quite rarely (6-)");
        group.Modifiers[0].CostType.Should().Be(TraitModifier.CostTypeEnum.Multiplier);
        group.Modifiers[0].PointsCost.Should().Be(0.5);
    }
}