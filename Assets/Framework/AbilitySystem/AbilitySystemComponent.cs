using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AbilitySystemComponent : MonoBehaviour, IPurchaseListener
{
    public delegate void OnAbilityGivenDelegate(Ability newAbility);
    public event OnAbilityGivenDelegate onAbilityGiven;
    public delegate void OnManaUpdatedDelegate(float newMana, float delta, float maxMana);
    public event OnManaUpdatedDelegate onManaUpdated;

    [SerializeField] float maxMana = 100f;
    [SerializeField] Ability[] initialAbilities;

    List<Ability> _abilities = new List<Ability>();
    private float _mana;

    public float Mana
    {
        get => _mana;
        private set => _mana = value;
    }

    public float MaxMana
    {
        get => maxMana;
        private set => maxMana = value;
    }

    private void Awake()
    {
        _mana = maxMana;
    }

    private void Start()
    {
        foreach (Ability ability in initialAbilities)
        {
            GiveAbility(ability); 
        }

        onManaUpdated?.Invoke(_mana, 0, maxMana);
    }

    public void GiveAbility(Ability newAbility)
    {
        Ability ability = Instantiate(newAbility);
        ability.Init(this);

        _abilities.Add(ability);
        onAbilityGiven?.Invoke(ability);
    }

    internal bool TryConsumeMana(float manaCost)
    {
        if (_mana < manaCost)
            return false;

        ChangeMana(-manaCost);
        return true;
    }

    public bool HandlePurchase(Object newPurchase)
    {
        Ability itemAsAbility = newPurchase as Ability;
        if (itemAsAbility == null)
            return false;

        GiveAbility(itemAsAbility);
        return true;
    }

    public void ChangeMana(float amt)
    {
       _mana += amt; 
       _mana = Mathf.Clamp(_mana, 0f, maxMana);
       onManaUpdated?.Invoke(_mana, amt, maxMana);
    }
}
