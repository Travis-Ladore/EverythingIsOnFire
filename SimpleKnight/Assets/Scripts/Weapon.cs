using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 1; // Damage the weapon deals

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has an EnemyBase component (either standard enemy or jumping enemy)
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Apply damage to the enemy
        }
    }

}
