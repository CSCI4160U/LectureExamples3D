using UnityEngine;

public class UnlockingInteractable : ToggleInteractable {
    [SerializeField] protected ToggleInteractable[] objectsToUnlock;

    public override void Interact() {
        base.Interact();

        if (!isLocked) {
            for (int i = 0; i < objectsToUnlock.Length; i++) {
                objectsToUnlock[i].Unlock();
            }
        }
    }
}
