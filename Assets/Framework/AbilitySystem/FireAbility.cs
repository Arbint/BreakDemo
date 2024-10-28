using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Fire")]
public class FireAbility : Ability
{
    [SerializeField] float damageAmt = 50f;
    [SerializeField] float damageDuration = 3f;

    [SerializeField] float damageRadius = 20f;
    [SerializeField] float damageScanDuration = 1f;

    [SerializeField] GameObject ScanVFX;
    [SerializeField] GameObject BurnVFX;

    [SerializeField] Scaner scanerPrefab;

    protected override void ActivateAbility()
    {
        if (!CommitAbility())
            return;

        Scaner newScaner = Instantiate(scanerPrefab, OwnerASC.gameObject.transform);
        newScaner.OnTargetDetected += TargetDetected;
        Instantiate(ScanVFX, newScaner.ScanPivot);
        newScaner.StartScan(damageRadius, damageScanDuration);
    }

    private void TargetDetected(GameObject newTarget)
    {
        ITeamInterface targetTeamInteface = newTarget.GetComponent<ITeamInterface>();
        if (targetTeamInteface == null)
            return;

        if (targetTeamInteface.GetTeamAttitudeTowards(OwnerASC.gameObject) != TeamAttitude.Enemy)
            return;

        HealthComponent targetHealthComp = newTarget.GetComponent<HealthComponent>();
        if (targetHealthComp == null)
            return;

        OwnerASC.StartCoroutine(DamageCoroutine(targetHealthComp)); 
    }

    IEnumerator DamageCoroutine(HealthComponent targetHealthComponent)
    {
        float counter = 0;
        float damageRate = damageAmt / damageDuration;
        GameObject newBurnVFX = Instantiate(BurnVFX, targetHealthComponent.transform);
        while(counter < damageDuration && targetHealthComponent != null)
        {
            counter += Time.deltaTime;
            targetHealthComponent.ChangeHealth(-damageRate * Time.deltaTime, OwnerASC.gameObject);
            yield return new WaitForEndOfFrame();
        }

        Destroy(newBurnVFX);
    }
}
