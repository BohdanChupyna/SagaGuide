using SagaGuide.Core.Domain.SkillAggregate;

namespace SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

public class SkillEqualityCompare : JsonEqualityComparer<Skill>
{
    public List<Skill> UnifyDateTimeAndDistinct(List<Skill> skills)
    {
        var time = DateTime.UtcNow;
        skills.ForEach(s =>
        {
            s.CreatedOn = time;
            s.ModifiedOn = time;
        });
        
        return skills.Distinct(this).ToList();
    }

    public override bool Equals(Skill? lh, Skill? rh)
    {
        if (lh == null || rh == null)
        {
            return false;
        }
        
        if (!lh.Name.ToLower().Equals(rh.Name.ToLower()))
        {
            return false;
        }

        return base.Equals(lh, rh);
    }
}
