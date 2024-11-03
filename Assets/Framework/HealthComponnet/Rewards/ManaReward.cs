using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/Mana Reward")]
public class ManaReward : Reward
{
    [SerializeField] private float manaRewardAmt = 20f;
    public override void ApplyReward(GameObject target)
    {
        AbilitySystemComponent abilitySystemComponent = target.GetComponent<AbilitySystemComponent>();
        if (abilitySystemComponent != null)
        {
            abilitySystemComponent.ChangeMana(manaRewardAmt);
        }
    }
}
