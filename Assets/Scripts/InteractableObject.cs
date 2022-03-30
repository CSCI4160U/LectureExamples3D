using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField] protected string activateText = "Right-click to interact";

    public virtual void Interact() {
        Debug.Log(gameObject.name + "::Interact()");
    }

    public virtual string GetInteractionText() {
        return activateText;
    }
}
