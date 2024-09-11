using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the enemy
    protected int currentHealth;
    public AudioClip deathSound; // Sound to play when the enemy dies
    private AudioSource audioSource; // Reference to the AudioSource component
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private Rigidbody2D rigidbody2D;
    public int coinReward = 10;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        // If no AudioSource is found, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the default properties for the audio source
        audioSource.playOnAwake = false;

        // Get references to the components we want to disable
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.AddCoins(coinReward);
        }
        // Play death sound
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Disable components to make the enemy disappear visually and stop interacting
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }
        if (rigidbody2D != null)
        {
            rigidbody2D.simulated = false;
        }

        // Destroy the enemy GameObject after the sound plays
        Destroy(gameObject, deathSound != null ? deathSound.length : 0f);
    }

}
