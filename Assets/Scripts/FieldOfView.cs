using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAIStateMachine))]
public class FieldOfView : MonoBehaviour {
    [Header("Vision")]
    [SerializeField] private Transform eye;
    [SerializeField] [Range(0, 360)] private float fovAngle = 60f;
    [SerializeField] private float visionRadius = 10f;

    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask wallLayers;
    [SerializeField] private float targetScanDelay = 0.25f;

    [Header("State Management")]
    [SerializeField] private bool isAlerted = false;

    private EnemyAIStateMachine stateMachine;

    private void Awake() {
        stateMachine = GetComponent<EnemyAIStateMachine>();
    }

    private void OnDrawGizmos() {
        Gizmos.matrix = Matrix4x4.identity;
        Vector3 focusPos = eye.position + eye.forward * visionRadius;

        if (stateMachine != null) {
            if (stateMachine.GetState() == EnemyState.TargetVisible) {
                Gizmos.color = Color.red;
            } else if (stateMachine.GetState() == EnemyState.Alerted) {
                Gizmos.color = Color.yellow;
            }
        } else {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(eye.position, 0.2f);
        Gizmos.DrawWireSphere(eye.position, visionRadius);
        Gizmos.DrawLine(eye.position, focusPos);
    }

    private void Start() {
        StartCoroutine(KeepSearchingForTargets(targetScanDelay));
    }

    IEnumerator KeepSearchingForTargets(float delay) {
        while (true) {
            yield return new WaitForSeconds(delay);
            LookForTargets();
        }
    }

    private void LookForTargets() {
        // Step 1:  Look for objects within our vision radius
        bool canSeeTarget = false;
        Collider[] targets = Physics.OverlapSphere(eye.position, visionRadius, targetLayer);

        for (int i = 0; i < targets.Length; i++) {
            Vector3 targetPos = targets[i].transform.position;
            targetPos.y += 1f;
            Vector3 targetDirection = (targetPos - eye.position).normalized;

            // Step 2:  Check if the target is within the field of view
            if (Vector3.Angle(eye.forward, targetDirection) < fovAngle) {
                // Step 3:  Check if we have line of sight
                float distance = Vector3.Distance(eye.position, targetPos);
                if (!Physics.Raycast(eye.position, targetDirection, distance, wallLayers)) {
                    canSeeTarget = true;
                    isAlerted = true;

                    // change state
                    stateMachine.SetTarget(targets[i].transform);
                    stateMachine.SetState(EnemyState.TargetVisible);

                    return; // TODO: choose among multiple targets
                }
            }
        }

        if (!canSeeTarget && isAlerted) {
            stateMachine.SetState(EnemyState.Alerted);
            isAlerted = false;
        }
    }
}

