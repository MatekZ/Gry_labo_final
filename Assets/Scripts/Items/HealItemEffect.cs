using UnityEngine;


[CreateAssetMenu(menuName = "Item Effects/Heal")]
public class HealItemEffect : UsableItemEffect
{
    public int HealAmount;
   

	public override void ExecuteEffect(UsableItem usableItem, Character character)
	{
        
/*
        if (damageable && damageable.Health < damageable.MaxHealth)
        {

            bool wasHealed = damageable.Heal((damageable.MaxHealth - damageable.Health));

        }*/
        character.Health = HealAmount;
       

    }

	public override string GetDescription()
	{
		return "Heals for " + HealAmount + " health.";
	}
}
