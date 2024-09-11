using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    public float speed = 5f; // Speed at which the enemy moves towards the player
    private Transform playerTransform;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
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
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
    }
}
