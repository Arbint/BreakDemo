using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    ITeamInterface _instigatorTeamInteface;
    Rigidbody _rigidbody;

    [SerializeField] float _projectileThrowHeight = 3f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
    }
    public void Launch(Vector3 destination, GameObject instigator)
    {
        _instigatorTeamInteface = instigator.GetComponent<ITeamInterface>();
        float gravity = Physics.gravity.magnitude;
        float travelHalfTime = Mathf.Sqrt(2f * _projectileThrowHeight/gravity);
        float verticalSpeed = gravity * travelHalfTime;

        Vector3 destinationVector = destination - transform.position;
        destinationVector.y = 0f;
        float horizontalSpeed = destinationVector.magnitude / (travelHalfTime * 2f); 
        Vector3 launchVelocity = verticalSpeed * Vector3.up + destinationVector.normalized * horizontalSpeed;

        _rigidbody.AddForce(launchVelocity, ForceMode.VelocityChange);
     }
}
