using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : EnemyBase
{
    public float speed = 2f; // Speed at which the enemy moves horizontally towards the player
    public float jumpForce = 10f; // Force of the jump towards the player
    public float jumpDistance = 3f; // Distance from the player within which the enemy will jump

    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isGrounded;

    public Transform groundCheck; // Transform used to check if the enemy is grounded
    public LayerMask groundLayer; // Layer mask for the ground

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player has the tag "Player"
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
        CheckGrounded(); // Check if the enemy is on the ground
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            // Move horizontally towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            Vector2 horizontalMovement = new Vector2(direction.x, 0);

            rb.velocity = new Vector2(horizontalMovement.x * speed, rb.velocity.y);

            // Jump towards the player if within the jump distance and on the ground
            if (distanceToPlayer <= jumpDistance && isGrounded)
            {
                JumpTowardsPlayer();
            }
        }
    }

    private void JumpTowardsPlayer()
    {
        // Apply a force upwards and towards the player to jump
        Vector2 jumpDirection = (playerTransform.position - transform.position).normalized;
        rb.AddForce(new Vector2(jumpDirection.x, 1) * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGrounded()
    {
        // Check if the enemy is grounded using a small circle at the ground check position
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
