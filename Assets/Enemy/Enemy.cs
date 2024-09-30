using System;
using Unity.Behavior;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IBehaviorInterface
{
    private HealthComponent _healthComponent;
    private Animator _animator;
    private static readonly int DeadId = Animator.StringToHash("Dead");
    
    private PerceptionComponent _perceptionComponent;
    private BehaviorGraphAgent _behaviorGraphAgent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _healthComponent.OnTakenDamage += TookDamage;
        _healthComponent.OnDead += StartDeath;
        _animator = GetComponent<Animator>();
        _perceptionComponent = GetComponent<PerceptionComponent>();
        _perceptionComponent.OnPerceptionTargetUpdated += HandleTargetUpdate;
        _behaviorGraphAgent = GetComponent<BehaviorGraphAgent>();
    }

    private void HandleTargetUpdate(GameObject target, bool bIsSensed)
    {
        if (bIsSensed)
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("Target", target);
        }
        else
        {
            _behaviorGraphAgent.BlackboardReference.SetVariableValue<GameObject>("Target", null);
            _behaviorGraphAgent.BlackboardReference.SetVariableValue("checkoutLocation", target.transform.position);
        }
    }

    private void StartDeath()
    {
        _animator.SetTrigger(DeadId);
    }

    public void DeathAnimationFinished()
    {
       Destroy(gameObject); 
    }
    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"I took {delta} amt of damage, health is now {newHealth}/{maxHealth}");
    }

    public void Attack(GameObject target)
    {
        _animator.SetTrigger("Attack");
    }
}
