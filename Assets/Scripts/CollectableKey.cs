using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableKey : CollectableItem {
    [SerializeField] protected ToggleInteractable[] objectsToUnlock;

    public override void Collect() {
        for (int i = 0; i < objectsToUnlock.Length; i++) {
            objectsToUnlock[i].Unlock();
        }
    }
}
