using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerManaBar : Widget
{
    [SerializeField] private Image manaBarImage;
    [SerializeField] private TextMeshProUGUI valueText;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        AbilitySystemComponent ownerASC = newOwner.GetComponent<AbilitySystemComponent>(); 
        if (ownerASC)
        {
            ownerManaComp.OnManaChanged += UpdateMana;
            UpdateMana(ownerManaComp.GetMana(), 0, ownerManaComp.GetMaxMana(), newOwner);
        }
    }

    private void UpdateMana(float newMana, float delta, float maxMana, GameObject instigator)
    {
        manaBarImage.fillAmount = newMana / maxMana;
        valueText.text = $"{newMana}/{maxMana}";
    }
}
