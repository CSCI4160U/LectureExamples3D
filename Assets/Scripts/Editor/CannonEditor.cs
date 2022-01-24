using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cannon))]
public class CannonEditor : Editor {
    [DrawGizmo(GizmoType.Pickable | GizmoType.Selected)]
    static void DrawGizmosSelected(Cannon cannon, GizmoType gizmoType) {
        float dashSize = 4f;

        // draw the starting point
        var launchPosition = cannon.LaunchPosition.position;
        Handles.DrawDottedLine(cannon.transform.position, launchPosition, dashSize);
        Handles.Label(launchPosition, "Launch Point");

        // if we don't have a projectile, we can't do anything
        if (cannon.ProjectilePrefab == null) {
            return;
        }

        // calculate the predicted trajectory
        var velocity = cannon.transform.up * cannon.LaunchVelocity;
        var position = launchPosition;
        var positions = new List<Vector3>();
        var physicsStep = 0.1f;
        for (var t = 0f; t <= 2f; t += physicsStep) {
            positions.Add(position);
            position += velocity * physicsStep;
            velocity += Physics.gravity * physicsStep;
        }
        positions.Add(position);

        // draw the trajectory as a sequence of lines
        using (new Handles.DrawingScope(Color.yellow)) {
            Handles.DrawAAPolyLine(positions.ToArray());
            Gizmos.DrawWireSphere(positions[positions.Count - 1], 0.125f);
            Handles.Label(positions[positions.Count - 1], "Estimated Position (after 2s)");
        }
    }

    private void OnSceneGUI() {
        var cannon = target as Cannon;
        var transform = cannon.transform;

        using (var cc = new EditorGUI.ChangeCheckScope()) {
            // allow user to change the launch position
            var newLaunchPosition = Handles.PositionHandle(cannon.LaunchPosition.position, cannon.transform.rotation);

            // if the position changed, record the offset for undoing
            if (cc.changed) {
                Undo.RecordObject(cannon.LaunchPosition, "Launch Point Move");
                cannon.LaunchPosition.position = newLaunchPosition;
            }
        }

        // create a fire button in the scene view
        Handles.BeginGUI();
        var rectMin = Camera.current.WorldToScreenPoint(cannon.LaunchPosition.position);
        var rect = new Rect();
        rect.xMin = rectMin.x;
        rect.yMin = SceneView.currentDrawingSceneView.position.height - rectMin.y;
        rect.width = 64;
        rect.height = 20;
        GUILayout.BeginArea(rect);
        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying)) {
            if (GUILayout.Button("Fire")) {
                cannon.Fire();
            }
        }
        GUILayout.EndArea();
        Handles.EndGUI();
    }
}
