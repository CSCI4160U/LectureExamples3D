using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private bool isDead = false;

    private Animator animator = null;
    private NavMeshAgent agent = null;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damage) {
        this.hp -= damage;

        if (this.hp <= 0) {
            this.hp = 0;

            this.isDead = true;

            if (agent != null) {
                agent.enabled = false;
            }

            if (animator != null) {
                animator.SetInteger("AttackNum", 0);
                animator.SetFloat("Forward", 0f);
                animator.SetBool("Dead", true);
            }
        } else {
            this.isDead = false;
        }
    }

    public int GetMaxHP() { return maxHP; }
    
    public int GetHP() { return hp; }

    public bool IsDead() { return isDead; }
}
