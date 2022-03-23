using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int hp = 100;
    [SerializeField] private int maxHP = 100;

    private bool isDead = false;

    public void TakeDamage(int damageAmount) {
        hp -= damageAmount;

        if (hp < 0) {
            hp = 0;
            isDead = true;

            // TODO: handle game over, re-spawn, etc.
        }
    }

}
