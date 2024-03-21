// using System;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using SagaGuide.Api.Constants;
// using SagaGuide.Api.Contract.Character;
// using SagaGuide.Core;
// using SagaGuide.Infrastructure.EntityFramework.DataSeeders;
// using SagaGuide.IntegrationTests.Setup;
// using FluentAssertions;
// using Microsoft.AspNetCore.Mvc;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace SagaGuide.IntegrationTests.Controllers;
//
// [Collection($"{SharedTestCollection.CollectionName}")]
// public class CharacterTraitControllerIntegrationTests : TestBase
// {
//     public CharacterTraitControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
//         : base(serverFixture, testOutputHelper)
//     {
//     }
//
//     [Fact]
//     public async Task PostFeature_WithCorrectInput_CreateFeature()
//     {
//         var createVm = new AddCharacterTraitViewModel
//         {
//             FeatureId = TraitIds.Amphibious,
//             SpentPoints = 1,
//             OptionalSpecialty = null
//         };
//
//         var characterFeatureGuid = await ClientPostAsync<AddCharacterTraitViewModel, Guid>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/traits", createVm, HttpStatusCode.Created);
//         characterFeatureGuid.Should().NotBeEmpty();
//
//         var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//         var characterFeature = characterVm.Traits.First(s => s.Id == characterFeatureGuid);
//         characterFeature.Trait.Id.Should().Be(createVm.FeatureId);
//         characterFeature.Level.Should().Be(1);
//     }
//
//     [Fact]
//     public async Task PostFeature_CharacterDontExist_ReturnNotFound()
//     {
//         var createVm = new AddCharacterTraitViewModel
//         {
//             FeatureId = TraitIds.Amphibious,
//             SpentPoints = 1,
//             OptionalSpecialty = null
//         };
//
//         var response = await ClientPostAsync<AddCharacterTraitViewModel, string>($"{RouteTemplates.CharacterApi}/{Guid.NewGuid()}/traits", createVm, HttpStatusCode.NotFound);
//         response.Should().Be(ErrorMessages.CharacterNotFound);
//     }
//
//     [Fact]
//     public async Task PostFeature_FeatureDontExist_ReturnNotFound()
//     {
//         var createVm = new AddCharacterTraitViewModel
//         {
//             FeatureId = Guid.NewGuid(),
//             SpentPoints = 1,
//             OptionalSpecialty = null
//         };
//
//         var response = await ClientPostAsync<AddCharacterTraitViewModel, string>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/traits", createVm, HttpStatusCode.NotFound);
//         response.Should().Be(ErrorMessages.FeatureNotFound);
//     }
//
//     [Fact]
//     public async Task PostFeature_SpentPoints0_ReturnBadRequest()
//     {
//         var createVm = new AddCharacterTraitViewModel
//         {
//             FeatureId = TraitIds.Amphibious,
//             SpentPoints = 0,
//             OptionalSpecialty = null
//         };
//
//         var problemDetails = await ClientPostBadRequestAsync<AddCharacterTraitViewModel, ValidationProblemDetails>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/traits", createVm);
//         problemDetails.Should().NotBeNull();
//         problemDetails!.Errors.Count.Should().Be(1);
//         problemDetails.Errors.First().Key.Should().Be(nameof(AddCharacterTraitViewModel.SpentPoints));
//     }
//
//     [Fact]
//     public async Task DeleteFeature_ForExistFeature_DeleteFeature()
//     {
//         var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//         var feature = characterVm.Traits.First().Id;
//
//         await ClientDeleteAsync($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/traits/{feature}", HttpStatusCode.NoContent);
//
//         characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//         characterVm.Traits.FirstOrDefault(s => s.Id == feature).Should().BeNull();
//     }
//
//     [Fact]
//     public async Task DeleteFeature_ForNotExistedFeature_ReturnNotFound()
//     {
//         var error = await ClientDeleteAsync<string>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/traits/{Guid.NewGuid()}", HttpStatusCode.NotFound);
//         error.Should().Be(ErrorMessages.FeatureNotFound);
//     }
//
//     [Fact]
//     public async Task DeleteFeature_ForNotExistedCharacter_ReturnNotFound()
//     {
//         var error = await ClientDeleteAsync<string>($"{RouteTemplates.CharacterApi}/{Guid.NewGuid()}/traits/{TraitIds.Amphibious}", HttpStatusCode.NotFound);
//         error.Should().Be(ErrorMessages.CharacterNotFound);
//     }
// }
//
