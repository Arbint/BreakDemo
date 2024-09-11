using System;
using UnityEngine;

public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimRange = 10f;
    [SerializeField] private LayerMask aimMask;
    public GameObject GetAimTarget()
    {
        Vector3 aimStart = muzzle.position;
        Vector3 aimDir = muzzle.forward;

        if (Physics.Raycast(aimStart, aimDir, out RaycastHit hitInfo, aimRange, aimMask))
        {
            return hitInfo.collider.gameObject;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(muzzle.position, muzzle.position + muzzle.forward * aimRange);
    }
}
