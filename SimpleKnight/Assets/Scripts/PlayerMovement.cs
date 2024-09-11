using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float vertical;
    public float speed = 15f;
    public float jumpingPower = 16f;
    public bool isFacingRight = true;
    public float fastFallMultiplier = 2f;
    public int coinCount = 0;


    private void Update()
    {
        if(isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        else if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        // Handle fast falling
        if (vertical < 0f && !IsGrounded() && rb.velocity.y <= 0f) // Only fast fall if pressing down, not grounded, and falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fastFallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontal = input.x;
        vertical = input.y;
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins Added: " + amount + ", Total Coins: " + coinCount);
    }

}
