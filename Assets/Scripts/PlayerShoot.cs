using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private Transform firePosition = null;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private AudioSource gunshotAudioSource;

    private float range = 100f;

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        RaycastHit hit;

        if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, range, enemyLayers)) {
            Debug.Log("Hit enemy: " + hit.collider.name);

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(10);

            if (enemyHealth.IsDead()) {
                Animator enemyAnimator = hit.collider.GetComponent<Animator>();
                enemyAnimator.SetBool("Dead", true);
            }
        } else if (Physics.Raycast(firePosition.position, firePosition.forward, out hit, range, wallLayers)) {
            Debug.Log("Hit wall: " + hit.collider.name);

            gunshotAudioSource.Play();
            Instantiate(bulletHolePrefab, hit.point + (0.01f * hit.normal), Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
        }
    }
}
