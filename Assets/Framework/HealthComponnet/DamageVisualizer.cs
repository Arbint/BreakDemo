using System;
using UnityEngine;

public class DamageVisualizer : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    [SerializeField, ColorUsage(true, true)] Color damagedColor;
    [SerializeField] float damageColorDuration = 0.2f;
    [SerializeField] string damageColorMaterialParmName = "_EmissionOffset";
    Color origColor;
    private void Awake()
    {
        HealthComponent healthComponet = GetComponent<HealthComponent>();
        if (healthComponet)
        {
            healthComponet.OnTakenDamage += TookDamage;
        }
        origColor = meshRenderer.material.GetColor(damageColorMaterialParmName);
    }

    private void TookDamage(float newHealth, float delta, float maxHealth, GameObject instigator)
    {
        if(meshRenderer.material.GetColor(damageColorMaterialParmName) == origColor)
        {
            meshRenderer.material.SetColor(damageColorMaterialParmName, damagedColor);
            Invoke("ResetColor", damageColorDuration);
        }
    }

    void ResetColor()
    {
        meshRenderer.material.SetColor(damageColorMaterialParmName, origColor); 
    }
}
