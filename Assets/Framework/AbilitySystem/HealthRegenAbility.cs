using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Health Regen")]
public class HealthRegenAbility : Ability
{
    [SerializeField] float regenAmt = 20f;
    [SerializeField] float regenDuration = 3f;

    HealthComponent _ownerHealthComponent;
    public override void Init(AbilitySystemComponent abilitySystemComponent)
    {
        base.Init(abilitySystemComponent);
        _ownerHealthComponent = abilitySystemComponent.GetComponent<HealthComponent>();
        _ownerHealthComponent.OnHealthChanged += (_,_,_,_) => BrocastCanCast();
    }

    public override bool CanCast()
    {
        return base.CanCast() && _ownerHealthComponent.GetHealth() != _ownerHealthComponent.GetMaxHealth();
    }

    protected override void ActivateAbility()
    {
        if (!CommitAbility())
            return;

        OwnerASC.StartCoroutine(HealthRegenCoroutine());
    }

    IEnumerator HealthRegenCoroutine()
    {
        float counter = 0f;
        float regenRate = regenAmt / regenDuration;
        while(counter < regenDuration)
        {
            counter += Time.deltaTime;
            _ownerHealthComponent.ChangeHealth(regenRate * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
