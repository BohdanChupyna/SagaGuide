using System;
using System.Collections.Generic;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Infrastructure.JsonConverters;
using FluentAssertions;
using Xunit;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class EquipmentConverterTests
{
    [Fact]
    public void EquipmentConverter_EquipmentWithModifiers_SuccessfullyParsed()
    {
	    const string json = @"{
			""id"": ""f6ab89f7-d767-4f93-8d72-e05126188a25"",
		    ""type"": ""equipment"",
		    ""description"": ""Brigandine Helmet"",
		    ""reference"": ""DFA109"",
		    ""tags"": [
			    ""Armor"",
			    ""Headgear""
			],
		    ""modifiers"": [
				{
				    ""id"": ""79652977-9254-4b5f-ba41-d28c7044069e"",
				    ""type"": ""eqp_modifier"",
				    ""name"": ""Full Face Coverage"",
				    ""reference"": ""DFA108"",
				    ""notes"": ""Can't see your side hexes, even if you have Peripheral Vision"",
				    ""disabled"": true,
				    ""cost"": ""+180"",
				    ""weight"": ""+2 lb"",
					""tech_level"": ""4"",
				    ""features"": [
					    {
						    ""type"": ""dr_bonus"",
						    ""location"": ""eye"",
						    ""amount"": 5
					    },
					    {
						    ""type"": ""dr_bonus"",
						    ""location"": ""face"",
						    ""amount"": 5
					    },
					    {
						    ""type"": ""attribute_bonus"",
						    ""attribute"": ""hearing"",
						    ""amount"": -4
					    }
				    ]
			    },
				{
				    ""id"": ""12342977-9254-4b5f-ba41-d28c7044069e"",
				    ""type"": ""eqp_modifier"",
				    ""name"": ""Full Eyes Coverage"",
				    ""reference"": ""DFA108"",
				    ""notes"": ""test notes"",
				    ""disabled"": true,
				    ""cost"": ""-20"",
					""cost_type"": ""to_final_base_cost"",
					""weight_type"": ""to_final_base_weight"",
				    ""weight"": ""-4 lb"",
				    ""features"": [
					    {
						    ""type"": ""dr_bonus"",
						    ""location"": ""eye"",
						    ""amount"": 10
					    },
				    ]
			    }
		    ],
		    ""quantity"": 1,
		    ""value"": 450,
		    ""weight"": ""5 lb"",
			""tech_level"": ""4"",
		    ""features"": [
			    {
				    ""type"": ""dr_bonus"",
				    ""location"": ""skull"",
				    ""amount"": 5
			    },
				{
				    ""type"": ""dr_bonus"",
				    ""location"": ""face"",
				    ""amount"": 3
			    },
		    ],
	    }";
	    
	    var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
	    var equipment = JsonConverterWrapper.Deserialize<Equipment>(json, jsonSettings)!;

	    equipment.Name.Should().Be("Brigandine Helmet");
	    equipment.Id.Should().Be(Guid.Parse("f6ab89f7-d767-4f93-8d72-e05126188a25"));
	    
	    equipment.BookReferences.Count.Should().Be(1);
	    equipment.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.DungeonFantasyRpg);
	    equipment.BookReferences[0].PageNumber.Should().Be(109);

	    equipment.Tags.Count.Should().Be(2);
	    equipment.Tags[0].Should().Be("Armor");
	    equipment.Tags[1].Should().Be("Headgear");

	    equipment.Cost.Should().Be(450);
	    equipment.Weight.Should().Be("5 lb");
	    equipment.TechLevel.Should().Be("4");
	    equipment.Features.Count.Should().Be(2);
	    
	    
	    equipment.Modifiers.Count.Should().Be(2);
	    var modifier = equipment.Modifiers[0];
	    modifier.Id.Should().Be(Guid.Parse("79652977-9254-4b5f-ba41-d28c7044069e"));
	    modifier.Name.Should().Be("Full Face Coverage");
	    modifier.BookReferences.Count.Should().Be(1);
	    modifier.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.DungeonFantasyRpg);
	    modifier.BookReferences[0].PageNumber.Should().Be(108);
	    modifier.Notes.Should().Be("Can't see your side hexes, even if you have Peripheral Vision");
	    modifier.Cost.Should().Be("+180");
	    modifier.CostType.Should().Be(EquipmentModifier.CostTypeEnum.OriginalEquipmentModifier);
	    modifier.Weight.Should().Be("+2 lb");
	    modifier.WeightType.Should().Be(EquipmentModifier.WeightTypeEnum.OriginalEquipmentModifier);
	    modifier.Features.Count.Should().Be(3);
	    modifier.TechLevel.Should().Be("4");
	    
	    modifier = equipment.Modifiers[1];
	    modifier.Id.Should().Be(Guid.Parse("12342977-9254-4b5f-ba41-d28c7044069e"));
	    modifier.Name.Should().Be("Full Eyes Coverage");
	    modifier.BookReferences.Count.Should().Be(1);
	    modifier.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.DungeonFantasyRpg);
	    modifier.BookReferences[0].PageNumber.Should().Be(108);
	    modifier.Notes.Should().Be("test notes");
	    modifier.Cost.Should().Be("-20");
	    modifier.CostType.Should().Be(EquipmentModifier.CostTypeEnum.FinalBaseEquipmentModifier);
	    modifier.Weight.Should().Be("-4 lb");
	    modifier.WeightType.Should().Be(EquipmentModifier.WeightTypeEnum.FinalBaseEquipmentModifier);
	    modifier.Features.Count.Should().Be(1);
	    
    }
    
    [Fact]
    public void EquipmentConverter_Weapon_SuccessfullyParsed()
    {
	    const string json = @"{
			""id"": ""244ed05d-f5c4-452f-aded-88f092e6fa09"",
			""type"": ""equipment"",
			""description"": ""Throwing Axe"",
			""reference"": ""B271"",
			""tech_level"": ""0"",
			""tags"": [
				""Melee Weapon"",
				""Missile Weapon""
			],
			""quantity"": 1,
			""value"": 60,
			""weight"": ""4 lb"",
			""weapons"": [
				{
					""id"": ""8eaf7588-b9f4-4641-a762-9012cf3a039b"",
					""type"": ""melee_weapon"",
					""damage"": {
						""type"": ""cut"",
						""st"": ""sw"",
						""base"": ""2""
					},
					""strength"": ""11"",
					""usage"": ""Swung"",
					""reach"": ""1"",
					""parry"": ""0U"",
					""block"": ""No"",
					""defaults"": [
						{
							""type"": ""dx"",
							""modifier"": -5
						},
						{
							""type"": ""skill"",
							""name"": ""Axe/Mace""
						},
						{
							""type"": ""skill"",
							""name"": ""Flail"",
							""modifier"": -4
						},
						{
							""type"": ""skill"",
							""name"": ""Two-Handed Axe/Mace"",
							""modifier"": -3
						}
					]
				},
				{
					""id"": ""5d2a970a-01a3-4ef9-9090-3e81835b4e95"",
					""type"": ""ranged_weapon"",
					""damage"": {
						""type"": ""cut"",
						""st"": ""sw"",
						""base"": ""2""
					},
					""strength"": ""11"",
					""usage"": ""Thrown"",
					""accuracy"": ""2"",
					""range"": ""x1/x1.5"",
					""rate_of_fire"": ""1"",
					""shots"": ""T(1)"",
					""bulk"": ""-3"",
					""recoil"": ""1"",
					""defaults"": [
						{
							""type"": ""dx"",
							""modifier"": -4
						},
						{
							""type"": ""skill"",
							""name"": ""Thrown Weapon"",
							""specialization"": ""Axe/Mace""
						}
					]
				}
			],
	    }";
	    
	    var jsonSettings = JsonSettingsWrapper.CreateGcsMasterLibrarySettings();
	    var equipment = JsonConverterWrapper.Deserialize<Equipment>(json, jsonSettings)!;

	    equipment.Name.Should().Be("Throwing Axe");
	    equipment.Id.Should().Be(Guid.Parse("244ed05d-f5c4-452f-aded-88f092e6fa09"));
	    equipment.Tags.Count.Should().Be(2);
	    
	    equipment.Attacks.Count.Should().Be(2);
	    var meleeAttack = (MeleeAttack)equipment.Attacks[0];
	    meleeAttack.Id.Should().Be(Guid.Parse("8eaf7588-b9f4-4641-a762-9012cf3a039b"));
	    meleeAttack.Damage.AttackType.Should().Be("sw");
	    meleeAttack.Damage.DamageType.Should().Be("cut");
	    meleeAttack.Damage.BaseDamage.Should().Be("2");
	    meleeAttack.MinimumStrength.Should().Be("11");
	    meleeAttack.Usage.Should().Be("Swung");
	    meleeAttack.Reach.Should().Be("1");
	    meleeAttack.Parry.Should().Be("0U");
	    meleeAttack.Block.Should().Be("No");
	    meleeAttack.Defaults.Count.Should().Be(4);

	    var rangeAttack = (RangedAttack)equipment.Attacks[1];
	    rangeAttack.Id.Should().Be(Guid.Parse("5d2a970a-01a3-4ef9-9090-3e81835b4e95"));
	    rangeAttack.Damage.AttackType.Should().Be("sw");
	    rangeAttack.Damage.DamageType.Should().Be("cut");
	    rangeAttack.Damage.BaseDamage.Should().Be("2");
	    rangeAttack.MinimumStrength.Should().Be("11");
	    rangeAttack.Usage.Should().Be("Thrown");
	    rangeAttack.Accuracy.Should().Be("2");
	    rangeAttack.Range.Should().Be("x1/x1.5");
	    rangeAttack.RateOfFire.Should().Be("1");
	    rangeAttack.Shots.Should().Be("T(1)");
	    rangeAttack.Bulk.Should().Be("-3");
	    rangeAttack.Recoil.Should().Be("1");
	    rangeAttack.Defaults.Count.Should().Be(2);
    }
}