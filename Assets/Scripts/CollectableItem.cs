using UnityEngine;

public class CollectableItem : InteractableObject {
    public virtual void Collect() {
        Debug.Log("Item collected: " + gameObject.name);
    }

    private void HideMesh(GameObject gameObject) {
        gameObject.SetActive(false);
    }

    public override void Interact() {
        Collect();

        HideMesh(gameObject);
    }
}
