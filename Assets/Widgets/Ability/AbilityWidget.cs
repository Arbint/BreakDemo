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

    [SerializeField] float scaledSize = 1.5f;
    [SerializeField] float scaledOffset = 100f;
    [SerializeField] float scaleRate = 20f;

    Vector3 _goalScale = Vector3.one;
    Vector3 _goalLocalOffset = Vector3.zero;

    Ability _ability;

    /// <summary>
    /// Set the amount of scale
    /// </summary>
    /// <param name="amt"> ranges from 0 to 1, 0 means no scaling up, 1 means full scaling up </param>
    public void SetScaleAmt(float amt)
    {
        _goalScale = Vector3.one * (1 + (scaledSize - 1) * amt);
        _goalLocalOffset = Vector3.left * amt * scaledOffset;
    }

    private void Update()
    {
        rootPanel.transform.localScale = Vector3.Lerp(rootPanel.transform.localScale, _goalScale, Time.deltaTime * scaleRate);
        rootPanel.transform.localPosition = Vector3.Lerp(rootPanel.transform.localPosition, _goalLocalOffset, Time.deltaTime * scaleRate);
    }

    internal void CastAbility()
    {
        _ability.TryActivateAbility();
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
