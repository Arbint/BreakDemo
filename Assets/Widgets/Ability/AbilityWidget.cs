using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] Image iconImage;
    [SerializeField] Image cooldownImage;

    Ability _ability;

    internal void CastAbility()
    {
        _ability.ActivateAbility();
    }

    internal void Init(Ability newAbility)
    {
        _ability = newAbility;
        iconImage.sprite = _ability.GetAbilityIcon();
    }
}
