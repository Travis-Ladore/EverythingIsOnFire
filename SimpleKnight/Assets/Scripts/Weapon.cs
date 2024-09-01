using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1; // Damage the weapon deals

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Assumes enemies have the tag "Enemy"
        {
            BasicEnemy enemy = collision.GetComponent<BasicEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Apply damage to the enemy
            }
        }
    }
}
