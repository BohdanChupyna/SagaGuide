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
using SagaGuide.IntegrationTests.Setup;
using Xunit;
using Xunit.Abstractions;

namespace SagaGuide.IntegrationTests.Controllers;

[Collection($"{SharedTestCollection.CollectionName}")]
public class CharacterControllerIntegrationTests: TestBase
{
    public CharacterControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
        : base(serverFixture, testOutputHelper)
    {
        var token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserA))
            .Build();

        ServerFixture.SetToken(token);
    }

    [Fact]
    public async Task GetUnregisteredCharacter_ReturnUnregisteredCharacter()
    {
        ServerFixture.SetToken(string.Empty);
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/unregistered/", HttpStatusCode.Created);
        
        CheckCharacter(characterVm, characterVm.Id, "Unregistered Character", 0, 0, 0);
    }
    
    [Fact]
    public async Task GetCharacter_ReturnCorrectCharacterWithAllNestedProperties()
    {
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");

        CheckCharacter(characterVm, EfDataSeeder.TestCharacterFrodoId, "Frodo", 2, 2, 0);
    }
    
    // [Fact]
    // public async Task GetCharacter_Performance()
    // {
    //     var list2 = new List<TimeSpan>();
    //     for(var i = 0; i < 10000; ++i)
    //     {
    //         var start = DateTime.Now;
    //         var vm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
    //         var runTime = DateTime.Now.Subtract(start);
    //         list2.Add(runTime);
    //     }
    //
    //     var min2 = list2.Min();
    //     var max2 = list2.Max();
    //     var avarage2 = list2.Aggregate(TimeSpan.Zero, (sum, cur) => sum + cur).Milliseconds / (double)list2.Count;
    // }
    
    [Fact]
    public async Task GetCharacters_ReturnCorrectCharactersWithAllNestedProperties()
    {
        var characters = (await ClientGetAsync<IEnumerable<CharacterViewModel>>($"{RouteTemplates.CharacterApi}?ids={EfDataSeeder.TestCharacterFrodoId}&ids={EfDataSeeder.TestCharacterGorlumId}")).ToArray();
        
        CheckCharacter(characters[0], EfDataSeeder.TestCharacterFrodoId, "Frodo", 2, 2, 0);
        CheckCharacter(characters[1], EfDataSeeder.TestCharacterGorlumId, "Gorlum", 2, 3, 0);
    }
    
    [Fact]
    public async Task GetCharactersInfo_ReturnCharactersInfo()
    {
        var characters = (await ClientGetAsync<IEnumerable<CharacterInfoViewModel>>($"{RouteTemplates.CharacterApi}/info?ids={EfDataSeeder.TestCharacterFrodoId}&ids={EfDataSeeder.TestCharacterGorlumId}")).ToArray();

        characters[0].Id.Should().Be(EfDataSeeder.TestCharacterFrodoId);
        characters[0].Name.Should().Be("Frodo");
        characters[0].Campaign.Should().Be("MiddleEarth");
        characters[0].Player.Should().Be("Mike");
        characters[0].Title.Should().Be("Sneaky hobbit");
        
        characters[1].Id.Should().Be(EfDataSeeder.TestCharacterGorlumId);
        characters[1].Name.Should().Be("Gorlum");
        characters[1].Campaign.Should().Be("MiddleEarth");
        characters[1].Player.Should().Be("Pite");
        characters[1].Title.Should().Be("Fisher");
    }
    
    [Fact]
    public async Task GetCharactersPage_ReturnCorrectPageOfCharactersWithAllNestedProperties()
    {
        var page = await ClientGetAsync<PaginatedItemsViewModel<CharacterViewModel>>($"{RouteTemplates.CharacterApi}/page?pageIndex=0&pageSize=1");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(0);
        page.PageSize.Should().Be(1);
        page.Count.Should().Be(2);
        var character = page.Data.Single();
        CheckCharacter(character, EfDataSeeder.TestCharacterFrodoId, "Frodo", 2, 2, 0);
        
        page = await ClientGetAsync<PaginatedItemsViewModel<CharacterViewModel>>($"{RouteTemplates.CharacterApi}/page?pageIndex=1&pageSize=1");
        page.Should().NotBeNull();
        page.PageIndex.Should().Be(1);
        page.PageSize.Should().Be(1);
        page.Count.Should().Be(2);
        character = page.Data.Single();
        CheckCharacter(character, EfDataSeeder.TestCharacterGorlumId, "Gorlum", 2, 3, 0);
    }
    
    [Fact]
    public async Task PostCharacter_CreateCharacterWithAttributesAndCharacteristics()
    {
        var characterVm = await ClientPostEmptyBodyAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}", HttpStatusCode.Created);
        characterVm.Name.Should().Be("New Character");
        Guid.TryParse(characterVm.Id.ToString(), out _).Should().BeTrue();

        characterVm.Traits.Count.Should().Be(0);
        characterVm.Skills.Count.Should().Be(0);
        characterVm.Techniques.Count.Should().Be(0);
        
        CheckCharacterAttributes(characterVm);
        
        var characterGetVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{characterVm.Id}");
        characterGetVm.Name.Should().Be(characterVm.Name);
        characterGetVm.Traits.Count.Should().Be(0);
        characterGetVm.Skills.Count.Should().Be(0);
        characterGetVm.Techniques.Count.Should().Be(0);
        CheckCharacterAttributes(characterGetVm);
    } 
    
    [Fact]
    public async Task PostUnregisteredCharacter_ReturnRegisteredCharacter()
    {
        ServerFixture.SetToken(string.Empty);
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/unregistered/", HttpStatusCode.Created);
        CheckCharacter(characterVm, characterVm.Id, "Unregistered Character", 0, 0, 0);

        var token = new TokenBuilder()
            .WithScope(Authorizations.ReadCharacters)
            .WithScope(Authorizations.WriteCharacter)
            .WithScope(Authorizations.ReadGurpsRules)
            .WithClaim(ServerFixture.GetUserClaim(UserIds.UserA))
            .Build();
        ServerFixture.SetToken(token);
        
        var traitsVm = (await ClientGetAsync<IEnumerable<TraitViewModel>>($"{RouteTemplates.DbApi}/traits")).ToArray();
        var disadvantage = traitsVm.First(t => t.Tags.Contains(Trait.TagsEnum.Disadvantage.ToString()));
        
        characterVm.Traits.Add(new CharacterTraitViewModel
        {
            Id = Guid.NewGuid(),
            Trait = disadvantage,
            OptionalSpecialty = null,
            Level = 1,
            SelectedTraitModifiers = new List<CharacterTraitModifier>()
        });
        characterVm.Name = "My new character";

        var registeredCharacterVm = await ClientPostAsync<CharacterViewModel, CharacterViewModel>($"{RouteTemplates.CharacterApi}", characterVm, HttpStatusCode.Created);
        registeredCharacterVm.UserId.Should().Be(UserIds.UserA);
        registeredCharacterVm.UserId.Should().NotBe(characterVm.UserId);
        
        registeredCharacterVm.Id.Should().NotBe(characterVm.Id);
        CheckCharacter(registeredCharacterVm, registeredCharacterVm.Id, "My new character", 0, 1, 0);
    }
    
    [Fact]
    public async Task PutCharacter_ReturnUpdatedCharacter()
    {
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        CheckCharacter(characterVm, EfDataSeeder.TestCharacterFrodoId, "Frodo", 2, 2, 0);
        characterVm.Name = "UpdatedFrodo";
        
        var traitsVm = (await ClientGetAsync<IEnumerable<TraitViewModel>>($"{RouteTemplates.DbApi}/traits")).ToArray();
        var disadvantage = traitsVm.First(t => t.Tags.Contains(Trait.TagsEnum.Disadvantage.ToString()));
        
        characterVm.Traits.Add(new CharacterTraitViewModel
        {
            Id = Guid.NewGuid(),
            Trait = disadvantage,
            OptionalSpecialty = null,
            Level = 1,
            SelectedTraitModifiers = new List<CharacterTraitModifier>()
        });
        
        var updatedVm = await ClientPutAsync<CharacterViewModel, CharacterViewModel>($"{RouteTemplates.CharacterApi}",
            characterVm, HttpStatusCode.OK);

        updatedVm.Id.Should().Be(characterVm.Id);
        CheckCharacter(characterVm, EfDataSeeder.TestCharacterFrodoId, characterVm.Name, 2, 3, 0);
        updatedVm.ModifiedOn.Should().BeAfter(characterVm.ModifiedOn);
        updatedVm.Traits.Find(t => t.Trait.Id == disadvantage.Id).Should().NotBeNull();
        updatedVm.Name.Should().Be(characterVm.Name);
        updatedVm.Version.Should().BeGreaterThan(characterVm.Version);

        var characterVm2 = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        characterVm2.Traits.Find(t => t.Trait.Id == disadvantage.Id).Should().NotBeNull();
        characterVm2.Id.Should().Be(updatedVm.Id);
        characterVm2.Name.Should().Be(updatedVm.Name);
        characterVm2.ModifiedOn.Should().BeExactly(new TimeSpan(updatedVm.ModifiedOn.Ticks));
        characterVm2.Version.Should().Be(updatedVm.Version);
    }
    
    [Fact]
    public async Task PutCharacter_NotExistedCharacter_ReturnBadRequest()
    {
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        characterVm.Id = Guid.Parse("e6a62b06-a0a6-4cb7-a6a6-dcddebc2f0de");

        var commandResponse = await ClientPutAsync<CharacterViewModel, string>($"{RouteTemplates.CharacterApi}",
            characterVm, HttpStatusCode.NotFound);

        commandResponse.Should().Be("Character with id e6a62b06-a0a6-4cb7-a6a6-dcddebc2f0de does not exist.");
    }
    
    [Fact]
    public async Task PutCharacter_OutDatedCharacter_ReturnBadRequest()
    {
        var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
        CheckCharacter(characterVm, EfDataSeeder.TestCharacterFrodoId, "Frodo", 2, 2, 0);
        characterVm.Name = "UpdatedFrodo";
        
        var updatedVm = await ClientPutAsync<CharacterViewModel, CharacterViewModel>($"{RouteTemplates.CharacterApi}",
            characterVm, HttpStatusCode.OK);

        updatedVm.Name.Should().Be(characterVm.Name);
        updatedVm.ModifiedOn.Should().BeAfter(characterVm.ModifiedOn);
        updatedVm.Version.Should().BeGreaterThan(characterVm.Version);
        
        characterVm.Name = "OutdatedFrodo";
        var commandResponse = await ClientPutAsync<CharacterViewModel, CommandResponse<CharacterViewModel>>($"{RouteTemplates.CharacterApi}",
            characterVm, HttpStatusCode.BadRequest);
        
        commandResponse.Messages.Single().Message.Should().Be($"Character with id {EfDataSeeder.TestCharacterFrodoId} is outdated or deleted.");
    }
    
    [Fact]
    public async Task DeleteCharacter_WithValidId_DeleteCharacter()
    {
        var characterVm = await ClientPostEmptyBodyAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}", HttpStatusCode.Created);
        var characterVm2 = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{characterVm.Id}");
        characterVm2.Id.Should().Be(characterVm.Id);

        await ClientDeleteAsync($"{RouteTemplates.CharacterApi}/{characterVm.Id}", HttpStatusCode.NoContent);
        
        var result = await ServerFixture.Client.GetAsync($"{RouteTemplates.CharacterApi}/{characterVm.Id}");
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    } 
    
    [Fact]
    public async Task DeleteCharacter_WithInvalidId_Return404NotFound()
    {
        var error = await ClientDeleteAsync<string>($"{RouteTemplates.CharacterApi}/{Guid.NewGuid()}", HttpStatusCode.NotFound);
        error.Should().Be(ErrorMessages.CharacterNotFound);
    } 
    
    public static void CheckCharacter(CharacterViewModel character, Guid expectedId, string expectedName,
        int expectedSkillsCount, int expectedTraitsCount, int expectedEquipmentCount)
    {
        character.Should().NotBeNull();
        character.Id.Should().Be(expectedId);
        character.Name.Should().Be(expectedName);

        CheckCharacterAttributes(character);
        
        character.Skills.Should().NotBeNull();
        character.Skills.Count.Should().Be(expectedSkillsCount);
        if (character.Skills.Count > 0)
        {
            character.Skills.Should().AllSatisfy(x =>
            {
                x.Skill.Should().NotBeNull();
            });
        }
        
        
        character.Traits.Should().NotBeNull();
        character.Traits.Count.Should().Be(expectedTraitsCount);
        if (character.Traits.Count > 0)
        {
            character.Traits.Should().AllSatisfy(x =>
            {
                x.Trait.Should().NotBeNull();
            }); 
        }
        
        character.Equipments.Should().NotBeNull();
        character.Equipments.Count.Should().Be(expectedEquipmentCount);
        if (character.Equipments.Count > 0)
        {
            character.Equipments.Should().AllSatisfy(x =>
            {
                x.Equipment.Should().NotBeNull();
            }); 
        }
    }

    private static void CheckCharacterAttributes(CharacterViewModel character)
    {
        character.Attributes.Should().NotBeNull();
        character.Attributes.Count.Should().Be(10);
        character.Attributes.Should().AllSatisfy(x =>
        {
            x.Attribute.Should().NotBeNull();
        });
    }
}