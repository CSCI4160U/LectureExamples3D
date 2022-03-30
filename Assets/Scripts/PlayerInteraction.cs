using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour {
    [SerializeField] private Transform camera;
    [SerializeField] private float interactionRange = 3f;

    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private LayerMask interactableMask;

    private void Update() {
        RaycastHit hit;
        InteractableObject interactableObject = null;

        if (Physics.Raycast(camera.position, camera.forward, out hit, interactionRange, interactableMask)) {
            interactableObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            if (interactableObject != null) {
                interactionText.text = interactableObject.GetInteractionText();
            } else {
                interactionText.text = "";             
            }
        } else {
            interactionText.text = "";
        }

        if (Input.GetButtonDown("Fire2") && interactableObject != null) {
            interactableObject.Interact();
        }
    }
}
