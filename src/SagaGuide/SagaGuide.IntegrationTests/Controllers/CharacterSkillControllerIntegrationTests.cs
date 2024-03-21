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
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Xunit;
// using Xunit.Abstractions;
//
// namespace SagaGuide.IntegrationTests.Controllers;
//
// [Collection($"{SharedTestCollection.CollectionName}")]
// public class CharacterSkillControllerIntegrationTests: TestBase
// {
//     public CharacterSkillControllerIntegrationTests(ServerFixture serverFixture, ITestOutputHelper testOutputHelper)
//         : base(serverFixture, testOutputHelper)
//     {
//     }
//     
//     // [Fact]
//     // public async Task PostSkill_WithUnsatisfiedPrerequisites_ReturnBadRequest()
//     // {
//     //     var createVm = new AddCharacterSkillViewModel
//     //     {
//     //         SkillId = SkillIds.Aquabatics,
//     //         SpentPoints = 1,
//     //         OptionalSpecialty = null
//     //     };
//     //         
//     //     var response = await ClientPostBadRequestAsync<AddCharacterSkillViewModel, CommandResponse<Guid?>>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills", createVm);
//     //     response.Should().NotBeNull();
//     //     response.IsSuccess.Should().BeFalse();
//     //     response.Messages.Should().AllSatisfy(message =>
//     //     {
//     //         message.IsOk.Should().BeFalse();
//     //         message.Message.Should().NotBeEmpty();
//     //         message.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//     //     });
//     // } 
//     
//     // [Fact]
//     // public async Task PostSkill_CharacterDontExist_ReturnNotFound()
//     // {
//     //     var createVm = new AddCharacterSkillViewModel
//     //     {
//     //         SkillId = SkillIds.Aquabatics,
//     //         SpentPoints = 1,
//     //         OptionalSpecialty = null
//     //     };
//     //         
//     //     var response = await ClientPostAsync<AddCharacterSkillViewModel, string>($"{RouteTemplates.CharacterApi}/{Guid.NewGuid()}/skills", createVm, HttpStatusCode.NotFound);
//     //     response.Should().Be("Character not found");
//     // } 
//     
//     // [Fact]
//     // public async Task PostSkill_SkillDontExist_ReturnNotFound()
//     // {
//     //     var createVm = new AddCharacterSkillViewModel
//     //     {
//     //         SkillId = Guid.NewGuid(),
//     //         SpentPoints = 1,
//     //         OptionalSpecialty = null
//     //     };
//     //         
//     //     var response = await ClientPostAsync<AddCharacterSkillViewModel, string>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills", createVm, HttpStatusCode.NotFound);
//     //     response.Should().Be("Skill not found");
//     // } 
//     
//     // [Fact]
//     // public async Task PostSkill_WithoutPrerequisites_CreateSkill()
//     // {
//     //     var createVm = new AddCharacterSkillViewModel
//     //     {
//     //         SkillId = SkillIds.Swimming,
//     //         SpentPoints = 1,
//     //         OptionalSpecialty = null,
//     //         DefaultedFrom = 
//     //     };
//     //         
//     //     var characterSkillGuid = await ClientPostAsync<AddCharacterSkillViewModel, Guid>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills", createVm, HttpStatusCode.Created);
//     //     characterSkillGuid.Should().NotBeEmpty();
//     //
//     //     var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//     //     var characterSkill = characterVm.Skills.First(s => s.Id == characterSkillGuid);
//     //     characterSkill.Skill.Id.Should().Be(createVm.SkillId);
//     //     characterSkill.SpentPoints.Should().Be(createVm.SpentPoints);
//     // } 
//     //
//     // [Fact]
//     // public async Task PostSkill_SpentPoints0_ReturnBadRequest()
//     // {
//     //     var createVm = new AddCharacterSkillViewModel
//     //     {
//     //         SkillId = SkillIds.Swimming,
//     //         SpentPoints = 0,
//     //         OptionalSpecialty = null
//     //     };
//     //         
//     //     var problemDetails = await ClientPostBadRequestAsync<AddCharacterSkillViewModel, ValidationProblemDetails>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills", createVm);
//     //     problemDetails.Should().NotBeNull();
//     //     problemDetails!.Errors.Count.Should().Be(1);
//     //     problemDetails.Errors.First().Key.Should().Be(nameof(AddCharacterSkillViewModel.SpentPoints));
//     // } 
//     
//     [Fact]
//     public async Task DeleteSkill_ForExistSkill_DeleteSkill()
//     {
//         var characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//         var skill = characterVm.Skills.First().Id;
//             
//         await ClientDeleteAsync($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills/{skill}", HttpStatusCode.NoContent);
//         
//         characterVm = await ClientGetAsync<CharacterViewModel>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}");
//         characterVm.Skills.FirstOrDefault(s => s.Id == skill).Should().BeNull();
//     } 
//     
//     [Fact]
//     public async Task DeleteSkill_ForNotExistedSkill_ReturnNotFound()
//     {
//        var error = await ClientDeleteAsync<string>($"{RouteTemplates.CharacterApi}/{EfDataSeeder.TestCharacterFrodoId}/skills/{Guid.NewGuid()}", HttpStatusCode.NotFound);
//        error.Should().Be(ErrorMessages.SkillNotFound);
//     }
//     
//     [Fact]
//     public async Task DeleteSkill_ForNotExistedCharacter_ReturnNotFound()
//     {
//         var error = await ClientDeleteAsync<string>($"{RouteTemplates.CharacterApi}/{Guid.NewGuid()}/skills/{Guid.NewGuid()}", HttpStatusCode.NotFound);
//         error.Should().Be(ErrorMessages.CharacterNotFound);
//     }
// }