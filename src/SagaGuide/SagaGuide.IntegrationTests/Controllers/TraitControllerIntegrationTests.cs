using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Trait;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Features;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using FluentAssertions;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class TraitControllerIntegrationTests : TestBase
{
    public TraitControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
    }
    
    [Fact]
    public async Task GetTrait_ReturnCorrectFeatureWithModifiers()
    {
        var traitViewModel = await ClientGetAsync<TraitViewModel>($"{RouteTemplates.DbApi}/traits/{TraitIds.Ally}");
        
        traitViewModel.Should().NotBeNull();
        traitViewModel.Id.Should().Be(TraitIds.Ally);
        traitViewModel.Name.Should().Be("Ally (@Who@)");
        traitViewModel.Modifiers.Count.Should().Be(5);
        traitViewModel.ModifierGroups.Count.Should().Be(5);
    }
    
    [Fact]
    public async Task GetTrait_ReturnCorrectTraitWithAllProperties()
    {
        var traitVm = await ClientGetAsync<TraitViewModel>($"{RouteTemplates.DbApi}/traits/{TraitIds.AbsoluteDirection}");
        
        traitVm.Should().NotBeNull();
        traitVm.Id.Should().Be(TraitIds.AbsoluteDirection);
        traitVm.Name.Should().Be("Absolute Direction");
        traitVm.BasePointsCost.Should().Be(5);
        traitVm.BookReferences.Count.Should().Be(1);
        traitVm.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        traitVm.BookReferences[0].PageNumber.Should().Be(34);

        traitVm.Tags.Count.Should().Be(3);
        traitVm.Tags[0].Should().Be("Advantage");
        traitVm.Tags[1].Should().Be("Mental");
        traitVm.Tags[2].Should().Be("Physical");
        
        traitVm.Modifiers.Count.Should().Be(2);
        var modifier = traitVm.Modifiers[0];
        modifier.Name.Should().Be("Requires signal");
        modifier.Id.Should().Be("940c9da3-6966-4ea6-9974-517614d0606b");
        modifier.BookReferences.Count.Should().Be(1);
        modifier.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        modifier.BookReferences[0].PageNumber.Should().Be(34);
        modifier.PointsCost.Should().Be(-20);
        modifier.CostType.Should().Be(TraitModifier.CostTypeEnum.Points);
        modifier.Features.Count.Should().Be(0);
        
        modifier = traitVm.Modifiers[1];
        modifier.Name.Should().Be("3D Spatial Sense");
        modifier.Id.Should().Be("12730389-6652-4df8-8b34-ad078b76e408");
        modifier.BookReferences.Count.Should().Be(1);
        modifier.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        modifier.BookReferences[0].PageNumber.Should().Be(34);
        modifier.PointsCost.Should().Be(5);
        modifier.CostType.Should().Be(TraitModifier.CostTypeEnum.Points);
        modifier.Features.Count.Should().Be(5);

        modifier.Features[0].FeatureType.Should().Be(IFeature.FeatureTypeEnum.SkillBonus);
        var skillBonusFeature = (SkillBonusFeature)modifier.Features[0];
        skillBonusFeature.SkillSelectionType.Should().Be(SkillBonusFeature.SkillSelectionTypeEnum.SkillsWithName);
        skillBonusFeature.NameCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.StartsWith);
        skillBonusFeature.NameCriteria.Qualifier.Should().Be("piloting");
        
        traitVm.Features.Count.Should().Be(4);
        traitVm.Features[1].FeatureType.Should().Be(IFeature.FeatureTypeEnum.SkillBonus);
        skillBonusFeature = (SkillBonusFeature)traitVm.Features[1];
        skillBonusFeature.SkillSelectionType.Should().Be(SkillBonusFeature.SkillSelectionTypeEnum.SkillsWithName);
        skillBonusFeature.NameCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillBonusFeature.NameCriteria.Qualifier.Should().Be("navigation");
        skillBonusFeature.SpecializationCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillBonusFeature.SpecializationCriteria.Qualifier.Should().Be("air");
    }
    
    [Fact]
    public async Task GetTraits_ReturnCorrectFeaturesWithModifiers()
    {
        var traitsVm = (await ClientGetAsync<IEnumerable<TraitViewModel>>($"{RouteTemplates.DbApi}/traits?ids={TraitIds.Ally}&ids={TraitIds.AbsoluteDirection}")).ToArray();
        traitsVm.Should().NotBeNull();
        traitsVm.Length.Should().Be(2);

        var ally = traitsVm.Single(x => x.Id == TraitIds.Ally);
        ally.Id.Should().Be(TraitIds.Ally);
        ally.Name.Should().Be("Ally (@Who@)");
        ally.Modifiers.Count.Should().Be(5);
        ally.ModifierGroups.Count.Should().Be(5);
        
        var absoluteDirection = traitsVm.Single(x => x.Id == TraitIds.AbsoluteDirection);
        absoluteDirection.Should().NotBeNull();
        absoluteDirection.Id.Should().Be(TraitIds.AbsoluteDirection);
        absoluteDirection.Name.Should().Be("Absolute Direction");
        absoluteDirection.Modifiers.Count.Should().Be(2);
        absoluteDirection.Features.Count.Should().Be(4);
    }
    
    [Fact]
    public async Task GetTraitsPage_ReturnPageOfFeaturesWithModifiers()
    {
        var page = await ClientGetAsync<PaginatedItemsViewModel<TraitViewModel>>($"{RouteTemplates.DbApi}/traits/page?pageIndex=0&pageSize=2");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(0);
        page.PageSize.Should().Be(2);
        page.Count.Should().Be(8);
        
        var featuresVm = page.Data.ToArray();
        featuresVm.Should().NotBeNull();
        featuresVm.Length.Should().Be(2);
        
        var vision = featuresVm.Single(x => x.Name == "360° Vision");
        vision.Modifiers.Count.Should().Be(3);
        vision.ModifierGroups.Count.Should().Be(0);
        
        var spatialSense = featuresVm.Single(x => x.Name == "Absent-Mindedness");
        spatialSense.Modifiers.Count.Should().Be(0);
        spatialSense.ModifierGroups.Count.Should().Be(0);
       
        page = await ClientGetAsync<PaginatedItemsViewModel<TraitViewModel>>($"{RouteTemplates.DbApi}/traits/page?pageIndex=1&pageSize=2");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(1);
        page.PageSize.Should().Be(2);
        page.Count.Should().Be(8);
        
        featuresVm = page.Data.ToArray();
        featuresVm.Should().NotBeNull();
        featuresVm.Length.Should().Be(2);
        
        var absoluteDirection = featuresVm.Single(x => x.Name == "Absolute Direction");
        absoluteDirection.Modifiers.Count.Should().Be(2);
        absoluteDirection.ModifierGroups.Count.Should().Be(0);
        
        var absoluteTiming = featuresVm.Single(x => x.Name == "Absolute Timing");
        absoluteTiming.Modifiers.Count.Should().Be(1);
        absoluteTiming.ModifierGroups.Count.Should().Be(0);
    }
    
    // [Fact]
    // public async Task GetTrait_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/traits/{TraitIds.AbsoluteDirection}", HttpStatusCode.Unauthorized);
    // }
    //
    // [Fact]
    // public async Task GetTraits_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/traits?ids={TraitIds.Ally}&ids={TraitIds.AbsoluteDirection}", HttpStatusCode.Unauthorized);
    // }
    //
    // [Fact]
    // public async Task GetTraitsPage_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/traits/page?pageIndex=0&pageSize=2", HttpStatusCode.Unauthorized);
    // }
}