namespace SagaGuide.Infrastructure.EntityFramework.DataSeeders;

public interface IDataSeeder
{
    Task SeedAsync();
    void Seed();

    Task SeedSkillsAsync();
    void SeedSkills();
}