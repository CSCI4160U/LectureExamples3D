using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Patrolling, Alerted, TargetVisible, Dead
}

public class EnemyAIStateMachine : MonoBehaviour {
    [SerializeField] private EnemyState currentState = EnemyState.Patrolling;

    [Header("Patrolling")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int waypointIndex = 0;
    [SerializeField] private bool patrolLoop = true;
    [SerializeField] private float closeEnoughDistance = 1f;

    [Header("Alerted")]
    [SerializeField] private float lastAlertTime = 0f;
    [SerializeField] private float alertCooldown = 8f;
    [SerializeField] private Vector3 lastKnownTargetPosition;

    [Header("Target Visible")]
    [SerializeField] private float lastShootTime = 0f;
    [SerializeField] private float shootCooldown = 1f;
    [SerializeField] private Transform target = null;

    private Animator animator = null;
    private NavMeshAgent agent = null;

    private void Awake() {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
        currentState = EnemyState.Patrolling;

        if (agent != null && waypoints != null && waypoints.Length > 0) {
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    public void Pause() {
        agent.enabled = false;
        animator.speed = 0f;
    }

    public void Resume() {
        agent.enabled = true;
        animator.speed = 1f;
    }

    public EnemyState GetState() {
        return currentState;
    }

    public void SetState(EnemyState newState) {
        if (currentState == newState) {
            return;
        }

        if (newState == EnemyState.Patrolling) {
            // resume patrol
            agent.enabled = true;
            waypointIndex = 0;
        } else if (newState == EnemyState.Alerted) {
            // investigate their last known position
            agent.enabled = true;
            agent.SetDestination(lastKnownTargetPosition);

            // remember when we entered this state
            lastAlertTime = Time.time;
        } else if (newState == EnemyState.TargetVisible) {
            // shoot at the target
            agent.enabled = false;
            animator.SetFloat("Forward", 0f);
            lastKnownTargetPosition = target.transform.position;
        } else {
            // disable navigation and play death animation
            agent.enabled = false;
            animator.SetFloat("Forward", 0f);
            animator.SetBool("Dead", true);
        }
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    public void SetLastKnownTargetLocation(Vector3 location) {
        this.lastKnownTargetPosition = location;
    }

    private void Update() {
        if (currentState == EnemyState.Dead) {
            return;
        } else if (currentState == EnemyState.Patrolling) {
            Patrol();
        } else if (currentState == EnemyState.Alerted) {
            if (Time.time > (lastAlertTime + alertCooldown)) {
                SetState(EnemyState.Patrolling);
                Patrol();
            } else {
                Alert();
            }
        } else if (currentState == EnemyState.TargetVisible) {
            Shoot();
        }
    }

    private void Patrol() {
        float distanceToWaypoint = Vector3.Distance(agent.transform.position, waypoints[waypointIndex].position);
        if (distanceToWaypoint < closeEnoughDistance) {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length) {
                if (patrolLoop) {
                    waypointIndex = 0;
                } else {
                    animator.SetFloat("Forward", 0);
                    agent.enabled = false;
                }
            }

            agent.SetDestination(waypoints[waypointIndex].position);
        }

        animator.SetFloat("Forward", agent.velocity.magnitude);
    }

    private void Alert() {
        float distanceToTarget = Vector3.Distance(agent.transform.position, lastKnownTargetPosition);

        if (distanceToTarget < closeEnoughDistance) {
            animator.SetFloat("Forward", 0f);

            // TODO: play a "look around" animation
        } else {
            animator.SetFloat("Forward", agent.velocity.magnitude);
        }
    }

    private void Shoot() {
        if (Time.time > (lastShootTime + shootCooldown)) {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.15f);

            animator.SetTrigger("Shoot");
            lastShootTime = Time.time;

            // TODO: do a raycast and calculate damage
        }
    }
}
