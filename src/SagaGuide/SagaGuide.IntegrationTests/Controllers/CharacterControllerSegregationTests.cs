using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SagaGuide.Api.Constants;
using SagaGuide.Api.Contract;
using SagaGuide.Api.Contract.Character;
using SagaGuide.Api.Contract.Trait;
using SagaGuide.Core;
using SagaGuide.Core.Domain.CharacterAggregate;
using SagaGuide.Core.Domain.TraitAggregate;
using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
using SagaGuide.TestData.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class CharacterControllerSegregationTests: TestBase
{
    public CharacterControllerSegregationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
        var token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserB))
            .Build();

        ServerFixture.SetToken(token);
    }
    
    [Fact]
    public async Task GetCharacter_WithoutJwtToken_ReturnsUnauthorized()
    {
        ServerFixture.RemoveToken();
        await ClientGetAsync($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}", HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task GetCharacter_OfAnotherUser_ReturnsNotFound()
    {
        var characterVm = await ClientGetAsync<NotFound>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}", HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetCharacters_ReturnOnlyUserCharacters()
    {
        var characters = (await ClientGetAsync<IEnumerable<CharacterViewModel>>($"{RouteTemplates.CharacterApi}")).ToArray();
        characters.Length.Should().Be(1);
       
        CharacterControllerIntegrationTests.CheckCharacter(characters[0], EfDataSeeder.TestCharacterGandalfId, "Gandalf", 2, 3, 0);
    }
    
    [Fact]
    public async Task GetCharactersInfo_ReturnOnlyUserCharactersInfo()
    {
        var characters = (await ClientGetAsync<IEnumerable<CharacterInfoViewModel>>($"{RouteTemplates.CharacterApi}/info")).ToArray();
        characters.Length.Should().Be(1);
        
        characters[0].Id.Should().Be(EfDataSeeder.TestCharacterGandalfId);
        characters[0].Name.Should().Be("Gandalf");
    }
    
    [Fact]
    public async Task GetCharactersPage_ReturnOnlyUserCharacters()
    {
        var page = await ClientGetAsync<PaginatedItemsViewModel<CharacterViewModel>>($"{RouteTemplates.CharacterApi}/page?pageIndex=0&pageSize=3");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(0);
        page.PageSize.Should().Be(3);
        page.Count.Should().Be(1);
        var character = page.Data.Single();
        CharacterControllerIntegrationTests.CheckCharacter(character, EfDataSeeder.TestCharacterGandalfId, "Gandalf", 2, 3, 0);
    }
    
    [Fact]
    public async Task PutCharacter_WithoutJwtToken_ReturnsUnauthorized()
    {
        ServerFixture.RemoveToken();
        await ClientPutAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}",
            new CharacterViewModel {Name = "", Id = Guid.NewGuid(), UserId = Guid.NewGuid()},
            HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task PutCharacter_NotUserCharacter_ReturnNotFound()
    {
        var token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserA))
            .Build();

        ServerFixture.SetToken(token);
        
        var userACharacterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        userACharacterVm.Name = "UpdatedFrodo";
        
        var traitsVm = (await ClientGetAsync<IEnumerable<TraitViewModel>>($"{RouteTemplates.DbApi}/traits")).ToArray();
        var disadvantage = traitsVm.First(t => t.Tags.Contains(Trait.TagsEnum.Disadvantage.ToString()));
        
        userACharacterVm.Traits.Add(new CharacterTraitViewModel
        {
            Id = Guid.NewGuid(),
            Trait = disadvantage,
            OptionalSpecialty = null,
            Level = 1,
            SelectedTraitModifiers = new List<CharacterTraitModifier>()
        });
        
        token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserB))
            .Build();

        ServerFixture.SetToken(token);
        
        var updatedVm = await ClientPutAsync<CharacterViewModel, string>($"{RouteTemplates.CharacterApi}",
            userACharacterVm, HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task DeleteCharacter_WithoutJwtToken_ReturnsUnauthorized()
    {
        ServerFixture.RemoveToken();
        await ClientDeleteAsync($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}", HttpStatusCode.Unauthorized);
    } 
    
    [Fact]
    public async Task DeleteCharacter_OfAnotherUser_ReturnsNotFound()
    {
        await ClientDeleteAsync($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}", HttpStatusCode.NotFound);
        
        var token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserA))
            .Build();
        ServerFixture.SetToken(token);
        
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        characterVm.Id.Should().Be(EfDataSeeder.TestCharacterFrodoId);
        characterVm.UserId.Should().Be(UserIds.UserA);
    } 
    
    [Fact]
    public async Task PostCharacter_WithoutJwtToken_ReturnsUnauthorized()
    {
        ServerFixture.RemoveToken();
        await ClientPostAsync($"{RouteTemplates.CharacterApi}", "", HttpStatusCode.Unauthorized);
    } 
}