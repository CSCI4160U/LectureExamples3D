using UnityEngine;

public class Ragdoller : MonoBehaviour {
    [SerializeField] private Rigidbody mainBody = null;

    public void Ragdoll(Transform newTransform) {
        // set up our ragdoll to be in the same position/rotation as the original object
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }

    public void ApplyForce(Vector3 forceDirection, float forceAmount) {
        if (mainBody != null && forceAmount > 0) {
            mainBody.AddForce(forceDirection * forceAmount);
        } else {
            Debug.Log("ApplyForce::No force applied");
        }
    }
}
