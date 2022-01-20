using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private Transform launchPosition;

    [Range(0, 100)] [SerializeField] private float launchVelocity = 10f;

    private void Update() {
        // called every (graphics) frame
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    [ContextMenu("Fire")]
    public void Fire() {
        Debug.Log("Firing cannon!");

        var body = Instantiate(projectilePrefab);
        body.position = launchPosition.position;
        body.velocity = transform.up * launchVelocity;
    }
}
