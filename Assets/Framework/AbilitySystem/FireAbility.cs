using UnityEngine;

public class FireAbility : Ability
{
    [SerializeField] float damageAmt = 50f;
    [SerializeField] float damageDuration = 3f;

    protected override void ActivateAbility()
    {
        if (!CommitAbility())
            return;


    }
}
