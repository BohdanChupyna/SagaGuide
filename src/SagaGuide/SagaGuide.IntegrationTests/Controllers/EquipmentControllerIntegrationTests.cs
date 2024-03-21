using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract.Equipment;
using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using FluentAssertions;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class EquipmentControllerIntegrationTests : TestBase
{
    public EquipmentControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
    }
    
    [Fact]
    public async Task GetEquipment_ReturnCorrectEquipment()
    {
        var equipmentVm = await ClientGetAsync<EquipmentViewModel>($"{RouteTemplates.DbApi}/equipments/{EquipmentIds.ThrowingAxe}");
        
        equipmentVm.Should().NotBeNull();
        equipmentVm.Id.Should().Be(EquipmentIds.ThrowingAxe);
        equipmentVm.Name.Should().Be("Throwing Axe");
        equipmentVm.Features.Count.Should().Be(0);
        equipmentVm.BookReferences.Count.Should().Be(1);
        equipmentVm.Cost.Should().Be(60);
        equipmentVm.Modifiers.Count.Should().Be(0);
        equipmentVm.Tags.Count.Should().Be(2);
        equipmentVm.TechLevel.Should().Be("0");
        equipmentVm.Weight.Should().Be("4 lb");
        equipmentVm.Attacks.Count.Should().Be(2);
        
        var melee = (MeleeAttack)equipmentVm.Attacks[0];
        melee.Id.Should().Be(Guid.Parse("8eaf7588-b9f4-4641-a762-9012cf3a039b"));
        melee.Damage.DamageType.Should().Be("cut");
        melee.Damage.AttackType.Should().Be("sw");
        melee.Damage.BaseDamage.Should().Be("2");
        melee.MinimumStrength.Should().Be("11");
        melee.Usage.Should().Be("Swung");
        melee.Reach.Should().Be("1");
        melee.Parry.Should().Be("0U");
        melee.Block.Should().Be("No");
        melee.Defaults.Count.Should().Be(4);
        melee.Defaults[0].AttributeType.Should().Be(Attribute.AttributeType.Dexterity);
        melee.Defaults[0].Modifier.Should().Be(-5);
        
        var range = (RangedAttack)equipmentVm.Attacks[1];
        range.Id.Should().Be(Guid.Parse("5d2a970a-01a3-4ef9-9090-3e81835b4e95"));
        range.Damage.DamageType.Should().Be("cut");
        range.Damage.AttackType.Should().Be("sw");
        range.Damage.BaseDamage.Should().Be("2");
        range.MinimumStrength.Should().Be("11");
        range.Usage.Should().Be("Thrown");
        
        range.Accuracy.Should().Be("2");
        range.Range.Should().Be("x1/x1.5");
        range.RateOfFire.Should().Be("1");
        range.Shots.Should().Be("T(1)");
        range.Bulk.Should().Be("-3");
        
        range.Defaults.Count.Should().Be(2);
        range.Defaults[1].Name.Should().Be("Thrown Weapon");
        range.Defaults[1].Specialization.Should().Be("Axe/Mace");
    }
    
    [Fact]
    public async Task GetEquipments_ReturnCorrectEquipments()
    {
        var equipmentVms = await ClientGetAsync<List<EquipmentViewModel>>($"{RouteTemplates.DbApi}/equipments?ids={EquipmentIds.ScaleArmor}&ids={EquipmentIds.Saw}");
        
        equipmentVms.Should().NotBeNull();
        equipmentVms.Count.Should().Be(2);
        
        equipmentVms[0].Id.Should().Be(EquipmentIds.Saw);
        equipmentVms[0].Name.Should().Be("SAW, 5.56mm");
        equipmentVms[0].Attacks.Count.Should().Be(1);
        equipmentVms[0].BookReferences.Count.Should().Be(1);
        equipmentVms[0].Cost.Should().Be(4800);
        equipmentVms[0].Features.Count.Should().Be(0);
        equipmentVms[0].LegalityClass.Should().Be("1");
        equipmentVms[0].Cost.Should().Be(4800);
        equipmentVms[0].Modifiers.Count.Should().Be(0);
        equipmentVms[0].Tags.Count.Should().Be(1);
        equipmentVms[0].TechLevel.Should().Be("7");
        equipmentVms[0].Weight.Should().Be("24 lb");
        
        equipmentVms[1].Id.Should().Be(EquipmentIds.ScaleArmor);
        equipmentVms[1].Name.Should().Be("Scale Armor");
        equipmentVms[1].Attacks.Count.Should().Be(0);
        equipmentVms[1].Features.Count.Should().Be(3);
        equipmentVms[1].BookReferences.Count.Should().Be(1);
        equipmentVms[1].Cost.Should().Be(420);
        equipmentVms[1].Modifiers.Count.Should().Be(0);
        equipmentVms[1].Tags.Count.Should().Be(1);
        equipmentVms[1].TechLevel.Should().Be("2");
        equipmentVms[1].Weight.Should().Be("35 lb");
    }
    
    // [Fact]
    // public async Task GetEquipments_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/equipments", HttpStatusCode.Unauthorized);
    // }
}