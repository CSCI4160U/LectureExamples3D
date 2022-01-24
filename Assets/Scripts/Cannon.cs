using UnityEngine;

public class Cannon : MonoBehaviour {
    [SerializeField] private Rigidbody _projectilePrefab;
    [SerializeField] private Transform _launchPosition;

    [Range(0, 100)] [SerializeField] private float _launchVelocity = 10f;

    public Rigidbody ProjectilePrefab {
        get { return _projectilePrefab; }
    }

    public float LaunchVelocity {
        get { return _launchVelocity; }
    }

    public Transform LaunchPosition {
        get { return _launchPosition; }
        set { _launchPosition = value; }
    }

    private void Update() {
        // called every (graphics) frame
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    [ContextMenu("Fire")]
    public void Fire() {
        Debug.Log("Firing cannon!");

        var body = Instantiate(_projectilePrefab);
        body.position = _launchPosition.position;
        body.velocity = transform.up * _launchVelocity;
    }
}
