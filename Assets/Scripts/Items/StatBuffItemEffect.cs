using System.Collections;
using UnityEngine;
using Kryz.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
	public int StrengthBuff, DefBuff;
	public float Duration;

	public override void ExecuteEffect(UsableItem parentItem, Character character)
	{
		StatModifier statModifier1 = new StatModifier(StrengthBuff, StatModType.Flat, parentItem);
        StatModifier statModifier2 = new StatModifier(DefBuff, StatModType.Flat, parentItem);
        character.Strength.AddModifier(statModifier1);
        character.Vitality.AddModifier(statModifier2);
        character.UpdateStatValues();
		character.StartCoroutine(RemoveBuff(character, statModifier1, statModifier1, Duration));
	}

	public override string GetDescription()
	{
		return "Grants " + StrengthBuff + " Strength and " + DefBuff + " defence for " + Duration + " seconds.";
	}

	private static IEnumerator RemoveBuff(Character character, StatModifier statModifier1, StatModifier statModifier2, float duration)
	{
		yield return new WaitForSeconds(duration);
		character.Strength.RemoveModifier(statModifier1);
        character.Vitality.RemoveModifier(statModifier2);
        character.UpdateStatValues();
	}
}
