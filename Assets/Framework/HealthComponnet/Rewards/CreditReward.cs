using UnityEngine;

[CreateAssetMenu(menuName = "Rewards/Credit Reward")]
public class CreditReward : Reward
{
    [SerializeField] private int rewardAmt = 20;
    public override void ApplyReward(GameObject target)
    {
        CreditComponent creditComponent = target.GetComponent<CreditComponent>();
        if (creditComponent != null)
        {
            creditComponent.ChangeCredit(rewardAmt);
        }
    }
}
