using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract.Skill;
using SagaGuide.Api.Contract.Technique;
using SagaGuide.Core.Domain;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using FluentAssertions;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class TechniqueControllerIntegrationTests : TestBase
{
    public TechniqueControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
    }
    
    [Fact]
    public async Task GetTechniques_ReturnSortedTechniques()
    {
        var techniquesVm = (await ClientGetAsync<IEnumerable<TechniqueViewModel>>(RouteTemplates.TechniquesApi)).ToList();
        
        techniquesVm.Should().NotBeNull();
        techniquesVm.Count.Should().Be(4);
        techniquesVm[0].Name.Should().Be("Neck Snap");
        techniquesVm[1].Name.Should().Be("Off-Hand Weapon Training");
        techniquesVm[2].Name.Should().Be("Retain Weapon");
        techniquesVm[3].Name.Should().Be("Retain Weapon (@Ranged Weapon@)");
    }
    
    [Fact]
    public async Task GetTechniques_ReturnCorrectTechniquesByIds()
    {
        var techniquesVm = (await ClientGetAsync<IEnumerable<TechniqueViewModel>>($"{RouteTemplates.TechniquesApi}?ids={TechniqueIds.NeckSnapId}&ids={TechniqueIds.OffHandWeaponTrainingId}")).ToList();
        
        techniquesVm.Should().NotBeNull();
        techniquesVm.Count.Should().Be(2);

        var neckSnap = techniquesVm[0];
        neckSnap.Name.Should().Be("Neck Snap");
        
        neckSnap.Tags.Count.Should().Be(4);
        neckSnap.Tags[0].Should().Be("Combat");
        neckSnap.Tags[1].Should().Be("Melee Combat");
        neckSnap.Tags[2].Should().Be("Technique");
        neckSnap.Tags[3].Should().Be("Weapon");

        neckSnap.BookReferences.Count.Should().Be(2);
        neckSnap.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        neckSnap.BookReferences[0].PageNumber.Should().Be(232);
        neckSnap.BookReferences[0].MagazineNumber.Should().BeNull();
        neckSnap.BookReferences[1].SourceBook.Should().Be(BookReference.SourceBookEnum.MartialArts);
        neckSnap.BookReferences[1].PageNumber.Should().Be(77);
        neckSnap.BookReferences[1].MagazineNumber.Should().BeNull();

        neckSnap.DifficultyLevel.Should().Be(SkillViewModel.DifficultyLevelEnumViewModel.Hard);
        neckSnap.PointsCost.Should().Be(2);
        neckSnap.TechniqueLimitModifier.Should().Be(3);
        neckSnap.Default.AttributeType.Should().Be(Attribute.AttributeType.Strength);
        neckSnap.Default.Modifier.Should().Be(-4);

        techniquesVm[1].Name.Should().Be("Off-Hand Weapon Training");
    }
    
    // [Fact]
    // public async Task GetTechniques_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync(RouteTemplates.TechniquesApi,  HttpStatusCode.Unauthorized);
    // }
}