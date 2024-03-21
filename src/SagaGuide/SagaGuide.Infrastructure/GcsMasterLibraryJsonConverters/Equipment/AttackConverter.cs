using SagaGuide.Core.Domain.EquipmentAggregate;
using SagaGuide.Core.Domain.SkillAggregate;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SagaGuide.Infrastructure.GcsMasterLibraryJsonConverters.Equipment;

public class AttackConverter : JsonConverter<Attack>
{
    public override void WriteJson(JsonWriter writer, Attack? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override Attack ReadJson(JsonReader reader, Type objectType, Attack? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var attackType = jsonObject["type"]!.Value<string>();

        Attack attack;
        if (attackType == "melee_weapon")
        {
            var meleeAttack = new MeleeAttack();

            meleeAttack.Reach = jsonObject.GetValue("reach", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer)!;
            meleeAttack.Parry = jsonObject.GetValue("parry", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer)!;
            meleeAttack.Block = jsonObject.GetValue("block", StringComparison.OrdinalIgnoreCase)?.ToObject<string>(serializer);
            attack = meleeAttack;
        }
        else if (attackType == "ranged_weapon")
        {
            var rangeAttack = new RangedAttack();

            rangeAttack.Accuracy = jsonObject.GetValue("accuracy", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            rangeAttack.Range = jsonObject.GetValue("range", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            rangeAttack.RateOfFire = jsonObject.GetValue("rate_of_fire", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            rangeAttack.Shots = jsonObject.GetValue("shots", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            rangeAttack.Bulk = jsonObject.GetValue("bulk", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            rangeAttack.Recoil = jsonObject.GetValue("recoil", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
            attack = rangeAttack;
        }
        else
            throw new Exception("unknown weapon type");

        attack.Id = Guid.Parse(jsonObject.GetValue("id", StringComparison.OrdinalIgnoreCase)!.ToObject<string>(serializer)!);
        attack.MinimumStrength = jsonObject.GetValue("strength", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer) ?? "0";
        attack.Usage = jsonObject.GetValue("usage", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        attack.UsageNotes = jsonObject.GetValue("usage_notes", StringComparison.OrdinalIgnoreCase)?.ToObject<string?>(serializer);
        attack.Defaults = jsonObject.GetValue("defaults", StringComparison.OrdinalIgnoreCase)?.ToObject<List<SkillDefault>>(serializer) ?? new List<SkillDefault>();
        attack.Damage = jsonObject.GetValue("damage", StringComparison.OrdinalIgnoreCase)?.ToObject<Damage>(serializer)!;
        
       return attack;
    }
}
