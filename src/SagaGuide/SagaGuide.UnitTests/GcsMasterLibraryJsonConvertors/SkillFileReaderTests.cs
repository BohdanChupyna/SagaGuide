using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using System.Linq;
using SagaGuide.Core.Domain.Common;
using SagaGuide.Core.Domain.Prerequisite;
using SagaGuide.Core.Domain.SkillAggregate;
using SagaGuide.Core.Domain.TechniqueAggregate;
using SagaGuide.Infrastructure.EntityFramework.GcsMasterLibrarySeeder;
using SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Skill;
using SagaGuide.Infrastructure.JsonConverters.EqualityComparers;
using SagaGuide.TestData;
using Attribute = SagaGuide.Core.Domain.Attribute;

namespace SagaGuide.UnitTests.GcsMasterLibraryJsonConvertors;

public class SkillFileReaderTests
{
     [Fact]
    public void ParseAllGcsLibrarySkills_Success()
    {
        List<string> filePaths = TestUtils.GetFilePathsByExtension(TestUtils.GcsMasterLibraryFolderPath, GcsMasterLibraryFileExtensions.SkillsAndTechniques);
    
        var skills = new List<Skill>();
        var techniques = new List<Technique>();
        //var filePath = "D:\\development\\gcs\\gcs_master_library-master\\Library\\Basic Set\\Basic Set Skills.skl";
    
        foreach (var filePath in filePaths)
        {
            SkillFileReader.Read(filePath, out var res, out var tech);
            skills.AddRange(res);
            techniques.AddRange(tech);
        }
    
        var count = skills.Count;
        var techCount = techniques.Count;
        
        // var uniqueSkills = new SkillEqualityCompare().UnifyDateTimeAndDistinct(skills);
        // var uniqueTechniques = new TechniqueEqualityCompare().UnifyDateTimeAndDistinct(techniques).ToList();
    }

    [Fact]
    public void Technique_ParseSuccessfully()
    {
        var filePath = TestFilesPaths.Skills;
        SkillFileReader.Read(filePath, out _, out var techniques);
        techniques.Count.Should().Be(3);

        var technique = techniques[0];
        technique.Id.Should().Be("df4adc7d-6ce7-41bd-a5ee-5d22e3605cd3");
        technique.Name.Should().Be("Feint");
        
        technique.BookReferences.Count.Should().Be(2);
        technique.BookReferences[0].SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        technique.BookReferences[0].PageNumber.Should().Be(231);
        technique.BookReferences[0].MagazineNumber.Should().BeNull();
        technique.BookReferences[1].SourceBook.Should().Be(BookReference.SourceBookEnum.PyramidMagazine);
        technique.BookReferences[1].PageNumber.Should().Be(19);
        technique.BookReferences[1].MagazineNumber.Should().Be(65);
        
        technique.Tags.Count().Should().Be(4);
        technique.Tags[0].Should().Be("Combat");
        technique.Tags[1].Should().Be("Melee Combat");
        technique.Tags[2].Should().Be("Technique");
        technique.Tags[3].Should().Be("Weapon");

        technique.DifficultyLevel.Should().Be(Skill.DifficultyLevelEnum.Hard);
        technique.PointsCost.Should().Be(2);
        var techniqueDefault = technique.Default;
        techniqueDefault.Name.Should().Be("@Unarmed or Melee Weapon Skill@");
        techniqueDefault.Modifier.Should().Be(0);
        technique.TechniqueLimitModifier.Should().Be(4);

        technique.Prerequisites.Should().NotBeNull();
        technique.Prerequisites!.Prerequisites.Count.Should().Be(1);
        var skillPrerequisite = (SkillPrerequisite)technique.Prerequisites!.Prerequisites.First();
        skillPrerequisite.ShouldBe.Should().BeTrue();
        skillPrerequisite.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillPrerequisite.NameCriteria.Qualifier.Should().Be("@Unarmed or Melee Weapon Skill@");
    }

    [Fact]
    public void TechniqueEqualityCompare_ReturnUniqueSkillsByNameAndDefault()
    {
        var filePath = TestFilesPaths.Skills;
        SkillFileReader.Read(filePath, out _, out var techniques);
        techniques.Count.Should().Be(3);
        techniques[0].Name.Should().Be("Feint");
        techniques[0].Default.Name.Should().Be("@Unarmed or Melee Weapon Skill@");
        techniques[1].Name.Should().Be("Feint");
        techniques[1].Default.Name.Should().Be("Brawling");
        techniques[1].Default.Modifier.Should().Be(0);
        techniques[2].Name.Should().Be("Feint");
        techniques[2].Default.Name.Should().Be("Brawling");
        techniques[2].Default.Modifier.Should().Be(0);
        
        var uniqueTechniques = new TechniqueEqualityCompare().UnifyDateTimeAndDistinct(techniques);
        uniqueTechniques.Count.Should().Be(2);
        
        uniqueTechniques[0].Name.Should().Be("Feint");
        uniqueTechniques[0].Default.Name.Should().Be("@Unarmed or Melee Weapon Skill@");
        uniqueTechniques[0].Default.Modifier.Should().Be(0);
        
        uniqueTechniques[1].Name.Should().Be("Feint");
        uniqueTechniques[1].Default.Name.Should().Be("Brawling");
        uniqueTechniques[1].Default.Modifier.Should().Be(0);
    }

    [Fact]
    public void SkillEqualityCompare_ReturnUniqueSkillsByNameAndSpecialization()
    {
        var filePath = TestFilesPaths.Skills;
        SkillFileReader.Read(filePath, out var skills, out _);
        skills.Count.Should().Be(4);
        
        var uniqueSkills = new SkillEqualityCompare().UnifyDateTimeAndDistinct(skills);
        uniqueSkills.Count.Should().Be(3);
        uniqueSkills[0].Name.Should().Be("Accounting");
        uniqueSkills[0].Specialization.Should().BeNull();
        uniqueSkills[1].Name.Should().Be("Engineer");
        uniqueSkills[1].Specialization.Should().Be("Materials");
        uniqueSkills[2].Name.Should().Be("Accounting");
        uniqueSkills[2].Specialization.Should().Be("Statistics");
    }
    
    [Fact]
    public void SimpleSkillWithoutPrerequisites_ParseSuccessfully()
    {
        var filePath = TestFilesPaths.Skills;
        SkillFileReader.Read(filePath, out var skills, out _);
        skills.Count.Should().Be(4);
        var accounting = skills[0];
        accounting.Id.Should().Be("435aab38-e1ac-43ac-89bd-c72cd1b2dc1a");
        accounting.Name.Should().Be("Accounting");
        
        accounting.BookReferences.Count.Should().Be(1);
        accounting.BookReferences.First().SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        accounting.BookReferences.First().PageNumber.Should().Be(174);

        accounting.Tags.Count.Should().Be(1);
        accounting.Tags.First().Should().Be("Business");

        accounting.AttributeType.Should().Be(Attribute.AttributeType.Intelligence);
        accounting.DifficultyLevel.Should().Be(Skill.DifficultyLevelEnum.Hard);
        accounting.PointsCost.Should().Be(1);
        
        accounting.Defaults.Count.Should().Be(4);
        accounting.Prerequisites.Should().BeNull();
        
        accounting.Defaults[0].AttributeType.Should().Be(Attribute.AttributeType.Intelligence);
        accounting.Defaults[0].Modifier.Should().Be(-6);
        
        accounting.Defaults[1].Name.Should().Be("Finance");
        accounting.Defaults[1].Modifier.Should().Be(-4);
        
        accounting.Defaults[2].Name.Should().Be("Mathematics");
        accounting.Defaults[2].Specialization.Should().Be("Statistics");
        accounting.Defaults[2].Modifier.Should().Be(-5);
        
        accounting.Defaults[3].Name.Should().Be("Merchant");
        accounting.Defaults[3].Modifier.Should().Be(-5);
    }
    
    [Fact]
    public void ComplexSkillWithPrerequisites_ParseSuccessfully()
    {
        var filePath = TestFilesPaths.Skills;
        SkillFileReader.Read(filePath, out var skills, out _);
        skills.Count.Should().Be(4);
        var skill = skills[1];
        skill.Id.Should().Be("c0c61ffa-13d5-46aa-a50b-9376a1b29212");
        skill.Name.Should().Be("Engineer");
        skill.LocalNotes.Should().Be("All important citizens and businesses, and most unimportant ones; all public buildings and most houses");
        
        skill.BookReferences.Count.Should().Be(1);
        skill.BookReferences.First().SourceBook.Should().Be(BookReference.SourceBookEnum.BasicSet);
        skill.BookReferences.First().PageNumber.Should().Be(190);

        skill.Tags.Count.Should().Be(3);
        skill.Tags[0].Should().Be("Design");
        skill.Tags[1].Should().Be("Engineer");
        skill.Tags[2].Should().Be("Invention");

        skill.Specialization.Should().Be("Materials");
        skill.AttributeType.Should().Be(Attribute.AttributeType.Intelligence);
        skill.DifficultyLevel.Should().Be(Skill.DifficultyLevelEnum.Hard);
        skill.PointsCost.Should().Be(1);
        
        skill.Defaults.Count.Should().Be(2);
        skill.Prerequisites.Should().NotBeNull();
        
        skill.Defaults[0].Name.Should().Be("Chemistry");
        skill.Defaults[0].Modifier.Should().Be(-6);
        
        skill.Defaults[1].Name.Should().Be("Metallurgy");
        skill.Defaults[1].Modifier.Should().Be(-6);

        skill.Prerequisites!.Prerequisites.Count.Should().Be(2);
        var prerequisiteGroup = (PrerequisiteGroup)skill.Prerequisites.Prerequisites[0];
        prerequisiteGroup.ShouldAllBeSatisfied.Should().Be(true);
        prerequisiteGroup.WhenTechLevel!.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        prerequisiteGroup.WhenTechLevel!.Qualifier.Should().Be(5);
        prerequisiteGroup.Prerequisites.Count.Should().Be(1);
        
        var skillPrerequisite = (SkillPrerequisite)prerequisiteGroup.Prerequisites[0];
        skillPrerequisite.ShouldBe.Should().Be(true);
        skillPrerequisite.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillPrerequisite.NameCriteria.Qualifier.Should().Be("mathematics");
        skillPrerequisite.SpecializationCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillPrerequisite.SpecializationCriteria!.Qualifier.Should().Be("applied");
        skillPrerequisite.LevelCriteria.Should().BeNull();

        prerequisiteGroup = (PrerequisiteGroup)skill.Prerequisites.Prerequisites[1];
        prerequisiteGroup.ShouldAllBeSatisfied.Should().Be(false);
        prerequisiteGroup.WhenTechLevel.Should().BeNull();
        prerequisiteGroup.Prerequisites.Count.Should().Be(3);
        
        skillPrerequisite = (SkillPrerequisite)prerequisiteGroup.Prerequisites[0];
        skillPrerequisite.ShouldBe.Should().Be(true);
        skillPrerequisite.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.Is);
        skillPrerequisite.NameCriteria.Qualifier.Should().Be("metallurgy");
        skillPrerequisite.SpecializationCriteria.Should().BeNull();
        skillPrerequisite.LevelCriteria!.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        skillPrerequisite.LevelCriteria!.Qualifier.Should().Be(14);

        var traitPrerequisite = (TraitPrerequisite)prerequisiteGroup.Prerequisites[1];
        traitPrerequisite.ShouldBe.Should().Be(true);
        traitPrerequisite.NameCriteria.Comparison.Should().Be(StringCriteria.ComparisonType.StartsWith);
        traitPrerequisite.NameCriteria.Qualifier.Should().Be("weapon master");
        traitPrerequisite.NotesCriteria!.Comparison.Should().Be(StringCriteria.ComparisonType.Contains);
        traitPrerequisite.NotesCriteria!.Qualifier.Should().Be("Written (Native");
        traitPrerequisite.LevelCriteria!.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        traitPrerequisite.LevelCriteria!.Qualifier.Should().Be(10);

        var attributePrerequisite = (AttributePrerequisite)prerequisiteGroup.Prerequisites[2];
        attributePrerequisite.ShouldBe.Should().Be(true);
        attributePrerequisite.QualifierCriteria.Comparison.Should().Be(IntegerCriteria.ComparisonType.AtLeast);
        attributePrerequisite.QualifierCriteria.Qualifier.Should().Be(12);
        attributePrerequisite.Required.Should().Be(Attribute.AttributeType.Dexterity);
        attributePrerequisite.CombinedWith.Should().Be(Attribute.AttributeType.Will);
    }
}