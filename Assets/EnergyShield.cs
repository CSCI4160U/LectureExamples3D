using UnityEngine;

public class EnergyShield : MonoBehaviour {
    [SerializeField] private GameObject shield;
    [SerializeField] private int maxStrength = 50;
    [SerializeField] private float initialHitRadius = 0.2f;
    [SerializeField] private float hitRadiusDecay = 0.01f;
    [SerializeField] private float hitRadiusThreshold = -0.1f;

    private int strength = 0;
    private float hitRadius = 0f;
    private Material material;

    private void Start() {
        strength = maxStrength;
        material = GetComponent<MeshRenderer>().material;
    }

    private void Update() {
        if (hitRadius > hitRadiusThreshold) {
            hitRadius -= hitRadiusDecay;
            Debug.Log("Updating hit radius: " + hitRadius);
            material.SetFloat("_HitRadius", hitRadius);
        }
    }

    public void RegisterHit(int damage, Vector3 hitPosition) {
        strength -= damage;

        if (strength <= 0) {
            Destroy(shield, 0.5f);
            Destroy(this.gameObject, 0.5f);
        }

        hitRadius = initialHitRadius;
        material.SetVector("_HitPosition", hitPosition);
        material.SetFloat("_HitRadius", hitRadius);
    }
}
