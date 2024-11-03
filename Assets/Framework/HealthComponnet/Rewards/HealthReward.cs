using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/Health Reward")]
public class HealthReward : Reward
{
    [SerializeField] private float healthRewardAmt = 20f;
    public override void ApplyReward(GameObject target)
    {
        HealthComponent healthComponent = target.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(healthRewardAmt, target);
        }
    }
}
