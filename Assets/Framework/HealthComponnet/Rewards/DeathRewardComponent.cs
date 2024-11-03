using System;
using UnityEngine;

public class DeathRewardComponent : MonoBehaviour
{
    [SerializeField] private Reward[] rewards;
    private void Awake()
    {
        HealthComponent _healthComponent = GetComponent<HealthComponent>();
        if (_healthComponent != null)
        {
            _healthComponent.OnDead += RewardKiller;
        }
    }

    private void RewardKiller(GameObject killer)
    {
        foreach (Reward reward in rewards)
        {
            reward.ApplyReward(killer); 
        }
    }
}
