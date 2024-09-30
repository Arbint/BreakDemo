using UnityEngine;
using UnityEngine.UI; 

public class PlayerHealthBar : Widget
{
    [SerializeField] private Image healthBarImage;
    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        HealthComponent ownerHealthComp = newOwner.GetComponent<HealthComponent>();
        if (ownerHealthComp)
        {
            ownerHealthComp.OnHealthChanged += UpdateHealth;
            UpdateHealth(ownerHealthComp.GetHealth(), 0, ownerHealthComp.GetMaxHealth(), newOwner);
        }
    }

    private void UpdateHealth(float newhealth, float delta, float maxhealth, GameObject instigator)
    {
        healthBarImage.fillAmount = newhealth / maxhealth;
    }
}
