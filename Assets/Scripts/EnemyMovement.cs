using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;
    [SerializeField] private float closeEnoughDistance = 1f;
    [SerializeField] private bool looping = false;

    private NavMeshAgent agent;
    private Animator animator;

    private bool patrolling = true;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        if (agent != null && waypoints.Length > 0 && waypointIndex < waypoints.Length) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    private void Update() {
        if (!patrolling) {
            return;
        }

        float distanceToWaypoint = Vector3.Distance(agent.transform.position, waypoints[waypointIndex].position);
        if (distanceToWaypoint < closeEnoughDistance) {
            // we're here, move to the next waypoint if there is one
            waypointIndex++;

            // loop, if desired
            if (waypointIndex >= waypoints.Length) {
                if (looping) {
                    waypointIndex = 0;
                } else {
                    patrolling = false;
                    animator.SetFloat("Forward", 0f);
                    return;
                }
            }

            // navigate to the new waypoint
            agent.SetDestination(waypoints[waypointIndex].position);
        }

        animator.SetFloat("Forward", agent.speed);
    }
}
