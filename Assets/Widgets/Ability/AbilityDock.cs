using UnityEngine;
using System.Collections.Generic;

public class AbilityDock : Widget
{
    [SerializeField] AbilityWidget abilityWidgetPrefab;
    [SerializeField] RectTransform rootPanel;

    List<AbilityWidget> _abiltyWidgets = new List<AbilityWidget>();

    AbilitySystemComponent _abilitySystemComponent;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        _abilitySystemComponent = newOwner.GetComponent<AbilitySystemComponent>();
        if(_abilitySystemComponent)
        {
            _abilitySystemComponent.onAbilityGiven += AbilityGiven;
        }
    }

    private void AbilityGiven(Ability newAbility)
    {
        AbilityWidget newAbilityWidget = Instantiate(abilityWidgetPrefab, rootPanel);
        newAbilityWidget.Init(newAbility);
        _abiltyWidgets.Add(newAbilityWidget);
    }
}
