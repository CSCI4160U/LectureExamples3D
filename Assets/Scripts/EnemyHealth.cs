using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int hp = 100;
    [SerializeField] private bool isDead = false;

    public void TakeDamage(int damage) {
        this.hp -= damage;

        if (this.hp <= 0) {
            this.hp = 0;
            this.isDead = true;
        } else {
            this.isDead = false;
        }
    }

    public int GetMaxHP() { return maxHP; }
    
    public int GetHP() { return hp; }

    public bool IsDead() { return isDead; }
}
