using SagaGuide.Core.Domain.TraitAggregate;

namespace SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

public class TraitEqualityCompare: JsonEqualityComparer<Trait>
{
    public List<Trait> UnifyDateTimeAndDistinct(List<Trait> traits)
    {
        var time = DateTime.UtcNow;
        traits.ForEach(trait =>
        {
            trait.CreatedOn = time;
            trait.ModifiedOn = time;
        });
        
        return traits.Distinct(this).ToList();
    }
    
    public override bool Equals(Trait? lh, Trait? rh)
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
