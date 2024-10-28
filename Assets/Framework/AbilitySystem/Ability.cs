using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public delegate void OnAbilityCooldownStaredDelegate(float cooldownDuration);
    public delegate void OnAbilityCanCastChangedDelegate(bool bCanCast);
    public event OnAbilityCanCastChangedDelegate OnAbilityCanCastChanged;

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

    protected void BrocastCanCast()
    {
        OnAbilityCanCastChanged?.Invoke(CanCast());
    }

    public Sprite GetAbilityIcon()
    {
        return abilityIcon;
    }

    public bool TryActivateAbility()
    {
        if(!CanCast())
        {
            return false;
        }

        ActivateAbility();
        return true;
    }

    protected abstract void ActivateAbility();

    public virtual bool CanCast()
    {
        return !_bIsOnCooldown && OwnerASC.Mana >= manaCost;
    }

    public virtual void Init(AbilitySystemComponent abilitySystemComponent)
    {
        OwnerASC = abilitySystemComponent; 
        OwnerASC.onManaUpdated += (mana, delta, maxMana) => BrocastCanCast();
    }
    private void StartCooldown()
    {
        OnAbilityCooldownStared?.Invoke(cooldownDuration); 
        OwnerASC.StartCoroutine(CooldownCoroutine());
        BrocastCanCast();
    }

    IEnumerator CooldownCoroutine()
    {
        _bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        _bIsOnCooldown = false;
        BrocastCanCast();
    }

    protected bool CommitAbility()
    {
        if (!OwnerASC)
            return false;

        if (_bIsOnCooldown)
            return false;

        if (!OwnerASC.TryConsumeMana(manaCost))
            return false;

        StartCooldown();
        return true;
    }
}
