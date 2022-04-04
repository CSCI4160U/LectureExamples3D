using UnityEngine;

public class CollectableHealth : CollectableItem {
    [SerializeField] protected PlayerHealth playerHealth;
    [SerializeField] protected int healthGain = 40;

    public override void Collect() {
        playerHealth.Heal(healthGain);
    }
}
