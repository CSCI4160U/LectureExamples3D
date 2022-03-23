using UnityEngine;

public class MeleeDamage : MonoBehaviour {
    [SerializeField] private int damage = 5;

    private void OnTriggerEnter(Collider other) {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null) {
            health.TakeDamage(damage);
        }
    }
}
