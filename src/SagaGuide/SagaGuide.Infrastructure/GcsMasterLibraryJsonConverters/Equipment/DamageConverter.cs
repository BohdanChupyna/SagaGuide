using SagaGuide.Core.Domain.EquipmentAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;

public class DamageConverter : JsonConverter<Damage>
{
    public override void WriteJson(JsonWriter writer, Damage? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Damage? ReadJson(JsonReader reader, Type objectType, Damage? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var damage = new Damage();
        
        damage.DamageType = jsonObject.GetValue("type", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!;
        damage.AttackType = jsonObject.GetValue("st", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        damage.BaseDamage = jsonObject.GetValue("base", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        damage.ArmorDivisor = jsonObject.GetValue("armor_divisor", StringComparison.OrdinalIgnoreCase)?.ToObject<double?>(serializer);
        
        damage.Fragmentation = jsonObject.GetValue("fragmentation", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        damage.FragmentationArmorDivisor = jsonObject.GetValue("fragmentation_armor_divisor", StringComparison.OrdinalIgnoreCase)?.ToObject<double?>(serializer);
        damage.FragmentationAttackType = jsonObject.GetValue("fragmentation_type", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
        damage.ModifierPerDie = jsonObject.GetValue("modifier_per_die", StringComparison.OrdinalIgnoreCase)?.ToObject<int?>(serializer);
        
        return damage;
    }
}