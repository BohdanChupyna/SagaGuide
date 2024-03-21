using System.Collections.Generic;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class TraitConverterTests
{
    [Fact]
    public void Trait_AllPropertiesAreDeserialized()
    {
        const string json = @"
            {
               ""id"": ""458f7edb-7179-4c14-8a5c-dcd28499a0e2"",
                ""type"": ""trait"",
                ""name"": ""Neutralize (Psi)"",
                ""notes"": ""local custom note"",
                ""round_down"": true,
                ""cr_adj"": ""action_penalty"",
                ""cr"": 12,
                ""reference"": ""B71"",
                ""base_points"": 50,
                ""points_per_level"": 1,
                ""can_level"": true,
                ""tags"": [
                    ""Advantage"",
                    ""Exotic"",
                    ""Mental""
                ],
                ""modifiers"": [
					{
						""id"": ""689f0318-5bb4-45b8-bc81-74c14097fe68"",
						""type"": ""modifier"",
						""name"": ""Favor"",
						""reference"": ""B55"",
						""cost"": 0.2,
						""cost_type"": ""multiplier"",
						""disabled"": true
					},
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
					},
					{
						""id"": ""aa16b468-bd6e-42aa-b471-8656922c6757"",
						""type"": ""modifier_container"",
						""open"": true,
						""children"": [
							{
								""id"": ""9b920170-2506-4d12-b871-be1100043704"",
								""type"": ""modifier"",
								""name"": ""Group of 6-10"",
								""reference"": ""B37"",
								""cost"": 6,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""d0e44bc7-5889-46ec-879f-3d9ba9a4e54f"",
								""type"": ""modifier"",
								""name"": ""Group of 11-20"",
								""reference"": ""B37"",
								""cost"": 8,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""8fc8e24d-4b27-4e45-9b3a-5a6f64ada309"",
								""type"": ""modifier"",
								""name"": ""Group of 21-50"",
								""reference"": ""B37"",
								""cost"": 10,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""1dedde55-e356-448c-961a-53dc73e8e58b"",
								""type"": ""modifier"",
								""name"": ""Group of 51-100"",
								""reference"": ""B37"",
								""cost"": 12,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""a6fa2681-e601-4915-9e43-e357c1119e73"",
								""type"": ""modifier"",
								""name"": ""Group of 101-1000"",
								""reference"": ""B37"",
								""cost"": 18,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""d176a29c-87f6-41b9-8d4e-10beb54d1822"",
								""type"": ""modifier"",
								""name"": ""Group of 1001-10000"",
								""reference"": ""B37"",
								""cost"": 24,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""8584249f-64fa-4939-87eb-63382071fbf3"",
								""type"": ""modifier"",
								""name"": ""Group of 10001-100000"",
								""reference"": ""B37"",
								""cost"": 30,
								""cost_type"": ""multiplier"",
								""disabled"": true
							},
							{
								""id"": ""7429f8b9-70f3-4fe7-8c48-a9d1b9c06e0a"",
								""type"": ""modifier"",
								""name"": ""Group of 100001-1000000"",
								""reference"": ""B37"",
								""cost"": 36,
								""cost_type"": ""multiplier"",
								""disabled"": true
							}
						],
						""name"": ""Ally Group"",
						""reference"": ""B37""
					},
					{
						""id"": ""1423dd75-af66-4dfb-bf98-39ebd775beb3"",
						""type"": ""modifier"",
						""name"": ""Minion"",
						""reference"": ""B38"",
						""cost"": 50,
						""disabled"": true
					},
					{
						""id"": ""6dc2adbd-08c7-4ef0-8614-22ec36071dbb"",
						""type"": ""modifier"",
						""name"": ""Minion"",
						""reference"": ""B38"",
						""notes"": ""IQ 0 or Slave Mentality"",
						""disabled"": true
					},
					{
						""id"": ""af970aba-632e-444c-abbe-ea978ae30f1f"",
						""type"": ""modifier_container"",
						""open"": true,
						""children"": [
							{
								""id"": ""c1eb750d-bdd1-4483-a293-892526735fad"",
								""type"": ""modifier"",
								""name"": ""25% of your starting points"",
								""reference"": ""B37"",
								""cost"": 1,
								""cost_type"": ""points"",
								""disabled"": true
							},
							{
								""id"": ""0b93dc24-8f30-4c58-8595-a5f2bd05173d"",
								""type"": ""modifier"",
								""name"": ""50% of your starting points"",
								""reference"": ""B37"",
								""cost"": 2,
								""cost_type"": ""points"",
								""disabled"": true
							},
							{
								""id"": ""469b4a79-c6cf-4f87-be4d-c39428a08d9b"",
								""type"": ""modifier"",
								""name"": ""75% of your starting points"",
								""reference"": ""B37"",
								""cost"": 3,
								""cost_type"": ""points"",
								""disabled"": true
							},
							{
								""id"": ""1c5fa2a8-39df-4651-a06b-21635ead97ca"",
								""type"": ""modifier"",
								""name"": ""100% of your starting points"",
								""reference"": ""B37"",
								""cost"": 5,
								""cost_type"": ""points"",
								""disabled"": true
							},
							{
								""id"": ""b9452cfa-cda2-4ce0-8531-7a6b9c33ddb9"",
								""type"": ""modifier"",
								""name"": ""150% of your starting points"",
								""reference"": ""B37"",
								""cost"": 10,
								""cost_type"": ""points"",
								""disabled"": true
							}
						],
						""name"": ""Point Total"",
						""reference"": ""B37""
					},
					{
						""id"": ""d8886838-7444-495b-aa11-02ac3ae23dd2"",
						""type"": ""modifier"",
						""name"": ""Special Abilities"",
						""reference"": ""B38"",
						""notes"": ""@Abilities@"",
						""cost"": 50,
						""disabled"": true
					},
					{
						""id"": ""c432c70e-76f8-4215-9b0e-1e1297817f73"",
						""type"": ""modifier_container"",
						""open"": true,
						""children"": [
							{
								""id"": ""d00c3a09-82c4-4569-81df-955e8e62ef34"",
								""type"": ""modifier"",
								""name"": ""Summonable"",
								""reference"": ""B38,P41"",
								""cost"": 100,
								""disabled"": true
							},
							{
								""id"": ""564697bc-4d59-487f-be52-9cd695d74145"",
								""type"": ""modifier"",
								""name"": ""Conjured"",
								""reference"": ""DF9:4,P41"",
								""cost"": 100,
								""disabled"": true
							},
							{
								""id"": ""2b7c2195-de87-4b42-b5b1-7adc5c64dc24"",
								""type"": ""modifier"",
								""name"": ""Harder Summoning 1"",
								""reference"": ""DF9:4"",
								""cost"": -5,
								""disabled"": true
							},
							{
								""id"": ""356fa9ea-c5e9-40c5-b3f2-a7f7ab5d3f3a"",
								""type"": ""modifier"",
								""name"": ""Harder Summoning 2"",
								""reference"": ""DF9:4"",
								""cost"": -10,
								""disabled"": true
							},
							{
								""id"": ""c405a9b9-2959-4866-8974-43ef5373bbe9"",
								""type"": ""modifier"",
								""name"": ""Harder Summoning 3"",
								""reference"": ""DF9:4"",
								""cost"": -20,
								""disabled"": true
							}
						],
						""name"": ""Summonable""
					},
					{
						""id"": ""dd121846-2ba4-46bb-83fc-d881218cffca"",
						""type"": ""modifier_container"",
						""open"": true,
						""children"": [
							{
								""id"": ""f3001c31-1cbb-4f95-af86-c92a7755a044"",
								""type"": ""modifier"",
								""name"": ""Sympathy"",
								""reference"": ""B38"",
								""notes"": ""Death of one party kills the other"",
								""cost"": -50,
								""disabled"": true
							},
							{
								""id"": ""30df2a61-903a-4f3f-b7ce-6319dcc61e6a"",
								""type"": ""modifier"",
								""name"": ""Sympathy"",
								""reference"": ""B38"",
								""notes"": ""Death of one party kills the other and wounds affect ally but not you"",
								""cost"": -10,
								""disabled"": true
							},
							{
								""id"": ""d3be4428-f357-41d5-8bce-142240934518"",
								""type"": ""modifier"",
								""name"": ""Sympathy"",
								""reference"": ""B38"",
								""notes"": ""Death of one party reduces the other to 0 HP"",
								""cost"": -25,
								""disabled"": true
							},
							{
								""id"": ""036a0c85-d7af-4de9-af22-eea6cec5ed47"",
								""type"": ""modifier"",
								""name"": ""Sympathy"",
								""reference"": ""B38"",
								""notes"": ""Death of one party reduces the other to 0 HP and wounds affect ally but not you"",
								""cost"": -5,
								""disabled"": true
							}
						],
						""name"": ""Sympathy"",
						""reference"": ""B38""
					},
					{
						""id"": ""2777d1e3-2b29-4c22-b543-d2553828e7e9"",
						""type"": ""modifier"",
						""name"": ""Unwilling"",
						""reference"": ""B38"",
						""cost"": -50,
						""disabled"": true
					}
				],
                ""features"": [
                    {
                    ""type"": ""conditional_modifier"",
                    ""situation"": ""to all HT rolls concerned with eye damage"",
                    ""amount"": 1,
                    ""per_level"": true
                    },
                    {
                    ""type"": ""dr_bonus"",
                    ""location"": ""eye"",
                    ""amount"": 1,
                    ""per_level"": true
                    }
                ],
            }";

        var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
        var trait = JsonConverterWrapper.Deserialize<Trait>(json, jsonSettings)!;

        trait.Id.Should().Be("458f7edb-7179-4c14-8a5c-dcd28499a0e2");
        trait.Name.Should().Be("Neutralize (Psi)");
        trait.LocalNotes!.Should().Be("local custom note");
        trait.RoundCostDown.Should().BeTrue();
        trait.SelfControlRoll.Should().Be(12);
        trait.SelfControlRollAdjustment.Should().Be(Trait.SelfControlRollAdjustmentEnum.ActionPenalty);
        trait.BasePointsCost.Should().Be(50);
        trait.PointsCostPerLevel.Should().Be(1);
        trait.CanLevel.Should().BeTrue();
        
        trait.BookReferences.Count.Should().Be(1);
        
        trait.Tags.Count.Should().Be(3);
        trait.Tags[0].Should().Be("Advantage");
        trait.Tags[1].Should().Be("Exotic");
        trait.Tags[2].Should().Be("Mental");

        trait.Modifiers.Count.Should().Be(5);
        trait.ModifierGroups.Count.Should().Be(5);
        trait.ModifierGroups[0].Modifiers.Count.Should().Be(5);
        trait.Features.Count.Should().Be(2);
    }

    [Fact]
    public void Trait_CanParseList()
    {
	    const string json = @"[{
		""id"": ""003f1809-fb3f-48c6-b962-2c8177457f20"",
		""type"": ""trait"",
		""name"": ""Amphibious"",
		""reference"": ""B40,P42"",
		""tags"": [
			""Advantage"",
			""Exotic"",
			""Physical""
		],
		""base_points"": 10
	},
	{
		""id"": ""aa524785-968c-43c4-bd1a-94c1d2263b35"",
		""type"": ""trait"",
		""name"": ""360° Vision"",
		""reference"": ""B34,P39"",
		""tags"": [
			""Advantage"",
			""Exotic"",
			""Physical""
		],
		""modifiers"": [{
				""id"": ""49d8792f-35b1-4cf6-ba95-071c87b0bae6"",
				""type"": ""modifier"",
				""name"": ""Easy to hit "",
				""reference"": ""B34"",
				""notes"": ""Others can target your eyes at -6"",
				""cost"": -20,
				""disabled"": true
			},
			{
				""id"": ""f7d427d0-1509-4a07-8896-bf7f40bbc808"",
				""type"": ""modifier"",
				""name"": ""Panoptic 1"",
				""reference"": ""P39"",
				""cost"": 20,
				""disabled"": true
			},
			{
				""id"": ""5002586b-ad58-459d-b542-bc5ba3766505"",
				""type"": ""modifier"",
				""name"": ""Panoptic 2"",
				""reference"": ""P39"",
				""cost"": 60,
				""disabled"": true
			}
		],
		""base_points"": 25
	},
	{
		""id"": ""4c6e2770-48ae-433a-bdef-bfeb0ecabf98"",
		""type"": ""trait"",
		""name"": ""Absolute Timing"",
		""reference"": ""B35"",
		""tags"": [
			""Advantage"",
			""Mental""
		],
		""modifiers"": [{
			""id"": ""e1830b10-082b-4286-aa34-c825edbdf81a"",
			""type"": ""modifier"",
			""name"": ""Chronolocation"",
			""cost"": 3,
			""cost_type"": ""points"",
			""disabled"": true
		}],
		""base_points"": 2
	}, {
		""id"": ""b074474a-27f9-4d63-8f35-3be884ae5343"",
		""type"": ""trait"",
		""name"": ""Absolute Direction"",
		""reference"": ""B34"",
		""tags"": [
			""Advantage"",
			""Mental"",
			""Physical""
		],
		""modifiers"": [{
				""id"": ""940c9da3-6966-4ea6-9974-517614d0606b"",
				""type"": ""modifier"",
				""name"": ""Requires signal"",
				""reference"": ""B34"",
				""cost"": -20,
				""disabled"": true
			},
			{
				""id"": ""12730389-6652-4df8-8b34-ad078b76e408"",
				""type"": ""modifier"",
				""name"": ""3D Spatial Sense"",
				""reference"": ""B34"",
				""cost"": 5,
				""cost_type"": ""points"",
				""disabled"": true,
				""features"": [{
						""type"": ""skill_bonus"",
						""selection_type"": ""skills_with_name"",
						""name"": {
							""compare"": ""starts_with"",
							""qualifier"": ""piloting""
						},
						""amount"": 1
					},
					{
						""type"": ""skill_bonus"",
						""selection_type"": ""skills_with_name"",
						""name"": {
							""compare"": ""is"",
							""qualifier"": ""aerobatics""
						},
						""amount"": 2
					},
					{
						""type"": ""skill_bonus"",
						""selection_type"": ""skills_with_name"",
						""name"": {
							""compare"": ""is"",
							""qualifier"": ""free fall""
						},
						""amount"": 2
					},
					{
						""type"": ""skill_bonus"",
						""selection_type"": ""skills_with_name"",
						""name"": {
							""compare"": ""is"",
							""qualifier"": ""navigation""
						},
						""specialization"": {
							""compare"": ""is"",
							""qualifier"": ""hyperspace""
						},
						""amount"": 2
					},
					{
						""type"": ""skill_bonus"",
						""selection_type"": ""skills_with_name"",
						""name"": {
							""compare"": ""is"",
							""qualifier"": ""navigation""
						},
						""specialization"": {
							""compare"": ""is"",
							""qualifier"": ""space""
						},
						""amount"": 2
					}
				]
			}
		],
		""base_points"": 5,
		""features"": [{
				""type"": ""skill_bonus"",
				""selection_type"": ""skills_with_name"",
				""name"": {
					""compare"": ""is"",
					""qualifier"": ""body sense""
				},
				""amount"": 3
			},
			{
				""type"": ""skill_bonus"",
				""selection_type"": ""skills_with_name"",
				""name"": {
					""compare"": ""is"",
					""qualifier"": ""navigation""
				},
				""specialization"": {
					""compare"": ""is"",
					""qualifier"": ""air""
				},
				""amount"": 3
			},
			{
				""type"": ""skill_bonus"",
				""selection_type"": ""skills_with_name"",
				""name"": {
					""compare"": ""is"",
					""qualifier"": ""navigation""
				},
				""specialization"": {
					""compare"": ""is"",
					""qualifier"": ""land""
				},
				""amount"": 3
			},
			{
				""type"": ""skill_bonus"",
				""selection_type"": ""skills_with_name"",
				""name"": {
					""compare"": ""is"",
					""qualifier"": ""navigation""
				},
				""specialization"": {
					""compare"": ""is"",
					""qualifier"": ""sea""
				},
				""amount"": 3
			}
		]
	}
]";
	    
	    var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
	    var traits = JsonConverterWrapper.Deserialize<List<Trait>>(json, jsonSettings)!;

	    traits.Count.Should().Be(4);
    }
}