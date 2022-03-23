using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float alertDistance = 10f;
    [SerializeField] private float attackDistance = 0.6f;

    private Animator animator = null;
    private NavMeshAgent agent = null;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (animator != null && animator.GetBool("Dead")) {
            return;
        }

        Vector3 targetDirection = (target.position - transform.position);
        targetDirection.y = 0f;

        if (targetDirection.magnitude < attackDistance) {
            // we are within range, so we can attack
            agent.enabled = false;
            animator.SetFloat("Forward", 0f);
            animator.SetInteger("AttackNum", Random.Range(0, 5));
            animator.SetTrigger("Attack");

            // make sure we're aimed at the target
            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
        } else if (targetDirection.magnitude < alertDistance) {
            // we've detected the target, move in to attack
            agent.enabled = true;
            agent.SetDestination(target.position);
            animator.SetFloat("Forward", agent.velocity.magnitude);
        } else {
            // the target is too far away to detect
        }
    }

}
