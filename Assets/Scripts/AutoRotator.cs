using UnityEngine;

public class AutoRotator : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 1f;

    private float rotationAngle = 0f;

    void Update() {
        rotationAngle += rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
    }
}
