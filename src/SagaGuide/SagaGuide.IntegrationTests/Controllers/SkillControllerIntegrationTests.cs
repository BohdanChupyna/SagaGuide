using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Skill;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using FluentAssertions;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class SkillControllerIntegrationTests : TestBase
{
    public SkillControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
    }
    
    [Fact]
    public async Task GetSkill_ReturnCorrectSkillWithPrerequisites()
    {
        var skillVm = await ClientGetAsync<SkillViewModel>($"{RouteTemplates.DbApi}/skills/{SkillIds.Surgery}");
        
        skillVm.Should().NotBeNull();
        skillVm.Id.Should().Be(SkillIds.Surgery);
        skillVm.Name.Should().Be("Surgery");
        skillVm.Prerequisites!.Prerequisites.Count.Should().Be(2);

        ((SkillPrerequisite)skillVm.Prerequisites.Prerequisites[0]).NameCriteria.Qualifier.Should().Be("first aid");
        ((SkillPrerequisite)skillVm.Prerequisites.Prerequisites[1]).NameCriteria.Qualifier.Should().Be("physician");
    }
    
    [Fact]
    public async Task GetSkills_ReturnCorrectSkillsWithPrerequisites()
    {
        var skillVm = (await ClientGetAsync<IEnumerable<SkillViewModel>>($"{RouteTemplates.DbApi}/skills?ids={SkillIds.Surgery}&ids={SkillIds.Swimming}")).ToArray();
        skillVm.Should().NotBeNull();
        skillVm.Length.Should().Be(2);
        
        var surgery = skillVm.Single(x => x.Id == SkillIds.Surgery);
        surgery.Should().NotBeNull();
        surgery.Id.Should().Be(SkillIds.Surgery);
        surgery.Name.Should().Be("Surgery");
        surgery.Prerequisites!.Prerequisites.Count.Should().Be(2);
        
        surgery.Prerequisites.Prerequisites[0].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Skill);
        var skill = (SkillPrerequisite)surgery.Prerequisites.Prerequisites[0];
        skill.ShouldBe.Should().BeTrue();
        skill.NameCriteria.Qualifier.Should().Be("first aid");
        skill.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        
        surgery.Prerequisites.Prerequisites[1].PrerequisiteType.Should().Be(IPrerequisite.PrerequisiteTypeEnum.Skill);
        var trait = (SkillPrerequisite)surgery.Prerequisites.Prerequisites[1];
        trait.ShouldBe.Should().BeTrue();
        trait.NameCriteria.Qualifier.Should().Be("physician");
        trait.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        
        var swimming = skillVm.Single(x => x.Id == SkillIds.Swimming);
        swimming.Should().NotBeNull();
        swimming.Id.Should().Be(SkillIds.Swimming);
        swimming.Name.Should().Be("Swimming");
        swimming.Prerequisites.Should().BeNull();
    }
    
    [Fact]
    public async Task GetSkillsPage_ReturnPageOfSkillsWithPrerequisites()
    {
        var page = await ClientGetAsync<PaginatedItemsViewModel<SkillViewModel>>($"{RouteTemplates.DbApi}/skills/page?pageIndex=0&pageSize=2");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(0);
        page.PageSize.Should().Be(2);
        page.Count.Should().Be(21);
        
        var skillVm = page.Data.ToArray();
        skillVm.Should().NotBeNull();
        skillVm.Length.Should().Be(2);
        skillVm.Should().ContainSingle(x => x.Name == "Accounting");
        skillVm.Should().ContainSingle(x => x.Name == "Acting");
       
        page = await ClientGetAsync<PaginatedItemsViewModel<SkillViewModel>>($"{RouteTemplates.DbApi}/skills/page?pageIndex=1&pageSize=2");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(1);
        page.PageSize.Should().Be(2);
        page.Count.Should().Be(21);
        
        skillVm = page.Data.ToArray();
        skillVm.Should().NotBeNull();
        skillVm.Length.Should().Be(2);
        skillVm.Should().ContainSingle(x => x.Name == "Administration");
        skillVm.Should().ContainSingle(x => x.Name == "Alchemy");
    }

    // [Fact]
    // public async Task GetSkill_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/skills/{SkillIds.Surgery}", HttpStatusCode.Unauthorized);
    // }
    //
    // [Fact]
    // public async Task GetSkills_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/skills", HttpStatusCode.Unauthorized);
    // }
    //
    // [Fact]
    // public async Task GetSkillsPage_WithoutJwtToken_ReturnsUnauthorized()
    // {
    //     ServerFixture.RemoveToken();
    //     await ClientGetAsync($"{RouteTemplates.DbApi}/skills/page?pageIndex=0&pageSize=2", HttpStatusCode.Unauthorized);
    // }
}