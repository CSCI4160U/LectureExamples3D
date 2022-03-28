using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthRagdoll : MonoBehaviour {
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int hp = 100;

    [SerializeField] private bool isDead = false;
    [SerializeField] Ragdoller ragdoll = null;

    private Animator animator = null;
    private NavMeshAgent agent = null;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount) {
        hp -= damageAmount;
        
        if (animator != null) {
            animator.SetTrigger("Hit");
        }

        if (hp <= 0) {
            hp = 0;

            isDead = true;

            if (agent != null) {
                agent.enabled = false;
            }


            if (animator != null) {
                animator.SetBool("Dead", true);
            }

            if (ragdoll != null) {
                ragdoll.Ragdoll(transform);
                ragdoll.gameObject.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
    }

    public void TakeExplosionDamage(Vector3 explosionSource, float forceAmount) {
        if (ragdoll != null) {
            ragdoll.Ragdoll(transform);
            ragdoll.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);

            Vector3 towardUs = (transform.position - explosionSource).normalized;
            ragdoll.ApplyForce(towardUs, forceAmount);
            Debug.Log("TakeExplosionDamage::forceAmount: " + forceAmount);
        }
    }
}
