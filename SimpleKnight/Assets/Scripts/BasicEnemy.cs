using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed = 5f; // Speed at which the enemy moves towards the player
    public int maxHealth = 3; // Maximum health of the enemy
    private int currentHealth;

    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has the tag "Player"
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // You could add a death animation or effect here
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Optionally handle collision responses if needed
        // Example: Check collision with walls or other enemies
    }
}
