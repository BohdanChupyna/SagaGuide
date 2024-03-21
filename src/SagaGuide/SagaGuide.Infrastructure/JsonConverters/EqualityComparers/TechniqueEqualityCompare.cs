using SagaGuide.Core.Domain.TechniqueAggregate;

namespace SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

public class TechniqueEqualityCompare: JsonEqualityComparer<Technique>
{
    public List<Technique> UnifyDateTimeAndDistinct(List<Technique> techniques)
    {
        var time = DateTime.UtcNow;
        techniques.ForEach(s =>
        {
            s.CreatedOn = time;
            s.ModifiedOn = time;
        });
        
        return techniques.Distinct(this).ToList();
    }
    
    public override bool Equals(Technique? lh, Technique? rh)
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
