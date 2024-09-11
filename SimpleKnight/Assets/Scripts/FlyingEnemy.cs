using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    public float moveSpeed = 5f; // Speed at which the enemy moves horizontally towards the player
    public float swoopSpeed = 10f; // Speed during swooping
    public float swoopDistance = 5f; // Distance from the player within which the enemy will swoop down
    public float swoopHeight = 10f; // Height of the swoop
    public float swoopDuration = 1f; // Duration of the swoop

    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool isSwooping = false;
    private float swoopStartTime;
    private Vector2 initialPosition;
    public int additionalCoinReward = 5;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            if (isSwooping)
            {
                SwoopTowardsPlayer();
            }
            else
            {
                MoveHorizontallyTowardsPlayer();
                CheckForSwoop();
            }
        }
    }

    private void MoveHorizontallyTowardsPlayer()
    {
        // Move horizontally towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private void CheckForSwoop()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Start swooping if within the swoop distance
        if (distanceToPlayer <= swoopDistance)
        {
            isSwooping = true;
            swoopStartTime = Time.time;
            // Optionally, update initial position to be current position when swooping starts
            initialPosition = transform.position;
        }
    }

    private void SwoopTowardsPlayer()
    {
        // Calculate the interpolation factor
        float t = (Time.time - swoopStartTime) / swoopDuration;

        // Check if swooping is complete
        if (t >= 1f)
        {
            t = 1f;
            isSwooping = false;
            rb.velocity = Vector2.zero; // Stop movement after swooping
            return;
        }

        // Compute the swoop target and position
        Vector2 swoopTarget = new Vector2(playerTransform.position.x, playerTransform.position.y + swoopHeight);
        Vector2 swoopPosition = Vector2.Lerp(initialPosition, swoopTarget, t);

        // Convert player position to Vector2 if necessary
        Vector2 playerPosition2D = new Vector2(playerTransform.position.x, playerTransform.position.y);

        // Calculate direction towards the player from the swoop position
        Vector2 direction = (playerPosition2D - swoopPosition).normalized;

        // Apply swooping velocity
        rb.velocity = new Vector2(direction.x * swoopSpeed, direction.y * swoopSpeed);
    }

    protected override void Die()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.AddCoins(additionalCoinReward);
        }
        base.Die();
    }
}
