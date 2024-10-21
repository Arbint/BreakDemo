using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityWidget : MonoBehaviour
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] Image iconImage;
    [SerializeField] Image cooldownImage;
    [SerializeField] float cooldownUpdateInterval = 0.05f;

    Ability _ability;

    internal void CastAbility()
    {
        _ability.ActivateAbility();
    }

    internal void Init(Ability newAbility)
    {
        _ability = newAbility;
        if(_ability)
            _ability.OnAbilityCooldownStared += StartCooldown;

        iconImage.sprite = _ability.GetAbilityIcon();
    }

    private void StartCooldown(float cooldownDuration)
    {
        StartCoroutine(CooldownCoroutine(cooldownDuration));        
    }

    IEnumerator CooldownCoroutine(float cooldownDuration)
    {
        float cooldownCounter = cooldownDuration;
        while(cooldownCounter > 0)
        {
            cooldownCounter -= cooldownUpdateInterval;
            cooldownImage.fillAmount = cooldownCounter / cooldownDuration;
            yield return new WaitForSeconds(cooldownUpdateInterval);
        }

        cooldownImage.fillAmount = 0;
    }
}
