using SagaGuide.Core.Domain.EquipmentAggregate;

namespace SagaGuide.Infrastructure.JsonConverters.EqualityComparers;

public class EquipmentEqualityCompare: JsonEqualityComparer<Equipment>
{
    public List<Equipment> UnifyDateTimeAndDistinct(List<Equipment> equipments)
    {
        var time = DateTime.UtcNow;
        equipments.ForEach(e =>
        {
            e.CreatedOn = time;
            e.ModifiedOn = time;
        });
        
        return equipments.Distinct(this).ToList();
    }
    
    public override bool Equals(Equipment? lh, Equipment? rh)
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
