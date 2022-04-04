using UnityEngine;

public class CollectableWeapon : CollectableItem {
    [SerializeField] protected WeaponInventory inventory;
    [SerializeField] protected int ammoGain = 12;
    [SerializeField] protected bool includesWeapon = false;

    public override void Collect() {
        inventory.ammunition += ammoGain;

        if (includesWeapon) {
            inventory.activeWeaponExists = true;
        }
    }
}
