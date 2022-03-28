using System.Collections.Generic;
using UnityEngine;
public class Barrel : MonoBehaviour {
    [SerializeField] private float radius = 3;
    [SerializeField] private GameObject explosionEffect = null;
    [SerializeField] private Transform explosionPoint = null;
    [SerializeField] private int damage = 100;
    [SerializeField] private bool isDestroyed = false;
    [SerializeField] private float forceAmount = 50000f;
    private List<Barrel> barrelsToExplode = null;

    private void Start() {
        barrelsToExplode = new List<Barrel>();
    }
    
    public void Explode() {
        Instantiate(explosionEffect, explosionPoint.position, Quaternion.identity);
        Destroy(transform.gameObject, 0.25f);

        // see if any players are nearby to damage
        LayerMask playerMask = LayerMask.GetMask("Player");
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerMask);
        if (hits.Length > 0) {
            PlayerHealth health = hits[0].GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }

        // see if any enemies are nearby to damage
        LayerMask enemyMask = LayerMask.GetMask("Enemies");
        hits = Physics.OverlapSphere(transform.position, radius, enemyMask);
        if (hits.Length > 0) {
            EnemyHealthRagdoll enemyHealth = hits[0].GetComponent<EnemyHealthRagdoll>();
            if (enemyHealth != null) {
                enemyHealth.TakeExplosionDamage(explosionPoint.position, forceAmount);
            }
        }

        // prevent an infinite cascade, remember that this barrel is already destroyed
        isDestroyed = true;

        // see if any other barrels are nearby to cascade
        LayerMask barrelMask = LayerMask.GetMask("Barrels");
        hits = Physics.OverlapSphere(transform.position, radius, barrelMask);
        barrelsToExplode.Clear();
        for (int i = 0; i < hits.Length; i++) {
            Barrel barrel = hits[i].GetComponent<Barrel>();
            if (!barrel.isDestroyed) {
                barrelsToExplode.Add(barrel);
            }
        }

        if (barrelsToExplode.Count > 0) {
            CascadeBarrels();
        }
    }
    void CascadeBarrels() {
        foreach (Barrel barrel in barrelsToExplode) {
            barrel.Explode();
        }
    }
    public void OnDrawGizmos() {
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(explosionPoint.position, radius);
    }
}