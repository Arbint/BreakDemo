using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStaredDelegate(float cooldownDuration);
    [SerializeField] float cooldownDuration = 3f;
    [SerializeField] float manaCost = 10f;
    [SerializeField] Sprite abilityIcon;

    public event OnAbilityCooldownStaredDelegate OnAbilityCooldownStared;
    bool _bIsOnCooldown;

    protected AbilitySystemComponent OwnerASC
    {
        get;
        private set;
    }

    public Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }

    public abstract void ActivateAbility();

    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerASC = abilitySystemComponent; 
    }
    private void StartCooldown()
    {
        OnAbilityCooldownStared?.Invoke(cooldownDuration); 
        OwnerASC.StartCoroutine(CooldownCoroutine()); 
    }

    IEnumerator CooldownCoroutine()
    {
        _bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCooldown = false;
    }

    protected bool CommitAbility()
    {
        if (!OwnerASC)
            return false;

        if (!OwnerASC.TryConsumeMana(manaCost))
            return false;

        if (_bIsOnCooldown)
            return false;

        StartCooldown();
        return true;
    }
}
