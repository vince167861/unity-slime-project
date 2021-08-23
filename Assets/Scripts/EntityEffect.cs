public class EntityEffect
{
	public enum EntityEffectType { Paralyze };
	public readonly EntityEffectType effectType;
	public readonly float duration;

	public EntityEffect(EntityEffectType t, float d)
	{
		duration = d;
		effectType = t;
	}
}
